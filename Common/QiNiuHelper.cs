using Qiniu.Conf;
using Qiniu.IO;
using Qiniu.RPC;
using Qiniu.RS;
using Qiniu.RSF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class QiNiuHelper
    {
        //初始化密钥
        public static void InitQiniuConfig()
        {
            Config.ACCESS_KEY = ConfigurationHelper.AppSetting("ACCESS_KEY");
            Config.SECRET_KEY = ConfigurationHelper.AppSetting("SECRET_KEY");
        }
        /// <summary>
        /// 获取域名
        /// </summary>
        /// <returns></returns>
        public static string GetDomain()
        {
            return ConfigurationHelper.AppSetting("Domain");
        }

        /// <summary>
        /// 获取空间名称
        /// </summary>
        /// <returns></returns>
        public static string GetBucket()
        {
            return ConfigurationHelper.AppSetting("Bucket");
        }


        /// <summary>
        /// 上传单个文件
        /// </summary>
        /// <param name="bucket">空间名称</param>
        /// <param name="key">存储的文件名称</param>
        /// <param name="fname">路径+名称  例如：http:\\newband.com\image\newband.jpg</param>
        public static bool PutFile(string bucket, string key, string fname)
        {
            bool result = false;
            try
            {
                InitQiniuConfig();

                var policy = new PutPolicy(bucket, 3600);
                string upToken = policy.Token();
                PutExtra extra = new PutExtra();
                IOClient client = new IOClient();
                client.PutFile(upToken, key, fname, extra);

                result = true;
            }
            catch
            {

            }

            return result;
        }


        /// <summary>
        /// 上传图片流
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="key"></param>
        /// <param name="putStream"></param>
        /// <returns></returns>
        public static string PutFile(string bucket, string key, Stream putStream)
        {
            try
            {
                InitQiniuConfig();
                //获取token
                var policy = new PutPolicy(bucket, 3600);
                string upToken = policy.Token();
                PutExtra extra = new PutExtra();
                IOClient client = new IOClient();

                //发送保存文件的请求
                PutRet result = client.Put(upToken, key, putStream, extra);
                if (result == null || string.IsNullOrEmpty(result.key) || result.OK == false) return "";

                //返回文件名称
                return result.key;
            }
            catch
            {
                return "";
            }
        }



        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="bucket">文件所在的空间名</param>
        /// <param name="key">文件key</param>
        public static bool Delete(string bucket, string key)
        {
            bool result = false;

            try
            {
                InitQiniuConfig();

                RSClient client = new RSClient();
                CallRet ret = client.Delete(new EntryPath(bucket, key));
                if (ret.OK)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch { }

            return result;
        }



        /// <summary>
        /// 获取七牛所有图片的访问集合
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="imageCount"></param>
        /// <returns></returns>
        public static string[] GetFileList(string bucket, ref int imageCount)
        {
            try
            {
                InitQiniuConfig();
                RSFClient client = new RSFClient(bucket);

                //获取100000 数量
                DumpRet dumpRet = client.ListPrefix(bucket, "", "", 100000);
                if (dumpRet == null || dumpRet.Items.Count <= 0) return null;

                //服务器上图片总数量
                imageCount = dumpRet.Items.Count;

                //获取的图片名称
                List<string> imagePath = new List<string>();
                foreach (DumpItem item in dumpRet.Items)
                    imagePath.Add(item.Key);

                return imagePath.ToArray();
            }
            catch
            {
                imageCount = 0;
                return null;
            }
        }




    }
}
