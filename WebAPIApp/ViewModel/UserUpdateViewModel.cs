using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIApp.ViewModel
{
    public class UserUpdateViewModel
    {
        [BindNever]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        [BindNever]
        public DateTime CreationDate { get; set; }
    }
}
