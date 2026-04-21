using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SpellsSRO
{
    /// <summary>
    /// Třída Mag reprezentuje postavu maga ve hře.
    /// </summary>
    public class Mag : Bytost
    {
        // Vlastnosti
        public string Jmeno { get; set; }
        public int Penize { get; set; }
        public int MaximalniZdravi { get; set; }
        public int Level { get; set; }
        public Staz[] Staze { get; set; }
        public Monstrum[] Monstra { get; set; }
        public Predmet[] Predmety { get; set; }

        // Konstruktory

        /// <summary>
        /// Výchozí konstruktor třídy Mag.
        /// </summary>
        public Mag() { }

        /// <summary>
        /// Konstruktor třídy Mag s parametry pro inicializaci vlastností.
        /// </summary>
        /// <param name="jmeno">Jméno maga.</param>
        /// <param name="zdravi">Zdraví maga.</param>
        /// <param name="maximalniZdravi">Maximální zdraví maga.</param>
        /// <param name="sila">Síla maga.</param>
        /// <param name="penize">Měna (peníze) maga.</param>
        /// <param name="level">Úroveň (level) maga.</param>
        public Mag(string jmeno, int zdravi, int maximalniZdravi, int sila, int penize, int level, Staz[] staze, Monstrum[] monstra, Predmet[] predmety) : base(zdravi, sila)
        {
            Jmeno = jmeno;
            MaximalniZdravi = maximalniZdravi;
            Penize = penize;
            Level = level;
            Staze = staze;
            Monstra = monstra;
            Predmety = predmety;
        }

        // Metody

        /// <summary>
        /// Přepisuje metodu ToString pro zobrazení informací o magovi.
        /// </summary>
        /// <returns>Textová reprezentace maga.</returns>
        public override string ToString()
        {
            return $"Jméno: {Jmeno}, Zdraví: {Zdravi}, Maximalni zdravi: {MaximalniZdravi}, Síla: {Sila}, Penize: {Penize}, Level: {Level}";
        }

        /// <summary>
        /// Metoda VypisInformace vypíše informace o magovi na konzoli.
        /// </summary>
        public void VypisInformace()
        {
            Console.WriteLine($"Jméno: {Jmeno}, Zdraví: {Zdravi}, Maximalni zdravi: {MaximalniZdravi}, Síla: {Sila}, Penize: {Penize}, Level: {Level}");
        }

        /// <summary>
        /// Metoda VylecitMaga provede léčení maga o určitý počet životů za cenu.
        /// </summary>
        public void VylecitMaga()
        {
            int cenik = 2 + Level;
            int vysledekLeceni = Zdravi + 2 + Level;
            if (Penize >= cenik && vysledekLeceni <= MaximalniZdravi)
            {
                Zdravi = Zdravi + cenik;
                Penize = Penize - 2;
                Console.WriteLine("Hrac se vylecil o " + cenik + " Zdravi");
            }
            else if (Penize >= cenik && vysledekLeceni > MaximalniZdravi)
            {
                int leceniPresCaru = MaximalniZdravi - Zdravi;
                Console.WriteLine("Hrac se vylecil o " + leceniPresCaru + " Zdravi");
                Zdravi = MaximalniZdravi;
            }
            else
            {
                Console.WriteLine("Nedostatek financi.");
            }
        }

        /// <summary>
        /// Metoda ZvysitSilu zvýší sílu maga za cenu.
        /// </summary>
        public void ZvysitSilu()
        {
            int cenik = 2 + Level;
            if (Penize >= cenik)
            {
                Sila = Sila + 1;
                Penize = Penize - cenik;
                Console.WriteLine("Hrac si zvysil silu o " + 1);
            }
            else
            {
                Console.WriteLine("Nedostatek financi.");
            }
        }

        /// <summary>
        /// Metoda ZvysitMaximalniZdravi zvýší maximální zdraví maga za cenu.
        /// </summary>
        public void ZvysitMaximalniZdravi()
        {
            int cenik = 2 + Level;
            if (Penize >= cenik)
            {
                MaximalniZdravi = MaximalniZdravi + 1;
                Penize = Penize - cenik;
                Console.WriteLine("Hrac si zvysil maximalni zdravi o " + 1);
            }
            else
            {
                Console.WriteLine("Nedostatek financi.");
            }
        }

        /// <summary>
        /// Metoda pro přidání nové stáže do seznamu stáží.
        /// </summary>
        public void PridatStaz()
        {
            Random random = new Random();

            List<string> mozneNazvy = new List<string>
            {
                "Dobrodruh",
                "Hrdina",
                "Legenda",
                "Temnota",
                "Drakobijec",
                "Kouzelnik",
                "Alchymista"
            };

            string nahodnyNazev = mozneNazvy[random.Next(mozneNazvy.Count)];

            Staz novaStaz = new Staz(nahodnyNazev, Staze.Length + 1);

            Staz[] noveStaze = new Staz[Staze.Length + 1];
            for (int i = 0; i < Staze.Length; i++)
            {
                noveStaze[i] = Staze[i];
            }
            noveStaze[noveStaze.Length - 1] = novaStaz;
            Staze = noveStaze;
        }

        /// <summary>
        /// Přidá nové monstrum do pole Monstrum.
        /// </summary>
        /// <param name="monstrum">Monstrum k přidání.</param>
        public void PridatMonstrum(Monstrum monstrum)
        {
            Monstrum[] noveMonstra = new Monstrum[Monstra.Length + 1];
            for (int i = 0; i < Monstra.Length; i++)
            {
                noveMonstra[i] = Monstra[i];
            }
            noveMonstra[noveMonstra.Length - 1] = monstrum;
            Monstra = noveMonstra;
        }

        /// <summary>
        /// Určuje šanci na získání předmětu od monstra.
        /// </summary>
        public void SanceNaPredmet()
        {
            Random random = new Random();
            int nahodneCislo = random.Next(0, 100);
            if (nahodneCislo < 30)
            {
                Console.WriteLine("Monstrum u sebe dokonce melo i predmet.");
                PridatPredmet();
            }
        }

        /// <summary>
        /// Přidá nový předmět do seznamu předmětů hráče.
        /// </summary>
        public void PridatPredmet()
        {
            Random random = new Random();
            List<string> mozneNazvyPredmetu = new List<string>
            {
                "Mec",
                "Hulka",
                "Orb",
                "Helma",
                "Vesta",
                "Boty",
                "Sekera"
            };
            string nahodnyNazevPredmetu = mozneNazvyPredmetu[random.Next(mozneNazvyPredmetu.Count)];

            string nahodnyAtribut = random.Next(2) == 0 ? "Sila" : "Zdravi";

            int hodnotaPredmetu = 2 + Level;

            Predmet novyPredmet = new Predmet(nahodnyNazevPredmetu, nahodnyAtribut, hodnotaPredmetu);

            Predmet[] novaPolePredmetu = new Predmet[Predmety.Length + 1];

            for (int i = 0; i < Predmety.Length; i++)
            {
                novaPolePredmetu[i] = Predmety[i];
            }

            novaPolePredmetu[novaPolePredmetu.Length - 1] = novyPredmet;

            Predmety = novaPolePredmetu;

            Console.WriteLine($"{novyPredmet}");
        }
    }
}
