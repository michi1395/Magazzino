using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using static Magazzino.Prodotto;

namespace Magazzino
{
    public class DataService
    {
        static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Magazzino;Trusted_Connection=True;";
        public static void VisualizzaProdotti()
        {
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                if (conn.State != ConnectionState.Open)
                    Console.WriteLine("Can't establish connection!!!");
                SqlCommand leggi = new("SELECT * FROM Prodotti", conn);
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

        public static void AggiungiProdotto()
        {
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                if (conn.State != ConnectionState.Open)
                    Console.WriteLine("Can't establish connection!!!");

                Console.WriteLine("---INSERISCI NUOVO PRODOTTO---");

                Prodotto prodotto = new();
                
                prodotto.CodiceProdotto=ControlloCodiceProdotto();

                Console.WriteLine("Categoria: ");
                prodotto.Categoria= InserisciCategoriaProdotto();
                string myCat = Enum.GetName(typeof(Tipo), prodotto.Categoria);
                Console.Write("Descrizione: ");
                prodotto.Descrizione = Console.ReadLine();
                Console.Write("Prezzo: ");
                prodotto.PrezzoUnitario = CheckPrezzo();
                Console.Write("Quantità disponibile: ");
                prodotto.QuantitaDisponibile = CheckProd();
                

                SqlCommand insert = conn.CreateCommand();
                string insertCommand = "INSERT INTO Prodotti VALUES(@codiceProdotto, @categoria, " +
                     " @descrizione, @prezzo, @quantita)";
                insert.CommandText = insertCommand;
                insert.CommandType = System.Data.CommandType.Text;

                insert.Parameters.AddWithValue("@codiceProdotto", prodotto.CodiceProdotto);
                insert.Parameters.AddWithValue("@categoria", myCat);
                insert.Parameters.AddWithValue("@descrizione", prodotto.Descrizione);
                insert.Parameters.AddWithValue("@prezzo", prodotto.PrezzoUnitario);
                insert.Parameters.AddWithValue("@quantita", prodotto.QuantitaDisponibile);

                int result = insert.ExecuteNonQuery();

                if (result != 1)
                    Console.WriteLine("Errore di inserimento dati.");

                Console.WriteLine(new String('-', 110));
                Console.WriteLine();
                Console.WriteLine("> Premi un tasto per tornare al menù principale");
                Console.ReadLine();

                conn.Close();
            }
        }

        public static string ControlloCodiceProdotto()
        {
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                if (conn.State != ConnectionState.Open)
                    Console.WriteLine("Can't establish connection!!!");

                SqlCommand leggi = new("SELECT * FROM Prodotti", conn);
                leggi.CommandType = System.Data.CommandType.Text;

                SqlDataReader reader = leggi.ExecuteReader();
                Console.Write("Codice Prodotto: ");
                string codiceProdotto = Console.ReadLine();
                List<string> codiciTab = new();

                while (reader.Read())
                {
                    codiciTab.Add((string)reader["CodiceProdotto"]);
                }


                foreach (string codice in codiciTab)
                    if (codiceProdotto == codice)
                    {
                        Console.WriteLine("Non puoi inserire questo Codice Prodotto");
                        Console.Write("Codice Prodotto: ");
                        codiceProdotto = Console.ReadLine();
                    }
                conn.Close();
                return codiceProdotto;
            }

            
        }

        public static void EliminaProdotto()
        {
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                if (conn.State != ConnectionState.Open)
                    Console.WriteLine("Can't establish connection!!!");


                Console.WriteLine("---RIMUOVI PRODOTTO---");

                VisualizzaProdotti();
                Console.WriteLine("Inserisci l'ID del prodotto da eliminare..");
                Console.WriteLine();
                string id = Console.ReadLine();

                SqlCommand delete = conn.CreateCommand();
                string deleteCommand = "DELETE FROM Prodotti WHERE ID = @id";
                delete.CommandText = deleteCommand;
                delete.CommandType = System.Data.CommandType.Text;

                int.TryParse(id, out int idVal);

                delete.Parameters.AddWithValue("@id", idVal);

                int result = delete.ExecuteNonQuery();

                if (result != 1)
                    Console.WriteLine("Errore di inserimento dati.");


                Console.WriteLine(new String('-', 110));
                Console.WriteLine();
                Console.WriteLine("> Premi un tasto per tornare al menù principale");
                Console.ReadLine();
                conn.Close();


            }
        }

        public static void ModificaProdotto()
        {
            using (SqlConnection conn = new(connectionString))
            {
                conn.Open();
                if (conn.State != ConnectionState.Open)
                    Console.WriteLine("Can't establish connection!!!");

                Console.WriteLine("---MODIFICA Prodotto---");
                VisualizzaProdotti();
                Console.WriteLine("Inserisci l'ID dell'abbonamento da modificare..");
                Console.WriteLine();
                Prodotto prodotto = new();
                string id = Console.ReadLine();

                prodotto.CodiceProdotto=ControlloCodiceProdotto();
                Console.WriteLine("Categoria: ");
                prodotto.Categoria = InserisciCategoriaProdotto();
                string myCat = Enum.GetName(typeof(Tipo), prodotto.Categoria);

                Console.Write("Descrizione: ");
                prodotto.Descrizione = Console.ReadLine();
                Console.Write("Prezzo: ");
                prodotto.PrezzoUnitario = CheckPrezzo();
                Console.Write("Quantità disponibile: ");
                prodotto.QuantitaDisponibile = CheckProd();

                SqlCommand update = conn.CreateCommand();
                string updateCommand = "UPDATE Prodotti SET CodiceProdotto=@codiceProdotto, Categoria=@categoria, " +
                     " Descrizione=@descrizione, PrezzoUnitario=@prezzo, QuantitaDisponibile=@quantita WHERE ID = @id";
                update.CommandText = updateCommand;
                update.CommandType = System.Data.CommandType.Text;

                int.TryParse(id, out int idVal);

                update.Parameters.AddWithValue("@id", idVal);
                update.Parameters.AddWithValue("@codiceProdotto", prodotto.CodiceProdotto);
                update.Parameters.AddWithValue("@categoria", myCat);
                update.Parameters.AddWithValue("@descrizione", prodotto.Descrizione);
                update.Parameters.AddWithValue("@prezzo", prodotto.PrezzoUnitario);
                update.Parameters.AddWithValue("@quantita", prodotto.QuantitaDisponibile);

                int result = update.ExecuteNonQuery();

                if (result != 1)
                    Console.WriteLine("Errore di inserimento dati.");


                Console.WriteLine(new String('-', 110));
                Console.WriteLine();
                Console.WriteLine("> Premi un tasto per tornare al menù principale");
                Console.ReadLine();
                conn.Close();
            }
        }

        public static Tipo InserisciCategoriaProdotto()
        {

            Console.WriteLine($"Premi {(int)Tipo.Alimentari} per creare un prodotto di categoria {Tipo.Alimentari}");
            Console.WriteLine($"Premi {(int)Tipo.Cancelleria} per creare un prodotto di categoria {Tipo.Cancelleria}");
            Console.WriteLine($"Premi {(int)Tipo.Sanitari} per creare un prodotto di categoria {Tipo.Sanitari}");
            Console.WriteLine($"Premi {(int)Tipo.Elettronica} per creare un prodotto di categoria {Tipo.Elettronica}");
            int tipo = Check();
            return (Tipo)tipo;

        }

        public static int Check()
        {
            int num = 0;
            while (!int.TryParse(Console.ReadLine(), out num) || num < 0 || num > 3)
            {
                Console.WriteLine("Puoi inserire solo 0, 1, 2 o 3! Riprova");
                Console.WriteLine(">> ");
            }

            return num;

        }

        public static decimal CheckPrezzo()
        {
            decimal num = 0;
            while (!decimal.TryParse(Console.ReadLine(), out num) || num < 0)
            {
                Console.WriteLine("Puoi inserire solo Valori positivi! Riprova");
                Console.WriteLine(">> ");
            }

            return num;

        }

        public static int CheckProd()
        {
            int num = 0;
            while (!int.TryParse(Console.ReadLine(), out num) || num < 0)
            {
                Console.WriteLine("Puoi inserire solo numeri interi! Riprova");
                Console.WriteLine(">> ");
            }

            return num;
        }

    }
}