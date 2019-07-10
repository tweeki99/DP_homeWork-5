using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpHw_5.Observer
{
    enum RainStatus
    {
        Raining,
        NotRaining
    }

    // Интерфейс подписчика
    interface IObserver
    {
        void Update(Object ob);
    }

    // Интерфейс издателя
    interface IObservable
    {
        void RegisterObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers();
    }

    // Человек с зонтом. Может гулять при любой погоде
    class ManWithUmbrella : IObserver
    {
        public void Update(object ob)
        {
            RainStatus rainStatus = (RainStatus)ob;

            Console.WriteLine("Человек с зонтом пойдет пешком");
        }
    }

    // Человек без зонта. Во время дождя поедет на такси 
    class ManWithoutUmbrella : IObserver
    {
        public void Update(object ob)
        {
            RainStatus rainStatus = (RainStatus)ob;

            if (rainStatus == RainStatus.Raining)
                Console.WriteLine("Человек без зонта поедет на такси");
            else
                Console.WriteLine("Человек без зонта пойдет пешком");
        }
    }

    // Класс - Издатель. Оповещает подписанных на него людей о погоде
    class Stock : IObservable
    {
        RainStatus rainStatus;

        List<IObserver> observers;
        public Stock()
        {
            observers = new List<IObserver>();
            rainStatus = RainStatus.NotRaining;
        }
        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (IObserver observer in observers)
            {
                observer.Update(rainStatus);
            }
        }

        public void Rain()
        {
            if (rainStatus == RainStatus.Raining)
            {
                Console.WriteLine("Дождь прекратился");
                rainStatus = RainStatus.NotRaining;
            }
            else
            {
                Console.WriteLine("Пошел дождь");
                rainStatus = RainStatus.Raining;
            }

            NotifyObservers();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создаем издателя и двух людей
            Stock stock = new Stock();
            ManWithoutUmbrella manWithoutUmbrella = new ManWithoutUmbrella();
            ManWithUmbrella manWithUmbrella = new ManWithUmbrella();

            // Подписываемся
            stock.RegisterObserver(manWithUmbrella);
            stock.RegisterObserver(manWithoutUmbrella);

            // Играемся с погодой и смотрим на реакцию людей
            while (true)
            {
                stock.Rain();

                Console.ReadLine();
                Console.WriteLine();
            }
        }
    }
}
