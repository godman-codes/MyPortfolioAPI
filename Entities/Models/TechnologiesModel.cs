using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class TechnologiesModel : EntityBase<Guid>
    {
        public TechnologiesModel() : base()
        {

        }


        [Required(ErrorMessage = "Technology name is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }
        public int? Percentage { get; set; }
        [ForeignKey(nameof(UserModel))]
        public string? OwnerId { get; set; }
        public UserModel Owner { get; set; }
    }
}
