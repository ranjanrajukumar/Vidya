using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vidya.Domain.Entities
{
    [Table("MenuItem")]
    public class MenuList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuId { get; set; }

        public int ParentId { get; set; } // null if no parent

        [Required, StringLength(500)]
        public string MenuName { get; set; }

        [Required, StringLength(500)]
        public string MenuDescription { get; set; }

        [Required, StringLength(500)]
        public string DisplayName { get; set; }

        [Required, StringLength(500)]
        public string MenuUrl { get; set; }

        [Required, StringLength(500)]
        public string MenuUrlPath { get; set; }

        public int MenuOrder { get; set; }
        public int SortOrder { get; set; }
        public int IsActive { get; set; } // bool instead of int

        public string Icon { get; set; }

        public string Category { get; set; }

        public string? AuthAdd { get; set; }
        public string? AuthLstEdt { get; set; }
        public string? AuthDel { get; set; }

        public DateTime? AddOnDt { get; set; }
        public DateTime? EditOnDt { get; set; }
        public DateTime? DelOnDt { get; set; }
        public int? DelStatus { get; set; }

        // ✅ Added navigation property for hierarchical menus
        [NotMapped]
        public List<MenuList> SubMenus { get; set; } = new List<MenuList>();
    }
}

