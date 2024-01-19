using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Repositories
{
    internal class LocalDBRepository
    {
        protected string connectionString;

        public LocalDBRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["LocalDB"].ConnectionString;
        }
    }
}
