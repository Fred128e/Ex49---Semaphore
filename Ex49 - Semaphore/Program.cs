using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Ex49___Semaphore
{
    class Program
    {
        static Semaphore semaphore = new Semaphore(10,10); //Skilift - 10 frie pladser, 10 max pladser
        static Random r = new Random();
        static void Main(string[] args)
        {

            for(int i = 0;i < 10;i++)
            {
                new Thread(Arrival).Start();
                //Thread.Sleep(r.Next(1000,10000)); //10-20 sek. at komme ned af pisten/ankomme til liften
            }
            Console.ReadKey();

        }
        static void Arrival()
        {
            for(int i = 0;i < r.Next(5,10);i++)
            {
                //Ankomst
                Stopwatch timer = new Stopwatch();
                timer.Start();
                semaphore.WaitOne();
                Console.WriteLine("A new skier has arrived");

                //På liften
                Thread.Sleep(1000);
                timer.Stop();

                Console.WriteLine(timer.Elapsed);

                //Af liften
                Console.WriteLine("Skier off the lift!");
                semaphore.Release();
                Thread.Sleep(r.Next(10000,20000));
            }
            
        }
    }
}
