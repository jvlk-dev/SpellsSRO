using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellsSRO
{
    /// <summary>
    /// Třída Staz představuje obecny ukazatel obtiznosti ve hre.
    /// </summary>
    public class Staz
    {
        // Vlastnosti
        public string Nazev { get; set; }
        public int Obtiznost { get; set; }

        // Konstruktory

        /// <summary>
        /// Výchozí konstruktor třídy Staz.
        /// </summary>
        public Staz() { }

        /// <summary>
        /// Konstruktor třídy Staz s parametry pro inicializaci nazvu a obtiznosti.
        /// </summary>
        /// <param name="nazev">Nazev staze.</param>
        /// <param name="obtiznost">Obtiznost staze.</param>
        public Staz(string nazev, int obtiznost)
        {
            Nazev = nazev;
            Obtiznost = obtiznost;
        }

        public override string ToString()
        {
            return $"Nazev: {Nazev}, Obtiznost: {Obtiznost}";
        }
    }
}
