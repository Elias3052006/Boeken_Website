using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BisinessLogicLayer.DTOs
{
    public class UserSignUpDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PostCode { get; set; }
        public int HouseNumber { get; set; }
    }
}
