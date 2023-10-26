using Contracts;
using EntrustContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntrustServices
{
    public sealed class EntrustManager : IEntrustManager
    {
        private readonly Lazy<IEntrustAuthService> _entrustAuthService;

        public EntrustManager(ILoggerManager loggerManager)
        {
            _entrustAuthService = new Lazy<IEntrustAuthService>(() => new EntrustAuthService(loggerManager));
        }
        public IEntrustAuthService EntrustAuthService => _entrustAuthService.Value;
    }
}
