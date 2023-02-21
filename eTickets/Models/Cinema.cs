﻿using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Cinema:IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Cinema Logo")]
        [Required (ErrorMessage = "Cinema logo is required.")]
        public string Logo { get; set; }
        [Display(Name = "Cinema Name")]
        [Required(ErrorMessage = "Cinema Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Cinema name must be between 3 to 50 chars")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Cinema Description is required.")]
        public string Description { get; set; }
        //Relationships
        public List<Movie>? Movies { get; set; }
    }
}
