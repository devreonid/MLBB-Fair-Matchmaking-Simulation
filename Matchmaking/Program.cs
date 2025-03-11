using System.Threading;
using System.Collections.Generic;
using Matchmaking;

namespace Matchmaking_Scheme;

public static class Program
{
    private static Random random = new Random();
    
    static void Fetching(List<Player> listObject)
    {
        Dictionary<int, List<Player>> angkaSamaMap = new Dictionary<int, List<Player>>();

        // Mengelompokkan objek-objek dengan nilai yang sama
        foreach (var imObject in listObject)
        {
            int nilai = imObject.identificator;
            
            if (!angkaSamaMap.ContainsKey(nilai)) //Jika key nilai belum ditambahkan ke dictionary sebelumnya
            {
                angkaSamaMap.Add(nilai, new List<Player>()); //Maka buatkan keynya berisi value list kosong
            }

            angkaSamaMap[nilai].Add(imObject); //Mengisi value dengan imObject yang memiliki nilai identificator yang sama
 
            //DEBUG
            //Console.WriteLine("imObject Identificator: ");
            //Console.WriteLine(imObject.identificator);
            //Console.WriteLine();
        }

        List<Player> listObjekWakil = new List<Player>();

        // Memilih satu wakil dari setiap objek dengan angka identificator yang sama secara acak
        foreach (var pair in angkaSamaMap)
        {
            int indexWakil = random.Next(pair.Value.Count); // Misal muncul 6 kali berarti 1 sampai 6
            Player objekWakil = pair.Value[indexWakil]; // pair.Value[hasilRandom dari 1 sampai 6]
            listObjekWakil.Add(objekWakil);

            //DEBUG
            //Console.WriteLine("Pair Key: " + pair.Key); //Key = nilainya
            //Console.WriteLine("Pair value: " + pair.Value.Count); //Value.Count = berapa kali muncul nilainya
            //Console.WriteLine(indexWakil);
        }

        if (listObjekWakil.Count < 5) //Mengecek apakah teradapat 5 role yang saling melengkapi
        {
            int sumLOW = 0;
            foreach (var playerx in listObjekWakil)
            {
                sumLOW += playerx.identificator; // Total 1(Midlaner) + 2(Explaner) + 3(Roamer) + 4(Jungler) + 5(Goldlaner) = 15
            }
            
            if (sumLOW == 10) // Jika 10 maka kurang 5 (Goldlaner)
            {
                Console.Clear();
                Console.WriteLine("Tidak ditemukan player Goldlaner");
                Restart();
            }

            else if (sumLOW == 11) // Jika 11 maka kurang 4 (Jungler)
            {
                Console.Clear();
                Console.WriteLine("Tidak ditemukan player Explaner");
                Restart();
            }

            else if (sumLOW == 12) // Jika 12 maka kurang 3 (Roamer)
            {
                Console.Clear();
                Console.WriteLine("Tidak ditemukan player Jungler");
                Restart();
            }

            else if (sumLOW == 13) // Jika 13 maka kurang 2 (Explaner)
            {
                Console.Clear();
                Console.WriteLine("Tidak ditemukan player Roamer");
                Restart();
            }

            else if (sumLOW == 14) // Jika 14 maka kurang 1 (Midlaner)
            {
                Console.Clear();
                Console.WriteLine("Tidak ditemukan player Midlaner");
                Restart();
            }

            else // Fail-safe
            {
                Console.Clear();
                Console.WriteLine("Tidak ditemukan role yang saling melengkapi");
                Restart();
            }

        }

        int slotCounter = 1;
        foreach (var player in listObjekWakil)
        {
            // Mengakses properti identificator dari objek Player
            Console.WriteLine("Player - {0} (Slot {1})", player.order, slotCounter);
            Console.WriteLine("Debug Player Identificator: {0}", player.identificator);

            if (player.identificator == 1)
            {
                Console.WriteLine("Midlaner {0} Match", player.theHighest);
            }

            else if (player.identificator == 2)
            {
                Console.WriteLine("Explaner {0} Match", player.theHighest);
            }

            else if (player.identificator == 3)
            {
                Console.WriteLine("Roamer {0} Match", player.theHighest);
            }

            else if (player.identificator == 4)
            {
                Console.WriteLine("Jungler {0} Match", player.theHighest);
            }

            else if (player.identificator == 5)
            {
                Console.WriteLine("Goldlaner {0} Match", player.theHighest);
            }


            slotCounter++;
            Console.WriteLine();
        }

        int sisa = listObject.Count - listObjekWakil.Count;
        Console.WriteLine("Sisa player yang belum mendapatkan pasangan dan akan melanjutkan matchmaking: " + sisa);
    }

    static void Start()
    {
        Console.WriteLine("Start Matchmaking?");
        Console.WriteLine("Press enter to start.");

        Console.ReadLine();
        Console.Clear();

        Console.WriteLine("Searching for online players...");
        Console.WriteLine("Please Wait.");

        // Membuat list untuk object Player
        List<Player> objects = new List<Player>();
        int ready = random.Next(15, 30);
        int sleep;
        if (ready < 20)
        {
            sleep = random.Next(4000, 5000);
        }
        else
        {
            sleep = random.Next(2000, 4000);
        }

        // Mengisi list Player dengan random. Minimal 15 dan Maksimal 20 objek.
        for (int i = 0; i < ready; i++)
        {
            //Console.WriteLine($"Player {i + 1} :"); //DEBUG
            objects.Add(new Player());
            objects[i].order = i + 1; //Agar bisa disusun dan diketahui urutan playernya
        }

        Thread.Sleep(sleep);

        Console.Clear();
        Console.WriteLine("Found {0} players ready for matchmaking", ready);
        Console.WriteLine("Fetching players data...", ready);

        Thread.Sleep(sleep);

        Console.Clear();

        Fetching(objects);

        Console.WriteLine();
    }

    public static void Restart()
    {
        string input;
        do
        {
            Console.WriteLine("Restart matchmaking? [y] / [n]");
            input = Console.ReadLine();
            input.ToLower();

            if (input == "y")
            {
                Console.Clear();
                Start();
                Restart();
            }

            else if (input == "n")
            {
                Environment.Exit(0);
            }

        } while (input != "y" && input != "n");
    }

    public static void Main()
    {

        Console.Title = "Matchmaking Simulation";
        Start();
        Restart();

        Console.WriteLine();
    }  
}