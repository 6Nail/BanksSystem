using System;
using System.Collections.Concurrent;
using System.Threading;

namespace SyncHw
{
    class Program
    {
        static void Main(string[] args)
        {
            var accounts = new ConcurrentDictionary<int, Account>();
            accounts.TryAdd(0, new Account
            {
                name = "Первый"
            });
            accounts.TryAdd(1, new Account
            {
                name = "Второй"
            });
            accounts.TryAdd(2, new Account
            {
                name = "Третий"
            });

            var random = new Random();

            for(var i = 0; i < 1000; i++)
            {
                var randFrom = random.Next(3);
                var randTo = random.Next(3);
                while(randTo == randFrom)
                {
                    randTo = random.Next(3);
                }
                var thread = new Thread(accounts[randFrom].Transfer);
                thread.Start(accounts[randTo]);
            }
            Thread.Sleep(5000);
            Console.WriteLine($"{accounts[0].name} === {accounts[0].cash}");
            Console.WriteLine($"{accounts[1].name} === {accounts[1].cash}");
            Console.WriteLine($"{accounts[2].name} === {accounts[2].cash}");
            Console.ReadKey();
        }
    }
}
