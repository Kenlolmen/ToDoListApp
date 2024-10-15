namespace ToDoListApp.Models
{
    public class Filtry
    {
        public Filtry(string filterstring)
        {
             Filterstring = filterstring ?? "all-all-all";
             string[] filtry = Filterstring.Split('-');

            IDKategoria = filtry.Length > 0 ? filtry[0] : "all";
            Due = filtry.Length > 1 ? filtry[1] : "all";
            IDStatus = filtry.Length > 2 ? filtry[2] : "all";

        }
        public string IDStatus { get; set; }
        public string Due { get; set; }
        public string Filterstring { get; set; }
        public string IDKategoria { get; set; }

        public bool CzyKategoria => IDKategoria.ToLower() != "all";
        public bool CzyDue => Due.ToLower() != "all";
        public bool CzyStatus => IDStatus.ToLower() != "all";
        public static Dictionary<string, string> DueFilterValues => new Dictionary<string, string>
        {
            {"przyszlosc","Przyszlosc" },
            {"przeszlosc","Przeszlosc" },
            {"dzis","Dzis" },

        };
        public bool CzyPrzyszlosc => Due.ToLower() == "przyszlosc";
        public bool CzyPrzeszlosc => Due.ToLower() == "przeszlosc";
        public bool CzyDzis => Due.ToLower() == "dzis";


    }
}
