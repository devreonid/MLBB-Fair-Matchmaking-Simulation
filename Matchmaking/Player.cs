using System;
using System.Collections.Generic;
namespace Matchmaking
{
    internal class Player
    {
        public int identificator { get; set; }
        public int theHighest { get; set; }
        public int order { get; set; }

        public Player()
        {
            Highest();
        }

        static Random random = new Random();

        //Maksimal match terakhir yang dapat direkam oleh sistem MLBB paling banyak adalah 50
        int mid = random.Next(1, 50);
        int exp = random.Next(1, 50);
        int roam = random.Next(1, 50);
        int jungle = random.Next(1, 50);
        int gold = random.Next(1, 50);

        // Midlane = 1
        // Explane = 2
        // Roam = 3
        // Jungle = 4
        // Goldlane = 5 

        public int Highest()
        {
            theHighest = (((mid > exp) ? mid : exp) > ((roam > jungle) ? roam : jungle)) ? ((mid > exp) ? mid : exp) : ((roam > jungle) ? roam : jungle);
            theHighest = (theHighest > gold) ? theHighest : gold;

            //Console.WriteLine(theHighest); //DEBUG

            if (theHighest == mid)
                return identificator = 1;

            else if (theHighest == exp)
                return identificator = 2;

            else if (theHighest == roam)
                return identificator = 3;

            else if (theHighest == jungle)
                return identificator = 4;

            else if (theHighest == gold)
                return identificator = 5;

            return theHighest;
        }
    }
}
