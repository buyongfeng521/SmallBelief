using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HelperCommon
{
    public class UploadHelper
    {
        /// <summary>
        /// 图片上传至七牛
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string UploadImage(HttpPostedFileBase img)
        {
            string result = "";
            string strFileExt = Path.GetExtension(img.FileName);
            if (strFileExt.ToUpper() == ".BMP" || strFileExt.ToUpper() == ".JPG" || strFileExt.ToUpper() == ".JPEG" || strFileExt.ToUpper() == ".PNG")
            {
                string strFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + new Random().Next(1000, 9999) + strFileExt;
                result = QiNiuHelper.PutFile(ConfigurationHelper.AppSetting("Bucket"), strFileName, img.InputStream);
            }

            return result;
        }

        /// <summary>
        /// 根据图片名称上传至七牛
        /// </summary>
        /// <param name="img"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string UploadImage(HttpPostedFileBase img, string fileName)
        {
            string result = "";

            if (img != null && !string.IsNullOrEmpty(fileName))
            {
                QiNiuHelper.Delete(ConfigurationHelper.AppSetting("Bucket"), fileName);
                result = QiNiuHelper.PutFile(ConfigurationHelper.AppSetting("Bucket"), fileName, img.InputStream);
            }

            return result;
        }
    }
}
