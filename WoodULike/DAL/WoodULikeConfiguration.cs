using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace WoodULike.DAL
{
    public class WoodULikeConfiguration : DbConfiguration
    {
        public WoodULikeConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }

        //TODO: Finish bespoke method to determine transient errors
        //public class WoodULikeExecutionStrategy : DbExecutionStrategy
        //{
        //    protected override bool ShouldRetryOn(Exception exception)
        //    {
        //        if ((exception.InnerException is TimeoutException || exception.InnerException is SqlException) && 
        //            (exception. == 40613 ||
        //             exception.HResult == 40501 ||
        //             exception.HResult == 40197 ||
        //             exception.HResult == 10929 ||
        //             exception.HResult == 10928 ||
        //             exception.HResult == 10060 ||
        //             exception.HResult == 10054 ||
        //             exception.HResult == 10053 ||
        //             exception.HResult == 233 ||
        //             exception.HResult == 64 ||
        //             exception.HResult == 20))
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }
        //}


    }
}