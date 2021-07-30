using System;
using System.Data;
using System.Data.SqlClient;

namespace Magazzino
{
    public class Report
    {
        static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Magazzino;Trusted_Connection=True;";
        public static void VisualizzaReport()
        {
            Console.WriteLine("-------REPORTS-------");
            int scelta = 0;
            do
            {

                Console.WriteLine("\nPremi");
                Console.WriteLine();
                Console.WriteLine("[1]  Elenco prodotti con Giacenza limitata (<10)");
                Console.WriteLine("[2]  Numero di prodotti per categoria");
                Console.WriteLine("[0]  Per tornare al Menù principale");


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
                        VisualizzaProdottiGiacenza();

                        break;
                    case 2:
                        Console.Clear();
                        ProdottiPerCategoria();
                        
                        break;
                    case 0:
                        Console.Clear();
                        Menù.Start();
                        break;

                }
            } while (scelta != 0);
        }

        public static void VisualizzaProdottiGiacenza()
        {
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                if (conn.State != ConnectionState.Open)
                    Console.WriteLine("Can't establish connection!!!");
                SqlCommand leggi = new("SELECT * FROM Prodotti WHERE QuantitaDisponibile < 10", conn);
                leggi.CommandType = System.Data.CommandType.Text;

                SqlDataReader reader = leggi.ExecuteReader();
                Console.WriteLine();
                Console.WriteLine("{0,5}{1,20}{2,20}{3,20}{4,20}{5,30}", "ID", "Codice Prodotto", "Categoria", "Descrizione", "Prezzo", "Quantità Disponibile");
                Console.WriteLine(new String('-', 114));

                while (reader.Read())
                {

                    Console.WriteLine("{0,5}{1,20}{2,20}{3,20}{4,20}{5,30}",
                    reader["ID"],
                    reader["CodiceProdotto"],
                    reader["Categoria"],
                    reader["Descrizione"],
                    reader["PrezzoUnitario"],
                    reader["QuantitaDisponibile"]
                    );
                }

                Console.WriteLine(new String('-', 114));
                Console.WriteLine();

                conn.Close();

            }
        }

        public static void ProdottiPerCategoria()
        {
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                if (conn.State != ConnectionState.Open)
                    Console.WriteLine("Can't establish connection!!!");
                SqlCommand leggi = new("SELECT Categoria, COUNT(*) AS [# Prodotti] FROM Prodotti "
                                        +" GROUP BY Categoria ", conn);
                leggi.CommandType = System.Data.CommandType.Text;

                SqlDataReader reader = leggi.ExecuteReader();
                Console.WriteLine();
                Console.WriteLine("{0,20}{1,20}", "Categoria", "# Prodotti");
                Console.WriteLine(new String('-', 114));

                while (reader.Read())
                {

                    Console.WriteLine("{0,20}{1,20}",
                    reader["Categoria"],
                    reader["# Prodotti"]
                    );
                }

                Console.WriteLine(new String('-', 114));
                Console.WriteLine();

                conn.Close();

            }
        }
    }
}