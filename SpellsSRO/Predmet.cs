using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellsSRO
{
    /// <summary>
    /// Třída Predmet představuje predmety ve hře s určeným názvem, atributem a hodnotou.
    /// </summary>
    public class Predmet
    {
        // Vlastnosti
        public string Nazev { get; set; }
        public string Atribut { get; set; }
        public int Hodnota { get; set; }

        // Konstruktory

        /// <summary>
        /// Výchozí konstruktor třídy Predmet.
        /// </summary>
        public Predmet() { }

        /// <summary>
        /// Konstruktor třídy Predmet s parametry pro inicializaci názvu, atributu a hodnoty.
        /// </summary>
        /// <param name="nazev">Nazev predmetu.</param>
        /// <param name="atribut">Atribut predmetu.</param>
        /// <param name="hodnota">Hodnota predmetu.</param>
        public Predmet(string nazev, string atribut, int hodnota)
        {
            Nazev = nazev;
            Atribut = atribut;
            Hodnota = hodnota;
        }

        /// <summary>
        /// Přepisuje metodu ToString pro zobrazení informací o predmetu.
        /// </summary>
        /// <returns>Textová reprezentace predmetu.</returns>
        public override string ToString()
        {
            return $"Nazev: {Nazev}, Atribut: {Atribut}, Hodnota: {Hodnota}";
        }
    }
}
