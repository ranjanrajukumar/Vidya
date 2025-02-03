using System;

namespace Vidya.API.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; } // Can be optional for responses
        public int RoleID { get; set; }
        public int IsAdmin { get; set; }
        public int Category { get; set; }
        public string AuthAdd { get; set; }
        public string AuthLstEdt { get; set; }
        public string AuthDel { get; set; }
        public DateTime AddOnDt { get; set; }
        public DateTime? EditOnDt { get; set; } // Nullable
        public DateTime? DelOnDt { get; set; } // Nullable
        public int DelStatus { get; set; }
    }
}
