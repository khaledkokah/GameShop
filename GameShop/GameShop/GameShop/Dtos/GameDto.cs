using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameShop.Dtos
{
    public class GameDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the name")]
        [StringLength(255)]
        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

        public DateTime AddDate { get; set; }

        [Required]
        [Range(1, 20)]
        public int NumberInStock { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public CategoryDto Category { get; set; }
    }
}