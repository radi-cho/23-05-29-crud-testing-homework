using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "Name of genre must not exceed 20 characters.")]
        public string Name { get; set; }

        public List<User> Users { get; set; } = new List<User>();

        public List<Genre> Genres { get; set; } = new List<Genre>();

        private Game()
        {

        }

        public Game(string name, List<User> users = null, List<Genre> genres = null)
        {
            Name = name;
            if (users != null)
            {
                Users = users;
            }
            if (genres != null)
            {
                Genres = genres;
            }
        }
    }
}