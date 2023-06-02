
using Entities.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class EntityBase : IEntityBase
    { 
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public EntityBase()
        {
            IsDeleted = false;
            CreatedDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
            
        }

        public void ToDeletedEntity()
        {
            IsDeleted = true;
            UpdatedDate = DateTime.Now;
        }


    }
}
