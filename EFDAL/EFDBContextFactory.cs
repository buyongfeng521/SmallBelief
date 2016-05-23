using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDAL
{
    public class EFDBContextFactory
    {
        public DbContext GetDBContext()
        {
            return new SmallBeliefEntities();
        }
    }
}
