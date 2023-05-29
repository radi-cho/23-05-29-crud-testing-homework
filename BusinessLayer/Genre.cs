using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Name of genre must not exceed 20 characters.")]
        public string Name { get; set; }

        public List<User> Users { get; set; } = new List<User>();

        public List<Game> Games { get; set; } = new List<Game>();

        private Genre()
        {

        }

        public Genre(string name, List<User> users = null)
        {
            Name = name;
            if (users != null)
            {
                Users = users;
            }
        }
    }
}