using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Technologies : EntityBase<Guid>
    {
        public string Name  { get; set; }
        public int? Percentage { get; set; }
        public Guid UserId { get; set; }
        public User OwnerId { get; set; }
    }
}
