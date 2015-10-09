using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Theater_Wednesday
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("\n\n\n\nEnter \"1\" to Check out a movie.\n\nEnter \"2\" to Check in a movie.\n\nEnter \"3\" to see movies checked out.\n\nEnter \"4\" to see rental status of all renters.\n\nEnter \"5\" to see ONLY renters with late rental status.");
            int Decision = int.Parse(Console.ReadLine());
            if (Decision==1)
            {
                Checkout();//Runs method Checkout
                Main();
            }
            else
            {
                if (Decision == 2)
                {
                    Console.WriteLine("\nThe Check in process isn't ready yet.\nFeel free to keep your rental :)");
                    Main();
                }
                else
                {
                    if (Decision == 3)
                    {
                        MoviesNotReturned();//Runs method MoviesNotReturned
                        Main();
                    }
                    else
                    {
                        if (Decision == 4)
                        {
                            CheckMovies();
                            Main();
                        }
                        else
                        {
                            if (Decision == 5)
                            {
                                CheckOnlyLateMovies();
                                Main();
                            }
                        }
                    }

                    {
                        Console.WriteLine("Invalid Entry");
                        Main();
                    }
                }
            }
        }
        
        static void Checkout()
        {
            

            Console.WriteLine("\nWhat is the renter's name?");
            string name = Console.ReadLine();
            Console.WriteLine("\nWhat is the renters phone number or e-mail address?");
            string phoneNum = Console.ReadLine();
            Console.WriteLine("\nWhat movie will be rented?");
            string movie = Console.ReadLine();
            
            Name(name,movie,phoneNum);//sends string name and movie to Movie method to write to txt file
            MoviesOut(name, movie, phoneNum);

            RenterList(name);

        }
        static void Checkin()
        {
            Console.WriteLine("Name?");
            string name = Console.ReadLine();
            DeleteInfo(name);
        }
        static void MoviesNotReturned()
        {
            StreamReader Renterreader = new StreamReader("..\\..\\Renterlist.txt");
            int lineNumber = 0;
            string line = Renterreader.ReadLine();
            using (Renterreader)
                while (line != null)
                {
                    lineNumber++;

                    string name = line;
                    StreamReader reader = new StreamReader("..\\..\\" + name + ".txt");
                    int NumberOfLines = 5;
                    string[] ListLines = new string[NumberOfLines];
                    using (reader)
                        for (int i = 0; i < NumberOfLines; i++)
                        {
                            ListLines[i] = reader.ReadLine();
                            Console.WriteLine(ListLines[4]);
                        }
                    line = Renterreader.ReadLine();

                }
            Console.WriteLine();
        }
        static void MoviesOut(string name, string movie, string phoneNum)
        {
            string path = "..\\..\\" + name + ".txt";

            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine("The movie " + movie + " was checked out by " + name + " on " + DateTime.Now);
            }
        }

        static void Name(string name, string movie, string phoneNum)
        {
            StreamWriter writer = new StreamWriter("..\\..\\"+name+".txt");
            using (writer)
            {
                writer.WriteLine(name);
                writer.WriteLine(phoneNum);
                writer.WriteLine(movie);
                writer.WriteLine(DateTime.Now);

            }
        }
        
        static void DeleteInfo(string name)
        {
            StreamReader reader = new StreamReader("..\\..\\" + name + ".txt");
            int NumberOfLines = 3;
            string[] ListLines = new string[NumberOfLines];
            using (reader)
            for (int i = 0; i < NumberOfLines; i++)
            {
                ListLines[i] = reader.ReadLine();
            }
            Console.WriteLine("\nYou have "+ListLines[2]+" checked out.\n\nEnter \"Y\" to return " + ListLines[2]);
            string returnMovie = Console.ReadLine();
            if (returnMovie=="Y")
            {
                File.Delete("..\\..\\" + name + ".txt");

                
            }
            else
            {
                Console.WriteLine("\nInvalid Entry");
            }
        }
        static void CheckMovies()
        {
            StreamReader Renterreader = new StreamReader("..\\..\\Renterlist.txt");
            int lineNumber = 0;
            string line = Renterreader.ReadLine();
            using (Renterreader)
            while (line != null)
            {
                lineNumber++;
                
                string name = line;
                StreamReader reader = new StreamReader("..\\..\\" + name + ".txt");
                int NumberOfLines = 4;
                string[] ListLines = new string[NumberOfLines];
                using (reader)
                    for (int i = 0; i < NumberOfLines; i++)
                    {
                        ListLines[i] = reader.ReadLine();
                    }
                string date = ListLines[3];
                DateTime incdata = Convert.ToDateTime(date);
                bool late = true;
                System.DateTime time = incdata.AddHours(12);
                late = (time < DateTime.Now);
                if (late)
                {
                    Console.WriteLine("\n\nBOOM! " + name + "'s rental is late and $5.00 has been added to " + name + "'s rental fee\n\nYou can contact " + name + " at " + ListLines[1]);
                }
                else
                {
                    Console.WriteLine("\n\n" + name + "'s rental is not late and is due on " + time);
                }
                line = Renterreader.ReadLine();
            }
            

            
        }
        static void RenterList(string name)
        {
            string path = "..\\..\\Renterlist.txt";

            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine(name);
            }
            
        }
        static void CheckOnlyLateMovies()
        {
            StreamReader Renterreader = new StreamReader("..\\..\\Renterlist.txt");
            int lineNumber = 0;
            string line = Renterreader.ReadLine();
            using(Renterreader)
            while (line != null)
            {
                lineNumber++;

                string name = line;
                StreamReader reader = new StreamReader("..\\..\\" + name + ".txt");
                int NumberOfLines = 4;
                string[] ListLines = new string[NumberOfLines];
                using (reader)
                    for (int i = 0; i < NumberOfLines; i++)
                    {
                        ListLines[i] = reader.ReadLine();
                    }
                string date = ListLines[3];
                DateTime incdata = Convert.ToDateTime(date);
                bool late = true;
                System.DateTime time = incdata.AddHours(12);
                late = (time < DateTime.Now);
                if (late)
                {
                    Console.WriteLine("\n\nBOOM! " + name + "'s rental is late and $5.00 has been added to " + name + "'s rental fee\n\nYou can contact " + name + " at " + ListLines[1]);
                }
                else
                {
                    Console.WriteLine();
                }
                line = Renterreader.ReadLine();
            }
            

        }
    }
}