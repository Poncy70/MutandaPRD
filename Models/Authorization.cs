namespace Mutanda.Models
{
    public class Authorization : BaseModel
    {
        public string DeviceMail { get; set; }
        public string DBName { get; set; }
        public int IdAgente { get; set; }
        public bool SuperUser { get; set; }
        public short OAuthProvider { get; set; }
        public bool AccesDenied { get; set; }
    }
}
