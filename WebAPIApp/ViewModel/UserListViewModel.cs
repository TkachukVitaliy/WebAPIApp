using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIApp.ViewModel
{
    public class UserListViewModel
    {
        public IEnumerable<UserViewModel> Users { get; set; }
    }
}
