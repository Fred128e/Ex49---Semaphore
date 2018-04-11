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
        static int count = 0;
        static long averageTime = 0;
        static object _locker = new object();

        static void Main(string[] args)
        {
            Thread[] threadArray = new Thread[10];
            for(int i = 0;i < 10;i++)
            {
                threadArray[i] = new Thread(Arrival);
                threadArray[i].Start();
                //Thread.Sleep(r.Next(1000,10000)); //10-20 sek. at komme ned af pisten/ankomme til liften
            }
            for(int i = 0;i < threadArray.Length;i++)
            {
                threadArray[i].Join();
            }
            Console.WriteLine("AVERAGE TIME: " + averageTime);
            Console.ReadKey();

        }
        static void Arrival()
        {
            
            for(int i = 0;i < r.Next(5,10);i++)
            {
                //Ankomst
                Stopwatch timer = new Stopwatch();
                timer.Start();
                Console.WriteLine("A new skier has arrived");
                semaphore.WaitOne();
                timer.Stop();
                Console.WriteLine(timer.ElapsedMilliseconds);
                

                //På liften
                Thread.Sleep(100);

                //Af liften
                Console.WriteLine("Skier off the lift!");
                semaphore.Release();

                lock(_locker)
                {
                    averageTime = averageTime + timer.ElapsedMilliseconds;
                }

                count = count + 1;
                Thread.Sleep(r.Next(10,21));
                
            }
            //Gennemsnit
            Console.WriteLine("Average time " + averageTime);
        

        }
    }
}
