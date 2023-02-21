using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Producer:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="ProfilePicture is required.")]
        [Display(Name = "Profile Picture")]
        public string ProfilePicture { get; set; }
        [Required(ErrorMessage = "FullName is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full name must be between 3 to 50 chars")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Biography is required.")]
        [Display(Name = "Biography")]
        public string Bio { get; set; }
        //Relationships
        public List<Movie>? Movies { get; set; }
    }
}
