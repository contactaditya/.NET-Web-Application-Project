using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using System.Data.Entity;
using DataLayer;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace YATConsole
{
    class Program
    {
        public static void qryTester(UserSort sortby)
        {

            Builder b = new Builder();

            //Test searching users
            List<User> users = b.queryUsers(minAge: 20, maxAge: 30, gender: true, address: "11791", SearcherID: "1", sortBy: sortby);
            foreach (User user in users)
            {
                Console.WriteLine(user.FirstName + " " + user.LastName);
            }

        }
        static void Main(string[] args)
        {
            Builder b = new Builder();
            b.putData();
            b.userGenerator(300);
            b.getData();

            Console.WriteLine("ANALYTICS");
            Analytics a = new Analytics();
            a.movieRank();
            a.genderCount();
            a.ageRank();
            a.registrationMonths();
            a.stateCount();
            Console.WriteLine();

            Console.WriteLine("SORTING");
            qryTester(UserSort.LastJoin);
            qryTester(UserSort.LastLog);
            qryTester(UserSort.Match);

            //unable to read file in builderlayer
            //makeZipBinary(Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())), "zips.csv"));

            Console.WriteLine("\nDone!");
            Console.ReadKey();
        }

        public static void makeZipBinary(string filename)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string[] lines = System.IO.File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string[] stringSeparators = new string[] { "," };
                string[] result = line.Split(stringSeparators, StringSplitOptions.None);
                dictionary.Add(result[0], result[1]);
            }
            Stream outstream = File.Open("zipd.bin", FileMode.Create);
            BinaryFormatter outformatter = new BinaryFormatter();
            outformatter.Serialize(outstream, dictionary);
            outstream.Close();
        }
    }
}
