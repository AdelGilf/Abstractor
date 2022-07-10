using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models
{
    public class User
    {
        public string Email { get; set; } = null!;

        public string NickName { get; set; } = null!;

        public string Password { get; set; } = null!;

        //public Guid RefreshToken { get; set; }

        //public DateTime CreationTokenTime { get; set; }
    }
}
