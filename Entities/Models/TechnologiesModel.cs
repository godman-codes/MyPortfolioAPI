using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class TechnologiesModel : EntityBase
    {
        public TechnologiesModel() : base()
        {

        }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("TechnologyId")]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? Percentage { get; set; }
        [ForeignKey(nameof(UserModel))]
        public string? OwnerId { get; set; }
        public UserModel Owner { get; set; }
    }
}
