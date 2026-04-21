using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Reflection.Emit;
using System.Net.Security;


namespace SpellsSRO
{
    /// <summary>
    /// Třída Controller slouží k řízení logiky hry a manipulaci s postavami.
    /// </summary>
    public class Controller
    {
        // Vlastnosti
        public string cestaUloziste { get; set; } = "souborNaUlozeni.xml";
        public string inicializacniSoubor { get; set; } = "InicializacniSoubor.xml"; new

        private Mag[] postavy = new Mag[3];

        private Mag[] originalniPostavy = new Mag[3];

        /// <summary>
        /// Konstruktor třídy Controller, který inicializuje instanci třídy.
        /// </summary>
        public Controller()
        {
            Staz[] mojeStaze = new Staz[] { new Staz("Uvod", 1), };
            Monstrum[] mojeMonstra = new Monstrum[] { };
            Predmet[] mojePredmety = new Predmet[] { };
            postavy[0] = new Mag("Gandalf", 11, 11, 11, 11, 1, mojeStaze, mojeMonstra, mojePredmety);
            postavy[1] = new Mag("Brumbal", 10, 15, 15, 10, 1, mojeStaze, mojeMonstra, mojePredmety);
            postavy[2] = new Mag("Gargamel", 10, 10, 10, 15, 1, mojeStaze, mojeMonstra, mojePredmety);

            for (int i = 0; i < postavy.Length; i++)
            {
                originalniPostavy[i] = new Mag(postavy[i].Jmeno, postavy[i].Zdravi, postavy[i].MaximalniZdravi, postavy[i].Sila, postavy[i].Penize, postavy[i].Level, postavy[i].Staze, postavy[i].Monstra, postavy[i].Predmety);
            }

            InicializaceUloziste();
            NacistPostavyZeSouboru();
        }

        // Metody

        /// <summary>
        /// Metoda Clear() vymaže konzoli.
        /// </summary>
        public void Clear()
        {
            Console.Clear();
        }

        /// <summary>
        /// Metoda MainMenu() zobrazí hlavní nabídku hry.
        /// </summary>
        public void MainMenu()
        {
            Clear();
            Console.WriteLine("Vitejte v programu pro magy.");
            Console.WriteLine("Vyberte 1-3.");
            Console.WriteLine("1. Hrat");
            Console.WriteLine("2. Nastavit uloziste");
            Console.WriteLine("3. Ukoncit");
        }

        // --------------- Hra ---------------

        /// <summary>
        /// Metoda Hrat() umožňuje hráči začít hru a vybrat si postavu.
        /// </summary>
        public void Hrat()
        {
            bool vyberPostavy = false;

            while (!vyberPostavy)
            {
                Clear();

                // Načtení postav ze souboru
                NacistPostavyZeSouboru();

                Console.WriteLine("Vyberte za jakou postavu chcete hrat");
                Console.WriteLine("Zvolte 1-3.");

                // Výpis načtených postav
                for (int i = 0; i < postavy.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {postavy[i].Jmeno}");
                }

                string postavaInput = Console.ReadLine();
                int postavaVolba = 0;

                if (int.TryParse(postavaInput, out postavaVolba) && postavaVolba >= 1 && postavaVolba <= postavy.Length)
                {
                    Clear();
                    Console.WriteLine("Spousteni hry");
                    Thread.Sleep(1000);
                    SpustitHru(postavy[postavaVolba - 1], postavaVolba - 1);
                    vyberPostavy = true;
                }
                else
                {
                    Clear();
                    Console.WriteLine("Neplatná volba postavy.");
                    Thread.Sleep(1000);
                }
            }
        }

        /// <summary>
        /// Spustí hru pro zvolenou postavu a umožní hráči provádět akce jako bojovat, léčit se, zvyšovat si životy a sílu.
        /// </summary>
        /// <param name="hrac">Postava, za kterou hráč hraje.</param>
        private void SpustitHru(Mag hrac, int index)
        {

            bool hraBool = false;

            while (!hraBool)
            {
                Monstrum monstrum = new Monstrum();
                monstrum.VytvoritMonstrum(hrac.Level);

                Clear();
                Console.WriteLine("Hrajete za postavu: ");
                Console.WriteLine(hrac);
                Console.WriteLine("Vyberte 1-3.");
                Console.WriteLine("-----------------");
                Console.WriteLine("1. Jit bojovat");
                Console.WriteLine("Vas dalsi nepritel: " + monstrum);
                Console.WriteLine("-----------------");
                Console.WriteLine("2. Vylecit se");
                int zivotyNaVyleceni = 2 + hrac.Level;
                int cenaZaVyleceni = 2 + hrac.Level;
                Console.WriteLine("Cena leceni: " + zivotyNaVyleceni + " Zivotu za " + 2 + " Penez");
                Console.WriteLine("-----------------");
                Console.WriteLine("3. Zvysit si maximalni zdravi");
                int cenaZvyseniMaxZdravi = 2 + hrac.Level;
                Console.WriteLine("Cena za zvyseni maximalniho zdravi: " + 1 + " maximalnich zivotu za " + cenaZvyseniMaxZdravi + " Penez");
                Console.WriteLine("-----------------");
                Console.WriteLine("4. Zvysit si silu");
                int cenaZaZesileni = 2 + hrac.Level;
                Console.WriteLine("Cena za zvyseni sily: " + 1 + " Sila za " + cenaZaZesileni + " Penez");
                Console.WriteLine("-----------------");
                Console.WriteLine("5. Vypis stazi");
                Console.WriteLine("-----------------");
                Console.WriteLine("6. Vypis vami zabytych monster.");
                Console.WriteLine("-----------------");
                Console.WriteLine("7. Vypis vasich predmetu.");
                Console.WriteLine("-----------------");
                Console.WriteLine("8. Ukoncit hru.");


                string hraInput = Console.ReadLine();
                int hraVolba = 0;

                if (int.TryParse(hraInput, out hraVolba))
                {
                    switch (hraVolba)
                    {
                        case 1:
                            Clear();
                            Console.WriteLine("Bojujete proti: " + monstrum);
                            Thread.Sleep(1000);


                            
                            int vysledneZdravi = hrac.Zdravi - monstrum.Sila;
                            hrac.Zdravi = hrac.Zdravi - monstrum.Sila;

                            int monstrumVysledneZdravi = monstrum.Zdravi - hrac.Sila;
                            if (vysledneZdravi <= 0)
                            {
                                Clear();
                                Console.WriteLine("Hrac umrel...");
                                Console.WriteLine("Hra se ukoncuje...");
                                Thread.Sleep(1000);

                                ResetovatPostavu(index);

                                hraBool = true;
                            }
                            else
                            {
                                if (monstrumVysledneZdravi > 0)
                                {
                                    Clear();
                                    Console.WriteLine("Monstrum prezilo, ale zranene utelko!");
                                    Console.WriteLine("Presto i vsak ty zkusenosti za to staly.");
                                    hrac.Level = hrac.Level + 1;
                                    Thread.Sleep(1000);
                                }
                                else
                                {
                                    Clear();
                                    Console.WriteLine("Monstrum zemrelo.");
                                    Console.WriteLine("Odnasite si zkusenosti a par zlataku.");
                                    hrac.SanceNaPredmet();
                                    hrac.PridatStaz();
                                    hrac.PridatMonstrum(monstrum);
                                    hrac.Level = hrac.Level + 1;
                                    hrac.Penize = hrac.Penize + monstrum.Penize;
                                    Thread.Sleep(1000);
                                }
                            }

                            
                            Thread.Sleep(1000);
                            break;
                        case 2:
                            Clear();
                            hrac.VylecitMaga();
                            Thread.Sleep(1000);
                            break;
                        case 3:
                            Clear();
                            hrac.ZvysitMaximalniZdravi();
                            Thread.Sleep(1000);
                            break;
                        case 4:
                            Clear();
                            hrac.ZvysitSilu();
                            Thread.Sleep(1000);
                            break;
                        case 5:
                            Clear();
                            Console.WriteLine("Vase staze: ");
                            Console.WriteLine("---------------");
                            // var staze = hrac.Staze.Where(m => m.Obtiznost == 1);
                            var staze = hrac.Staze.Where(m => true);
                            foreach (var staz in staze)
                            {
                                Console.WriteLine(staz);
                            }
                            Console.WriteLine("---------------");
                            Console.WriteLine("Pro vraceni do menu napiste cokoliv: ");
                            string stazeInput = Console.ReadLine();
                            break;
                        case 6:
                            Clear();
                            Console.WriteLine("Monstra vami zabita: ");
                            Console.WriteLine("---------------");
                            // var skretMonstra = hrac.Monstra.Where(m => m.Nazev == "Skret");
                            var skretMonstra = hrac.Monstra.Where(m => true);
                            foreach (var skretMonstrum in skretMonstra)
                            {
                                Console.WriteLine(skretMonstrum);
                            }
                            Console.WriteLine("---------------");
                            Console.WriteLine("Pro vraceni do menu napiste cokoliv: ");
                            string monstraInput = Console.ReadLine();
                            break;
                        case 7:
                            Clear();
                            Console.WriteLine("Zadejte typ zbrane, kterou chcete zobrazit:");
                            Console.WriteLine("'Zdravi' pro zobrazeni predmetu pridavajici zdravi");
                            Console.WriteLine("'Sila' pro zobrazeni predmetu pridavajici silu");
                            string atributPredmetu = Console.ReadLine();
                            var predmety = hrac.Predmety.Where(p => p.Atribut.Equals(atributPredmetu, StringComparison.OrdinalIgnoreCase));

                            if (!predmety.Any())
                            {
                                Console.WriteLine("Nemáte žádné zbraně tohoto typu.");
                            }
                            else
                            {
                                Console.WriteLine("Vaše zbraně typu " + atributPredmetu + ": ");
                                Console.WriteLine("---------------");
                                foreach (var predmet in predmety)
                                {
                                    Console.WriteLine(predmet);
                                }
                                Console.WriteLine("---------------");
                            }
                            Console.WriteLine("Pro vraceni do menu napiste cokoliv: ");
                            string zbraneInput = Console.ReadLine();
                            break;
                        case 8:
                            Clear();
                            Console.WriteLine("Hra se ukoncuje...");
                            UlozitPostavyDoSouboru();
                            Thread.Sleep(1000);
                            hraBool = true;
                            break;
                        default:
                            Console.WriteLine("Neplatná volba postavy.");
                            Thread.Sleep(2000);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Nepovolené znaky pro volbu postavy.");
                }
            }
        }

        // --------------- Ukladani hry ---------------

        /// <summary>
        /// Metoda ResetovatPostavu resetuje postavu na postavu s puvodnimi daty v pripade smrti postavy.
        /// </summary>
        private void ResetovatPostavu(int index)
        {
            postavy[index].Jmeno = originalniPostavy[index].Jmeno;
            postavy[index].Zdravi = originalniPostavy[index].Zdravi;
            postavy[index].MaximalniZdravi = originalniPostavy[index].MaximalniZdravi;
            postavy[index].Sila = originalniPostavy[index].Sila;
            postavy[index].Penize = originalniPostavy[index].Penize;
            postavy[index].Level = originalniPostavy[index].Level;
            postavy[index].Staze = originalniPostavy[index].Staze;
            postavy[index].Monstra = originalniPostavy[index].Monstra;
            postavy[index].Predmety = originalniPostavy[index].Predmety;
        }

    /// <summary>
    /// Metoda NacistPostavyZeSouboru načte postavy ze souboru XML.
    /// </summary>
    private void NacistPostavyZeSouboru()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Mag[]));

            if (File.Exists(cestaUloziste))
            {
                using (FileStream fileStream = new FileStream(cestaUloziste, FileMode.Open))
                {
                    postavy = (Mag[])serializer.Deserialize(fileStream);
                }
            }
        }

        /// <summary>
        /// Metoda UlozitPostavyDoSouboru uloží postavy do souboru XML.
        /// </summary>
        private void UlozitPostavyDoSouboru()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Mag[]));

            using (FileStream fileStream = new FileStream(cestaUloziste, FileMode.Create))
            {
                serializer.Serialize(fileStream, postavy);
            }
        }

        // --------------- Uloziste ---------------

        /// <summary>
        /// Metoda NastaveniUloziste umožňuje změnit cestu k úložišti dat.
        /// </summary>
        public void NastaveniUloziste()
        {
            int zmenaUlozisteVolba = 0;
            bool zmenaUlozisteBool = false;

            while (!zmenaUlozisteBool)
            {
                Clear();
                Console.WriteLine("Zde muzete upravit cestu pro ukladani hry, chcete pokracovat?");
                Console.WriteLine("Aktualni cesta pro ukladani je: " + cestaUloziste);
                Console.WriteLine("1. Ano.");
                Console.WriteLine("2. Ne, vratit zpět.");
                string inputZmenaUlozisteVolba = Console.ReadLine();
                if (int.TryParse(inputZmenaUlozisteVolba, out zmenaUlozisteVolba))
                {
                    switch (zmenaUlozisteVolba)
                    {
                        case 1:
                            Clear();
                            Console.WriteLine("Aktualni cesta pro ukladani je: " + cestaUloziste);
                            Console.WriteLine("Zadejte prosim novou cestu, kde se ma soubor vytvorit:");
                            string inputZmenaUloziste = Console.ReadLine();
                            cestaUloziste = inputZmenaUloziste + "\\souborNaUlozeni.xml";
                            UlozeniUkladacihoSouboru(cestaUloziste);
                            zmenaUlozisteBool = true;
                            break;
                        case 2:
                            zmenaUlozisteBool = true;
                            break;
                        default:
                            Console.WriteLine("Zadejte prosim 1 nebo 2");
                            break;
                    }
                }

                else
                {
                    Console.WriteLine("Nepovolene charaktery.");
                }
            }
        }

        /// <summary>
        /// Metoda InicializaceUloziste inicializuje cestu úložiště z existujícího souboru nebo vytvoří nový.
        /// </summary>
        public void InicializaceUloziste()
        {
            if (!File.Exists(inicializacniSoubor))
            {
                using (var writer = new StreamWriter(inicializacniSoubor))
                {
                    writer.Write(cestaUloziste);
                }
                //Console.WriteLine("Xml soubor byl vytvořen a data uložena.");
            }
            else
            {
                using (var reader = new StreamReader(inicializacniSoubor))
                {
                    cestaUloziste = reader.ReadToEnd();
                }
                //Console.WriteLine("Data načtena ze stávajícího xml souboru.");
            }
        }

        /// <summary>
        /// Metoda UlozeniUkladacihoSouboru nahradí obsah inicializačního souboru novými údaji.
        /// </summary>
        /// <param name="cestaUloziste">Cesta k novému úložišti dat.</param>
        public void UlozeniUkladacihoSouboru(string cestaUloziste)
        {
            File.WriteAllText(inicializacniSoubor, cestaUloziste);
            //Console.WriteLine("Obsah souboru byl nahrazen novým obsahem.");
        }
    }
}
