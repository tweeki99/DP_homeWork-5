using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DpHw_5.Strategy
{
    public interface IStrategy
    {
        object DoAlgorithm();
    }

    // Стратегия генерации случайного четного числа
    class EvenNumberStrategy : IStrategy
    {
        public object DoAlgorithm()
        {
            Console.WriteLine("Рандомное четное число.");

            Random rnd = new Random();
            int number;

            while (true)
            {
                number = rnd.Next(100);

                if (number % 2 == 0)
                    break;
            }

            return number;
        }
    }

    // Стратегия генерации случайного нечетного числа
    class OddNumberStrategy : IStrategy
    {
        public object DoAlgorithm()
        {
            Console.WriteLine("Рандомное нечетное число.");

            Random rnd = new Random();
            int number;

            while (true)
            {
                number = rnd.Next(100);

                if (number % 2 != 0)
                    break;
            }

            return number;
        }
    }

    // Контекст перекладывает часть логики на стратегию 
    class Context
    {
        private IStrategy _strategy;

        public void SetStrategy(IStrategy strategy)
        {
            this._strategy = strategy;
        }

        public int Start()
        {
            var result = this._strategy.DoAlgorithm();

            return (int)result;
        }
    }

    

    class Program
    {
        static void Main(string[] args)
        {
            // Создаем контекста
            var context = new Context();

            // Задаем стратегию и начинаем выполнение
            context.SetStrategy(new EvenNumberStrategy());
            Console.WriteLine(context.Start());

            Console.WriteLine();

            // В любой момент можем изменить стратегию, тем самым изменив логику выполнения
            context.SetStrategy(new OddNumberStrategy());
            Console.WriteLine(context.Start());

            Console.ReadLine();
        }
    }
}