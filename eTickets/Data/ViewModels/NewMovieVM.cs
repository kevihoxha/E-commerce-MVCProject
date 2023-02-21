using eTickets.Data.Base;
using eTickets.Models.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class NewMovieVM
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Movie Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [Display(Name = "Description Name")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Name is $.")]
        [Display(Name = "Price Name")]
        public double Price { get; set; }
        [Required(ErrorMessage = "ImageURL is required.")]
        [Display(Name = "Image URL")]
        public string ImageURL { get; set; }
        [Required(ErrorMessage = "Start Date is required.")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End Date is required.")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Movie Category is required.")]
        [Display(Name = "Movie Category")]
        public MovieCategory MovieCategory { get; set; }
        //Relationships
        [Required(ErrorMessage = "Movie Actors is required.")]
        [Display(Name = "Movie Actors")]
        public List<int>? ActorIds{ get; set; }
        [Required(ErrorMessage = "Movie Cinema is required.")]
        [Display(Name = "Movie Cinema")]
        public int CinemaId { get; set; }
        [Required(ErrorMessage = "Movie Producer is required.")]
        [Display(Name = "Producer Cinema")]
        public int ProducerId { get; set; }
    }
}
