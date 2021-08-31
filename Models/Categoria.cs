using System;
using System.Collections.Generic;

#nullable disable

namespace Super.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Prodotti = new HashSet<Prodotto>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descrizione { get; set; }
        public HashSet<Prodotto> Prodotti;
    }
}
