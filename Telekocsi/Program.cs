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
            //Dictionary<string, int> utvonalak = new Dictionary<string, int>();        //Tanár megoldása

            //foreach (var a in Autok)
            //{
            //    if (!utvonalak.ContainsKey(a.Utvonal))
            //    {
            //        utvonalak.Add(a.Utvonal, a.Ferohely);                             //Ha nincs benne, akkor felvesszük
            //    }
            //    else
            //    {
            //        utvonalak[a.Utvonal] = utvonalak[a.Utvonal] + a.Ferohely;         //Ha van benne, akkor hozzáadjuk a férőhelyeket
            //    }
            //}

            int max = 0;
            string utv = "";

            //foreach (var u in utvonalak)
            //{
            //    if (u.Value > max)
            //    {
            //        max = u.Value;
            //        utv = u.Key;
            //    }
            //}

            //Console.WriteLine($"4. feladat: {utv} - {max}");

            //string maxNev = "";                                                       //Saját megoldás
            //int max = 0;

            //foreach (var a in Autok)
            //{

            //    int ferohely = 0;

            //    foreach (var b in Autok)
            //    {
            //        if (b.Indulas == a.Indulas && b.Cel == a.Cel)
            //        {
            //            ferohely += b.Ferohely;
            //        }
            //    }

            //    if (max < ferohely)
            //    {
            //        max = ferohely;
            //        maxNev = $"{a.Indulas} - {a.Cel}";
            //    }
            //}

            //Console.WriteLine($"4. feladat: {maxNev} - {max}");

            var utvonalak = from a in Autok
                            orderby a.Utvonal 
                            group a by a.Utvonal
                            into temp select temp;

            foreach (var ut in utvonalak)
            {
                int fh = ut.Sum(x => x.Ferohely);                       //a képzett csoporton végigmegy elemenként, és a férőhelyeket hozzáadja a Szumhoz
                if (max < fh) 
                {
                    max = fh;
                    utv = ut.Key;
                }
                //Console.WriteLine($"{ut.Key} -> {ut.Key}");
            }
            Console.WriteLine($"4. feladat: {utv} - {max}");
        }

        static void Otodik()
        {
            Console.WriteLine("5. feladat:");

            //foreach (var i in Igenyek)
            //{
            //    foreach (var a in Autok)
            //    {
            //        if (i.Indulas == a.Indulas && i.Cel == a.Cel && i.Szemelyek == a.Ferohely)
            //        {
            //            Console.WriteLine($"\t{i.Azonosito} => {a.Rendszam}");
            //        }
            //    }
            //}

            int db = 0;

            foreach (var i in Igenyek)
            {
                int a = 0;
                while (a < Autok.Count && !(i.Indulas == Autok[a].Indulas &&
                                            i.Cel == Autok[a].Cel &&
                                            i.Szemelyek <= Autok[a].Ferohely))
                {
                    a++;
                }
                if (a < Autok.Count)
                {
                    Console.WriteLine($"{i.Azonosito} => {Autok[a].Rendszam}");
                    db++;
                }
            }
            Console.WriteLine(db);
        }

        static void Hatodik()
        {
            StreamWriter sw = new StreamWriter("utasuzenetek.txt");

            foreach (var i in Igenyek)
            {
                int a = 0;
                while (a < Autok.Count && !(i.Indulas == Autok[a].Indulas &&
                                            i.Cel == Autok[a].Cel &&
                                            i.Szemelyek <= Autok[a].Ferohely))
                {
                    a++;
                }
                if (a < Autok.Count)
                {
                    sw.WriteLine($"{i.Azonosito}: Rendszám: {Autok[a].Rendszam} Telefonszám: {Autok[a].Telefonszam}");
                }
                else
                {
                    sw.WriteLine($"{i.Azonosito}: Sajnos nem sikerült autót találni");
                }
            }

            sw.Close();

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
