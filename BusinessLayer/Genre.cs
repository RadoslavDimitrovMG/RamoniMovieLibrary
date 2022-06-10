using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Genre
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public IEnumerable<Movie> Movies { get; set; }

        private Genre()
        {

        }

        public Genre(string name)
        {
            Name = name;
        }
    }
}
