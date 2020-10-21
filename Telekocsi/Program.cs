using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telekocsi
{
    class Program
    {
        static List<Auto> Autok = new List<Auto>();
        static List<Igeny> Igenyek = new List<Igeny>();

        static void Beolvas1()
        {
            StreamReader sr = new StreamReader("autok.csv");

            sr.ReadLine();

            while (!sr.EndOfStream)
            {
                Autok.Add(new Auto(sr.ReadLine()));
            }

            sr.Close();

            StreamReader i = new StreamReader("igenyek.csv");

            i.ReadLine();

            while (!i.EndOfStream)
            {
                Igenyek.Add(new Igeny(i.ReadLine()));
            }

        }

        static void Masodik()
        {
            Console.WriteLine(Autok.Count);
        }

        static void Harmadik()
        {
            int ferohelyek = 0;

            foreach (var a in Autok)
            {
                if (a.Indulas == "Budapest" && a.Cel == "Miskolc")
                {
                    ferohelyek = ferohelyek + a.Ferohely;
                }
            }

            Console.WriteLine("3. feladat: {0}", ferohelyek);
        }

        static void Negyedik()
        {
            //List<string> utvonalak = new List<string>();

            //foreach (var item in collection)
            //{

            //}

            string maxNev = "";
            int max = 0;

            foreach (var a in Autok)
            {
                
                int ferohely = 0;

                foreach (var b in Autok)
                {
                    if (b.Indulas == a.Indulas && b.Cel == a.Cel)
                    {
                        ferohely += b.Ferohely;
                    }
                }

                if (max < ferohely)
                {
                    max = ferohely;
                    maxNev = $"{a.Indulas} - {a.Cel}";
                }
            }

            Console.WriteLine($"{maxNev} - {max}");

        }

        static void Otodik()
        {
            foreach (var i in Igenyek)
            {
                foreach (var a in Autok)
                {
                    if (i.Indulas == a.Indulas && i.Cel == a.Cel && i.Szemelyek == a.Ferohely)
                    {
                        Console.WriteLine($"{i.Azonosito} => {a.Rendszam}");
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Beolvas1();            

            Masodik();
            Harmadik();
            Negyedik();
            Otodik();

            Console.ReadLine();
        }
    }
}
