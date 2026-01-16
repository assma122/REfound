using Refoundd.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Refoundd.Models
{
    public class Item
    {
        [Key]
        public int Item_Id { get; set; }

        [Required(ErrorMessage = "Please enter item name.")]
        public string Item_Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter description.")]
        public string Description { get; set; } = string.Empty;

        // Lost or Found
        [Required(ErrorMessage = "Please select status.")]
        public string Status { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter location.")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter date.")]
        public DateTime Date { get; set; }

        // Foreign Key to User
        [ForeignKey("User")]
        public int User_Id { get; set; }

        // Navigation property
        public User User { get; set; } = null!;
    }
}
