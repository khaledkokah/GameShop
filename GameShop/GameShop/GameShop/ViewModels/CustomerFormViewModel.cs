using GameShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameShop.ViewModels
{
    public class CustomerFormViewModel
    {
        //IEnumerable because you only need to iterate, no need to other List Add, remove, ..etc 
        public IEnumerable<CustomerType> CustomerTypes { get; set; }
        public Customer Customer { get; set; }
    }
}