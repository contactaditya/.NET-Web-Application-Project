using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using System.Data.Entity;
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
                Builder b = new Builder();
                b.putData();
                Console.WriteLine("Done!");
                Console.ReadKey();
        }
    }
}
