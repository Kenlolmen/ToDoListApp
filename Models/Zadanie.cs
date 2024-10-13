using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ToDoListApp.Models;

namespace ToDoListApp.Models
{
    public class Zadanie
    {
        public int Id { get; set; }
        public string Opis { get; set; } = string.Empty;
        public DateTime? Date { get; set; }
        public string IDStatus { get; set; } = string.Empty;
        [ValidateNever]
        public Status Status { get; set; } = null;
        public string IDKategoria { get; set; } = string.Empty;
        [ValidateNever]
        public Kategoria Kategoria { get; set; } = null;

        public bool CzyOpozniony => IDStatus == "open" && Date < DateTime.Today;

    }
}
