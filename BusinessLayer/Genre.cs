using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    class Genre
    {
        [Key]
        public int ID { get; private set; }
        [Required]
        public string Name { get; set; }

        public IEnumerable<Movie> movies { get; set; }

        private Genre()
        {

        }

        public Genre(string name)
        {
            Name = name;
        }
    }
}
