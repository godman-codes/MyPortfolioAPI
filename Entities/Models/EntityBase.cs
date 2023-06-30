
using Entities.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class EntityBase<T> : IEntityBase<T>
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public T Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public EntityBase()
        {
            this.IsDeleted = false;
            this.CreatedDate = DateTime.Now;
            this.UpdatedDate = DateTime.Now;
            
        }

        public void ToDeletedEntity()
        {
            this.IsDeleted = true;
            this.UpdatedDate = DateTime.Now;
        }


    }
}
