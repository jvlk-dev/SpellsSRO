using System;
using SpellsSRO;

/// <summary>
/// Třída Program obsahuje vstupní bod Main pro spuštění aplikace.
/// </summary>
class Program
{
    /// <summary>
    /// Vstupní bod aplikace, který inicializuje hlavní menu a kontroler pro správu hry.
    /// </summary>
    static void Main()
    {
        try
        {
            bool mainMenuBool = false;
            int mainMenuVolba = 0;
            Controller controller = new Controller();

            // Hlavní smyčka aplikace
            while (!mainMenuBool)
            {
                // Zobrazení hlavního menu
                controller.MainMenu();
                // Inicializace uložiště
                controller.InicializaceUloziste();
                string inputMainMenu = Console.ReadLine();

                if (int.TryParse(inputMainMenu, out mainMenuVolba))
                {
                    // Zpracování volby z hlavního menu
                    switch (mainMenuVolba)
                    {
                        case 1:
                            // Spuštění hry
                            controller.Hrat();
                            break;
                        case 2:
                            // Nastavení uložiště
                            controller.NastaveniUloziste();
                            break;
                        case 3:
                            // Ukončení aplikace
                            mainMenuBool = true;
                            break;
                        default:
                            Console.WriteLine("Zadejte prosim 1, 2, nebo 3.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Nepovolene charaktery.");
                }
            }
        }
        catch (Exception ex)
        {
            // Zachycení výjimky a zobrazení chybového hlášení
            Console.WriteLine("Došlo k chybě: " + ex.Message);
        }
        finally
        {
            // Zobrazení zprávy při ukončení programu
            Console.WriteLine("Konec programu.");
        }
    }
}

