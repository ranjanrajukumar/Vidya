namespace Vidya.API.DTO
{
    public class MenuListDTO
    {
        public int MenuId { get; set; }
        public int? ParentId { get; set; }
        public string MenuName { get; set; }
        public string MenuDescription { get; set; }
        public string DisplayName { get; set; }
        public string MenuUrl { get; set; }
        public string MenuUrlPath { get; set; }
        public int MenuOrder { get; set; }
        public int SortOrder { get; set; }
        public int IsActive { get; set; }

        public string Category { get; set; }
        public string Icon { get; set; }

        // Permissions
        public string? AuthAdd { get; set; }
        public string? AuthLstEdt { get; set; }
        public string? AuthDel { get; set; }

        // Optional: To hold submenus directly
        public List<MenuListDTO>? SubMenus { get; set; }



    }
}
