using System;
using System.Collections.Generic;

#nullable disable

namespace Super.Models
{
    public partial class Prodotto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Prezzo { get; set; }
        public DateTime Datascadenza { get; set; }
        public int? CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
