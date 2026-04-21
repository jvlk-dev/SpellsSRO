using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SpellsSRO
{
    /// <summary>
    /// Třída Monstrum reprezentuje nepřátelskou bytost v hře.
    /// </summary>
    public class Monstrum : Bytost
    {
        // Vlastnosti
        public string Nazev { get; set; }

        public int Penize { get; set; }

        // Konstruktory

        /// <summary>
        /// Výchozí konstruktor třídy Monstrum.
        /// </summary>
        public Monstrum() { }

        /// <summary>
        /// Konstruktor třídy Monstrum s parametry pro inicializaci vlastností.
        /// </summary>
        /// <param name="jmeno">Jméno monstra.</param>
        /// <param name="zdravi">Zdraví monstra.</param>
        /// <param name="sila">Síla monstra.</param>
        /// <param name="penize">Peníze (měna) monstra.</param>
        public Monstrum(string jmeno, int zdravi, int sila, int penize) : base(zdravi, sila)
        {
            Nazev = jmeno;
            Penize = penize;
        }

        // Metody

        /// <summary>
        /// Přepisuje metodu ToString pro zobrazení informací o monstru.
        /// </summary>
        /// <returns>Textová reprezentace monstra.</returns>
        public override string ToString()
        {
            return $"Jmeno: {Nazev}, Zdravi: {Zdravi}, Sila: {Sila}, Level: {Penize}";
        }

        /// <summary>
        /// Metoda VypisInformace vypíše informace o monstru na konzoli.
        /// </summary>
        public void VypisInformace()
        {
            Console.WriteLine($"Nazev: {Nazev}, Zdravi: {Zdravi}, Sila: {Sila}, Penize: {Penize}");
        }

        /// <summary>
        /// Metoda VytvoritMonstrum vytvoří monstrum na základě úrovně hráče.
        /// </summary>
        /// <param name="LevelHrace">Úroveň hráče, podle které se generuje monstrum.</param>
        public void VytvoritMonstrum(int LevelHrace)
        {
            Random rnd = new Random();

            string[] mozneNazvy = { "Kostlivec", "Skret", "Ork" };
            Nazev = mozneNazvy[rnd.Next(mozneNazvy.Length)];
            Zdravi = rnd.Next(5 + LevelHrace, 8 + LevelHrace);
            Sila = rnd.Next(5 + LevelHrace, 8 + LevelHrace);
            Penize = rnd.Next(1, LevelHrace + 1);
        }
    }
}

