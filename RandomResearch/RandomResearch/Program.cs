using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomResearch
{
    class Program
    {
        static void Main(string[] args)
        {
            //Basic(args);
            Advanced(args);
            //Console.Read();
        }

        private static void Basic(string[] args)
        {
            // 1
            // Call method that uses class-level Random
            F();
            // 2
            // Call same method
            // The random number sequence still be random
            F();
        }

        static Random _r = new Random();
        static void F()
        {
            // Use class-level Random so that when this
            // ... method is called many times, it still has
            // ... good Randoms.
            int n = _r.Next();
            // If this declared a local Random, it would
            // ... repeat itself.
            Console.WriteLine(n);
        }

        private static void Advanced(string[] args)
        {
            int populationLength = int.Parse(args[0]);
            int requiredLength = int.Parse(args[1]);
            //bool makeSureNoRepeatsInARow = args.Length >=3 ? bool.Parse(args[2]) : false;
            int minDuplicatesAbstand = args.Length >= 3 ? int.Parse(args[2]) : 0; //todo
            List<int> rslt = new List<int>();
            Random rnd = new Random();
            int prev = -1;
            //for (int i = 0; i < requiredLength; i++)
            //{
            //    int curr = 0;

            //    do {
            //        curr = rnd.Next(populationLength);
            //    } while (makeSureNoRepeatsInARow && curr == prev);
            //    rslt.Add(curr);
            //    prev = curr;
            //}
            for (int i = 0; i < requiredLength; i++)
            {
                int curr = 0;
                int currAbstand = 0;
                do {
                    curr = rnd.Next(populationLength);
                    currAbstand = rslt.IndexOf(curr, Math.Max(rslt.Count - minDuplicatesAbstand, 0));
                } while (minDuplicatesAbstand > 0 && currAbstand != -1);
                rslt.Add(curr);
                prev = curr;
            }

            

            Dictionary<int, int> grouppedBy = new Dictionary<int, int>();
            foreach (int val in rslt)
            {
                if (grouppedBy.ContainsKey(val))
                    grouppedBy[val]++;
                else
                    grouppedBy.Add(val, 1);
            }

            Console.WriteLine("Raw results generated:");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            for (int i = 0; i < rslt.Count; i++)
                Console.WriteLine("[{0}]: {1}", i, rslt[i]);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Results aggregated by count:");
            foreach(int id in grouppedBy.Keys)
                Console.WriteLine("[{0}]: {1}", id, grouppedBy[id]);
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");

        }

        
    }
}
