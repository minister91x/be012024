using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Eshop.Dapper
{
    public abstract class BaseApplicationService
    {
        protected IApplicationDbConnection DbConnection { get; }

        public BaseApplicationService(IServiceProvider serviceProvider)
        {
            DbConnection = serviceProvider.GetRequiredService<IApplicationDbConnection>(); ;
        }

    }
}
