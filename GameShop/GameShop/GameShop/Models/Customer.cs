using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameShop.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public CustomerType CustomerType { get; set; }

        [Display(Name = "Gamer Type")]
        public int CustomerTypeId { get; set; }
    }
}