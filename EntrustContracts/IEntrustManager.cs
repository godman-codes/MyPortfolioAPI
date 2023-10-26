using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntrustContracts
{
    public interface IEntrustManager
    {
        //IEntrustAdminService EntrustAdminService { get;  }
        IEntrustAuthService EntrustAuthService { get;  }
    }
}
