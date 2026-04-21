using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellsSRO
{
    /// <summary>
    /// Třída Bytost představuje obecnou bytost v hře s určeným zdravím a silou.
    /// </summary>
    public class Bytost
    {
        // Vlastnosti
        public int Zdravi { get; set; }
        public int Sila { get; set; }

        // Konstruktory

        /// <summary>
        /// Výchozí konstruktor třídy Bytost.
        /// </summary>
        public Bytost() { }

        /// <summary>
        /// Konstruktor třídy Bytost s parametry pro inicializaci zdraví a síly.
        /// </summary>
        /// <param name="zdravi">Zdraví bytosti.</param>
        /// <param name="sila">Síla bytosti.</param>
        public Bytost(int zdravi, int sila)
        {
            Zdravi = zdravi;
            Sila = sila;
        }
    }
}
