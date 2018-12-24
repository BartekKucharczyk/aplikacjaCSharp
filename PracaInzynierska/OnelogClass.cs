
namespace PracaInzynierska
{
    public class OnelogClass
    {
        public string ErrorId { get; set; }
        public string IdLog { get; set; }
        public string Description { get; set; }
       
        public string Date { get; set; }

        public string Severity { get; set; }

        public override string ToString()
        {
            return ErrorId + ":" + Description;
        }


    }
}