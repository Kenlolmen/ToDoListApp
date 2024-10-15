using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using ToDoListApp.Models;

namespace ToDoListApp.Models
{
    public class Zadanie
    {
        public int Id { get; set; }
        public string Opis { get; set; } = string.Empty;
        [Required(ErrorMessage = "Wybierz Date")]
        public DateTime? Date { get; set; }
        [Required(ErrorMessage = "Wybierz Status")]
       // public string IDStatus { get; set; } = string.Empty;
        public string StatusIDStatus { get; set; } = string.Empty;
        [ValidateNever]
        public Status Status { get; set; } = null;
        [Required(ErrorMessage = "Wybierz Kategorie")]   
      //  public string IDKategoria { get; set; } = string.Empty;
        public string KategoriaIDKategoria { get; set; } = string.Empty;
        [ValidateNever]
        public Kategoria Kategoria { get; set; } = null;

        public bool CzyOpozniony => StatusIDStatus == "otw" && Date < DateTime.Today;

    }
}
