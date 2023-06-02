using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
    public interface IEntityBase
    {

        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
        bool IsDeleted { get; set; }
    }
}
