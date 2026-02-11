using System.Reflection.Metadata.Ecma335;

namespace Feladat
{
    internal class Program
    {
        static int Szamol(int op1, char muvJel, int op2)
        {
            int ertek = 0;
            if (muvJel == '+')
            {
                ertek = op1 + op2;
            }
            else if (muvJel == '-')
            {
                ertek = op1 - op2;
            }
            else if (muvJel == '*')
            {
                ertek = op1 * op2;
            }
            else if (muvJel == '%')
            {
                ertek = op1 % op2;
            }
            else
            {
                ertek = op1 / op2;
            }
            return ertek;
        }
        static void Main(string[] args)
        {
            //1. feladat
            List<string> muveletekSorai = new List<string>();
            bool vanIlyenFajl = false;
            StreamReader sr;
            while (!vanIlyenFajl)
            {
                try
                {
                    sr = new StreamReader(Console.ReadLine());
                    sr.Close();
                    vanIlyenFajl = true;

                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Ez a fajl nem letezik!");
                }
            }

            sr = new StreamReader(Console.ReadLine());
            while (!sr.EndOfStream)
            {
                muveletekSorai.Add(sr.ReadLine());
            }
            sr.Close();

            //2. feladat
            List<string[]> feldaraboltLista = new List<string[]>();
            foreach (var item in muveletekSorai)
            {
                feldaraboltLista.Add(item.Split(' '));
            }
            List<int> eredemenyek = new List<int>();
            int szamlalo = 0;
            int szamoltMegoldas;
            while (szamlalo != feldaraboltLista.Count)
            {
                szamoltMegoldas = Szamol(Convert.ToInt32(feldaraboltLista[szamlalo][0]), Convert.ToChar(feldaraboltLista[szamlalo][1]), Convert.ToInt32(feldaraboltLista[szamlalo][2]));
                eredemenyek.Add(szamoltMegoldas);
                szamlalo++;
            }
            //3. feladat
            szamlalo = 0;
            StreamWriter sw = new StreamWriter("Eredmények.txt");
            while (szamlalo != eredemenyek.Count)
            {
                sw.WriteLine($"{feldaraboltLista[szamlalo][0]} {feldaraboltLista[szamlalo][1]} {feldaraboltLista[szamlalo][2]} = {eredemenyek[szamlalo]}");
                szamlalo++;
            }
            sw.Close();
            //4. feladat
            int osszeadasokSzama = 0;
            szamlalo = 0;
            int legnagyobbSzam = Convert.ToInt32(feldaraboltLista[0][0]);
            while (szamlalo != feldaraboltLista.Count)
            {
                if (Convert.ToChar(feldaraboltLista[szamlalo][1]) == '+')
                {
                    osszeadasokSzama++;
                }
                if (Convert.ToInt32(feldaraboltLista[szamlalo][0]) > legnagyobbSzam)
                {
                    legnagyobbSzam = Convert.ToInt32(feldaraboltLista[szamlalo][0]);
                }
                if (Convert.ToInt32(feldaraboltLista[szamlalo][2]) > legnagyobbSzam)
                {
                    legnagyobbSzam = Convert.ToInt32(feldaraboltLista[szamlalo][2]);
                }
                szamlalo++;
            }
            Console.WriteLine(legnagyobbSzam);
            Console.WriteLine(osszeadasokSzama);
        }
    }
}

