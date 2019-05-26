using GameShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameShop.ViewModels
{
    public class GameFormViewModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public int? Id { get; set; }

        [Required(ErrorMessage = "Please enter the name")]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Category")]
        [Required]
        public int? CategoryId { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Range(1, 20)]
        [Display(Name = "Number in Stock")]
        public int? NumberInStock { get; set; }

        public string Title
        {
            get
            {
                return (Id != 0) ? "Edit Game" : "New Game";
            }
        }

        public GameFormViewModel()
        {
            Id = 0;
        }

        public GameFormViewModel(Game game)
        {
            Id = game.Id;
            Name = game.Name;
            ReleaseDate = game.ReleaseDate;
            NumberInStock = game.NumberInStock;
            CategoryId = game.CategoryId;
        }
    }
}