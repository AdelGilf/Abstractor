using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTO
{
    public class RegistrationDTO
    {
        public string Email { get; set; } = null!;

        public string NickName { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
