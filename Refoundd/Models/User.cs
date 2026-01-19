using Refoundd.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Refoundd.Models
{
    public class User
    {
        [Key]
        public int User_Id { get; set; }

        [Required(ErrorMessage = "Please enter username.")]
        public string User_Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter first name.")]
        public string First_Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter last name.")]
        public string Last_Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter email.")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter password.")]
        public string Password { get; set; } = string.Empty;

        public int Flag { get; set; } = 0;

        // Navigation property: Items that belong to this user
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
