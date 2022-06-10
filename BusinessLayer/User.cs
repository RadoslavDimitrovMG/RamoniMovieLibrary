using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    class User
    {
        [Key]
        public int ID { get; private set; }
        [Required]
        public string Name { get; set; }
        [Range(1, 100, ErrorMessage = "Age must be between {0} and {1}.")]
        public int Age { get; set; }

        public IEnumerable<Movie> movies { get; set; }

        private User()
        {

        }

        public User(string name)
        {
            Name = name;
        }
    }
}

