using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vidya.Domain.Entities
{
    [Table("tbluser")] // Maps to the actual table name in the database
    public class Users
    {
        [Key] // Defines UserId as the primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty; // Ensures it's never null but allows an empty string

        public string Password { get; set; } // Nullable, in case it's not always provided

        public int MasterID { get; set; } // Nullable if not mandatory

        public int RoleID { get; set; } // Nullable if a role isn't always assigned

        public int IsAdmin { get; set; } // Nullable if user type is not always specified

        public string Category { get; set; } // Nullable if category is optional

        public string? AuthAdd { get; set; } // Nullable if not all users have add permissions

        public string? AuthLstEdt { get; set; } // Nullable for last edit permissions

        public string? AuthDel { get; set; } // Nullable for delete permissions

        public DateTime? AddOnDt { get; set; } // Nullable if date is not always provided

        public DateTime? EditOnDt { get; set; } // Nullable if not always edited

        public DateTime? DelOnDt { get; set; } // Nullable if not always deleted

        public int? DelStatus { get; set; } // Nullable if delete status isn't always tracked
    }
}
