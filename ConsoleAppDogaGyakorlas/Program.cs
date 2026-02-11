using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleAppDogaGyakorlas
{
    class Muvelet
    {
        //A private és public szócskák a láthatóságot szabályozzák.
        //A private mezők csak az osztályon belül érhetők el, míg a public tulajdonságokon keresztül kívülről is hozzáférhetünk az értékekhez.
        private int op1, op2;
        private char muveletiJel;

        // Lehetőség van a művelet eredményét is tárolni, hogy ne kelljen minden alkalommal újraszámolni, amikor lekérdezzük
        private int eredmeny;

        // A tulajdonságok segítségével ellenőrzött hozzáférést biztosíthatunk a mezőkhöz, például csak olvashatóvá tehetjük őket.
        public char MuveletiJel { get => muveletiJel; }
        // Az előbbinek egy másik fajta írásmódja, de ugyanazt a célt szolgálja
        public int Op1
        {
            get
            {
                return op1;
            }
        }
        public int Op2 { get => op2; }
        public int Eredmeny { get => eredmeny; }

        // Konstruktorok segítségével könnyen létrehozhatunk új példányokat az osztályból, és megadhatjuk a kezdeti értékeket.
        // A konstruktor létrehozásának szabályai: 
        //1. Az osztály nevével megegyező nevű metódus,
        //2. Nem rendelkezik visszatérési típussal, és nem lehet void sem,
        //3. Opcionálisan paramétereket fogadhat, amelyek segítségével inicializálhatjuk a mezőket.
        public Muvelet(int operandus1, char muvJel, int operandus2)
        {
            op1 = operandus1;
            op2 = operandus2;
            muveletiJel = muvJel;
        }

        // Ez a konstruktor egy string bemenetet vár, amelyet feldolgozva inicializálja a mezőket. Ez hasznos lehet, ha a műveleteket egy fájlból olvassuk be.
        // Akármennyi konstruktort létrehozhatunk, amennyire szükségünk van. Fontos viszont, hogy a konstruktorok eltérő típusú és különböző paramétereket fogadhatnak, így rugalmasan használhatjuk az osztályt különböző helyzetekben.
        public Muvelet(string sor)
        {
            // A Split metódus segítségével a bemeneti stringet szóközök mentén daraboljuk fel egy tömbbe, így könnyen hozzáférhetünk az operandusokhoz és a műveleti jelhez.
            // ha nem adunk meg argumentumot, akkor a Split mindenféle whitespace karakter mentén darabol, így több szóköz esetén sem lesz gond
            string[] mezok = sor.Split();
            op1 = int.Parse(mezok[0]);  // Az első mező az első operandus, amelyet int típusra konvertálunk a Parse metódussal.
            muveletiJel = mezok[1][0];  // A második mező a műveleti jel, amely egy karakter, így a mező első karakterét vesszük (ez azért fontos, mert a mező egy string, de nekünk csak egy karakterre van szükségünk).
            op2 = int.Parse(mezok[2]);

            // A művelet eredményét kiszámoljuk a Szamol statikus metódus segítségével, amely a két operandus és a műveleti jel alapján végzi el a megfelelő műveletet.
            eredmeny = Muvelet.Szamol(op1, muveletiJel, op2);
        }

        // Lehet metódussal is előállítani a kimenő fájlba kerülő sort.
        public string GetMuveletSora()
        {
            return $"{op1} {muveletiJel} {op2} = {eredmeny}";
        }

        //Lehet másként is megoldani, például egy property segítségével, amely csak olvasható és visszaadja a műveleti sort.
        public string MuveletSora { get => $"{op1} {muveletiJel} {op2} = {eredmeny}"; }

        /// <summary>
        /// Kiszámolja a megadott operandusok és műveleti jel alapján a művelet eredményét.
        /// </summary>
        /// <param name="numA">Első operandus</param>
        /// <param name="op">Műveleti jel</param>
        /// <param name="numB">Második operandus</param>
        /// <returns>Az eredmény</returns>
        /// 

        // A statikus metódus azt jelenti, hogy a metódus az osztályhoz tartozik, és nem egy konkrét példányhoz. Ez azt jelenti, hogy a Szamol metódust közvetlenül az osztály nevével hívhatjuk meg, anélkül, hogy létre kellene hoznunk egy Muvelet objektumot. Ez hasznos lehet olyan műveletek esetén, amelyek nem igényelnek állapotot vagy példányváltozókat, hanem csak bemeneti paraméterek alapján számolnak ki egy eredményt.
        public static int Szamol(int numA, char op, int numB)
        {
            // Hibavizsgálat nincs és feltételezzük, hogy a bemenet helyes, tehát a műveleti jel csak a megadott karakterek közül lehet, és a második operandus nem lehet nulla az osztás esetén.
            int eredmeny = 0;

            switch (op)
            {
                case '+':
                    eredmeny = numA + numB;
                    break;
                case '-':
                    eredmeny = numA - numB;
                    break;
                case '/':
                    eredmeny = numA / numB;
                    break;
                case '*':
                    eredmeny = numA * numB;
                    break;
                case '%':
                    eredmeny = numA % numB;
                    break;
            }

            return eredmeny;
        }


    }

    public class Program
    {
        static void Main(string[] args)
        {
            // A Muvelet osztály használatának bemutatása egyszerű példákon keresztül. Először a statikus Szamol metódust használjuk közvetlenül, majd létrehozunk egy Muvelet objektumot a konstruktor segítségével, és lekérdezzük az eredményt és a műveleti sort a tulajdonságokon keresztül.

            Console.WriteLine(Muvelet.Szamol(33, '*', 345));

            Muvelet elsoPelda = new Muvelet(12, '*', 456);
            Console.WriteLine(elsoPelda.GetMuveletSora());

            Muvelet masodikPelda = new Muvelet("34 + 56");
            Console.WriteLine(masodikPelda.MuveletSora);

            //Feladatsor megoldása

            //1. feladat

            // A GetLetezoFajl metódus segítségével bekérjük a felhasználótól a fájl nevét, és addig ismételjük a kérést, amíg egy létező fájlt nem ad meg. Ez biztosítja, hogy a további műveletek során ne kelljen hibakezeléssel foglalkozni a fájl megnyitásakor.
            String fileNev = GetLetezoFajl();

            // * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * * 

            //2.feladat
            // A BetoltesFajlbol metódus segítségével beolvassuk a megadott fájlból a műveleteket, és egy List<Muvelet> típusú listában tároljuk őket. Ez lehetővé teszi, hogy könnyen hozzáférjünk a műveletek adataihoz és eredményeihez a további feldolgozás során.
            List<Muvelet> muveletek = BetoltesFajlbol(fileNev);

            // * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * * 

            //3. feladat
            //Hozzon létre egy eredmeny.txt fájlt, amelybe a beolvasott állomány alapján az
            //eredményeket is tartalmazó sorok kerülnek be.
            //Minden sor formátuma: operandus operátor operandus = eredmény

            FajlbaIras("eredmeny.txt", muveletek);

            // * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * * 

            //4. feladat
            //Határozza meg:
            //a) Hány összeadás volt?
            //b) Mi volt a legnagyobb szám az operandusok között?

            int osszeadasokSzama = 0;

            foreach (Muvelet muv in muveletek)
            {
                if (muv.MuveletiJel == '+')
                {
                    osszeadasokSzama++;
                }
            }
            Console.WriteLine($"4. feladat a): osszeadasok szama:{osszeadasokSzama}");

            //LINQ segítségével:
            Console.WriteLine($"4. feladat a): osszeadasok szama:{muveletek.Count(x => x.MuveletiJel == '+')}");

            int legnagyobbOperandus = int.MinValue;
            foreach (Muvelet muv in muveletek)
            {
                if (muv.Op1 > legnagyobbOperandus)
                {
                    legnagyobbOperandus = muv.Op1;
                }
                if (muv.Op2 > legnagyobbOperandus)
                {
                    legnagyobbOperandus = muv.Op2;
                }
            }

            Console.WriteLine($"4. feladat b): legnagyobb operandus:{legnagyobbOperandus}");

            //LINQ segítségével:
            Console.WriteLine($"4. feladat b): legnagyobb operandus:{muveletek.Max(x => Math.Max(x.Op1, x.Op2))}");

            // * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * * 


            //5.FELADAT – Rekurzív megoldás
            //Írjon rekurzív függvényt, amely:
            //  Összeadja az összes kiszámolt eredményt
            //  Nem használhat ciklust!
            //Az alábbi két metódusfejrészből választhat:
            //static int OsszegRekurziv(int[] eredmenyek, int index)
            //static int OsszegRekurziv(List<int> eredmenyek)

            List<int> muveletekEredmenye = new List<int>();
            Console.WriteLine($"5. feladat: {OsszegRekurziv(muveletekEredmenye)}");

            // * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * *  * * * * * 

            //6.FELADAT – Véletlen inputfájl generálása
            //Írjon programrészt, amely:
            //  Bekér egy számot(sorok száma);
            //  Bekér egy állománynevet;
            //  Létrehoz egy új txt fájlt;
            //  Véletlenszerű operandusokat és operátorokat generál;
            //  A formátum azonos legyen az alapfeladattal!
            //  A műveletnek végrehajthatónak kell lennie!
            Console.Write("Kérem a sorok számát:");
            int sorokSzama = int.Parse(Console.ReadLine());
            Console.Write("Kérem az állomány nevét:");
            string fajlnev = Console.ReadLine();
            RandomMuveletekFajlba(fajlnev, sorokSzama);
        }

        private static void FajlbaIras(string fajlnev, List<Muvelet> muveletek)
        {
            StreamWriter sw = new StreamWriter(fajlnev);
            foreach (Muvelet muv in muveletek)
            {
                sw.WriteLine(muv.MuveletSora);
            }
            sw.Close();

            //LINQ esetén:
            //File.WriteAllLines(fajlnev, muveletek.Select(muv => muv.MuveletSora));
        }

        private static string GetLetezoFajl()
        {
            string fajlnev;
            bool fileLetezik = false;
            do
            {
                Console.Write("Kérem a fájl nevét/útvonalát: ");
                fajlnev = Console.ReadLine();
                try
                {
                    StreamReader sr = new StreamReader(fajlnev);
                    sr.Close();
                    fileLetezik = true;
                }
                catch (FileNotFoundException hiba)
                {
                    Console.WriteLine("Nem létezik a megadott fájl!");
                }
                catch (IOException hiba)
                {
                    Console.WriteLine("Hiba történt a fájl megnyitásakor!");
                }
                catch (Exception hiba)
                {
                    Console.WriteLine($"Hiba történt: {hiba.Message}");
                }
            }
            while (!fileLetezik);

            return fajlnev;
        }

        // A rekurzív megoldás során a függvény önmagát hívja meg, amíg el nem éri a bázis esetet, amelyben már csak egy eredmény maradt a listában. Ezután visszaadja ezt az egyetlen értéket, és a visszatérési értékek összeadódnak a rekurzív hívások során, így végül megkapjuk az összes eredmény összegét.
        static int OsszegRekurziv(List<int> eredmenyek)
        {
            if (eredmenyek.Count == 1)  // Bázis eset: ha csak egy eredmény maradt a listában, akkor visszaadjuk ezt az értéket, mert ez már nem igényel további összeadást.
            {
                return eredmenyek[0];
            }
            int elsoEredmeny = eredmenyek[0];  // Az első eredményt eltároljuk egy változóban, hogy később hozzáadhassuk a rekurzív hívás eredményéhez.
            eredmenyek.RemoveAt(0); // Az első eredményt eltávolítjuk a listából, hogy a következő rekurzív hívásban már csak a maradék eredmények legyenek benne. Ez biztosítja, hogy a rekurzív hívások során fokozatosan csökkenjen a lista mérete, amíg el nem érjük a bázis esetet.
            return elsoEredmeny + OsszegRekurziv(eredmenyek);
        }
        }




        static List<Muvelet> BetoltesFajlbol(string fajlnev)
        {
            List<Muvelet> muvLista = new List<Muvelet>();

            // A using használat előnye, hogy automatikusan gondoskodik a StreamReader erőforrás felszabadításáról, még akkor is, ha kivétel történik a fájl olvasása közben. Ez biztosítja, hogy a fájl ne maradjon nyitva, és ne okozzon erőforrás szivárgást.
            using (StreamReader sr = new StreamReader(fajlnev))
            {
                while (!sr.EndOfStream)
                {
                    muvLista.Add(new Muvelet(sr.ReadLine()));
                }
            }

            return muvLista;

            //LINQ esetén egyetlen sorral is megoldható
            //return File.ReadAllLines(fajlnev).Select(sor => new Muvelet(sor)).ToList();
        }

        static void RandomMuveletekFajlba(string fajlnev, int sorokSzama)
        {
            Random rnd = new();
            List<char> operatorok = new() { '+', '-', '*', '/', '%' };

            using (StreamWriter sw = new StreamWriter(fajlnev))
            {
                for (int i = 0; i < sorokSzama; i++)
                {
                    Muvelet muv = new Muvelet(rnd.Next(), operatorok[rnd.Next(5)], rnd.Next());
                    sw.WriteLine($"{muv.Op1} {muv.MuveletiJel} {muv.Op2}");
                }
            }
        }

    }
}
