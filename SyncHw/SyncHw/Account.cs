using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SyncHw
{
    public class Account
    {
        public string name;
        public int cash = 1000;
        private int transferSum;
        private object _locker = new object();

        public void Transfer(object accountTo)
        {
            lock (_locker)
            {
                transferSum = new Random().Next(100);
                var transferNumber = new Random().Next(10000);
                Thread.Sleep(5);
                if (cash < transferSum)
                {
                    Console.WriteLine($"У {name} не хватает {transferSum}тг для перевода . ===Номер транзакции: {transferNumber} ");
                    return;
                }
                Console.WriteLine($"Со счёта {name} сняли {transferSum} тг. для перевода {((Account)accountTo).name}.         ===Номер транзакции: {transferNumber}____{name}>>>{((Account)accountTo).name}____{transferSum}");
                cash -= transferSum;
                ((Account)accountTo).cash += transferSum;
                Console.WriteLine($"На счёт {((Account)accountTo).name} поступил перевод от {name} в сумме: {transferSum} тг. ===Номер транзакции: {transferNumber}____{((Account)accountTo).name}<<<{name}____{transferSum}");
            }
        }
        
    }
}
