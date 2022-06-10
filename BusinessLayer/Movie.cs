using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Movie
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(1900, 2022, ErrorMessage = "Year must be between {0} and {1}.")]
        public int ReleaseYear { get; set; }
        
        public int Length { get; set; }
        public IEnumerable<User> Users { get; set; }

        [Required]
        public IEnumerable<Genre> Genres { get; set; }

        private Movie()
        {

        }
        public Movie(string name, int releaseYear, string description, int lenght, IEnumerable<Genre> genres)
        {
            Name = name;
            ReleaseYear = releaseYear;
            Description = description;
            Length = lenght;
            Genres = genres;
        }
    }
}
