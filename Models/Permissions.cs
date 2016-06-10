namespace Mutanda.Models
{
    public class Permissions : BaseModel
    {
        public Permissions()
        {
           
        }
        public string deviceMail { get; set; }
        public string Identif1 { get; set; }
        public string Identif2 { get; set; }
        public int Tipo { get; set; }
        public int Visualizzazione{ get; set; }
        public int Scrittura { get; set; }
        public string Optional { get; set; }
    }
}
