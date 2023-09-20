using System;
using System.Collections.Generic;

namespace memento2
{
    class Program
    {
        static void Main(string[] args)
        {
            Vlad vlad = new Vlad();
            vlad.Go(); // денег 8, поездка 1
            GameHistory game = new GameHistory();

            game.History.Push(vlad.SaveState()); // сохраняем игру

            vlad.Go(); //денег 6, поездки 2

            vlad.RestoreState(game.History.Pop());

            vlad.Go(); //денег 6, поездки 2

            vlad.Go();
            vlad.Go();
            vlad.Go();
            vlad.Go(); //деньгам конец

            Console.Read();
        }
    }

    // Originator
    class Vlad
    {
        private int cash = 10; // кол-во денег
        private int metro = 0; // кол-во поездок

        public void Go()
        {
            if (cash > 0)
            {
                cash-=2;
                metro++;
                Console.WriteLine("Тратим кеш на метро. Осталось {0} рублей. А так же {1} поездка на счету.", cash, metro);
            }
            else
                Console.WriteLine("Деньги кончились чувак, никуда уже не уедешь");
        }
        // сохранение состояния
        public VladMemento SaveState()
        {
            Console.WriteLine("Сохранение сберегательного счета. Параметры: {0} монеток, {1} поездки", cash, metro);
            return new VladMemento(cash, metro);
        }

        // восстановление состояния
        public void RestoreState(VladMemento memento)
        {
            this.cash = memento.Cash;
            this.metro = memento.Metro;
            Console.WriteLine("Восстановление сберегательного счета. Параметры: {0} монеток, {1} поездки", cash, metro);
        }
    }
    // Memento
    class VladMemento
    {
        public int Cash { get; private set; }
        public int Metro { get; private set; }

        public VladMemento(int cash, int metro)
        {
            this.Cash = cash;
            this.Metro = metro;
        }
    }

    // Caretaker
    class GameHistory
    {
        public Stack<VladMemento> History { get; private set; }
        public GameHistory()
        {
            History = new Stack<VladMemento>();
        }
    }
}