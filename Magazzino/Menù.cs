using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magazzino
{
    class Menù
    {
        static public void Start()
        {
            Console.WriteLine("-------Benvenuto nella gestione Autori!-------");
            int scelta = 0;
            do
            {

                Console.WriteLine("\nPremi");
                Console.WriteLine();
                Console.WriteLine("[1]  Vedere tutti i Prodotti");
                Console.WriteLine("[2]  Aggiungere un nuovo Prodotto");
                Console.WriteLine("[3]  Eliminare un Prodotto");
                Console.WriteLine("[4]  Modificare un Prodotto");
                Console.WriteLine("[5]  Visualizzare i report");
                Console.WriteLine("[0]  Uscire");



                bool isInt = true;

                do
                {
                    isInt = int.TryParse(Console.ReadLine(), out scelta);
                    if (!isInt)
                    {
                        Console.WriteLine("La scelta è sbagliata... Riprova");
                    }
                } while (!isInt);
                switch (scelta)
                {
                    case 1:
                        Console.Clear();
                        DataService.VisualizzaProdotti();

                        break;
                    case 2:
                        Console.Clear();
                        DataService.AggiungiProdotto();
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        DataService.EliminaProdotto();
                        Console.Clear();
                        break;
                    case 4:
                        Console.Clear();
                        DataService.ModificaProdotto();
                        Console.Clear();
                        break;
                    case 5:
                        Console.Clear();
                        Report.VisualizzaReport();
                        Console.Clear();
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine(new String('=', 100));
                        Console.WriteLine("");
                        Console.WriteLine("Grazie per aver usufruito dei nostri servizi!!!");
                        break;
                    default:
                        Console.WriteLine("La scelta è sbagliata... Riprova");
                        break;

                }
            } while (scelta != 0);
        }
    }
}