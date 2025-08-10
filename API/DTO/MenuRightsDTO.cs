namespace Vidya.API.DTO
{
    public class MenuRightsDTO
    {
        public int IDCode { get; set; }
        public int MenuID { get; set; }
        public int UserId { get; set; }

        public string? AuthAdd { get; set; }
        public string? AuthLstEdt { get; set; }
        public string? AuthDel { get; set; }

        public DateTime? AddOnDt { get; set; }
        public DateTime? EditOnDt { get; set; }
        public DateTime? DelOnDt { get; set; }
        public int DelStatus { get; set; }
    }
}
