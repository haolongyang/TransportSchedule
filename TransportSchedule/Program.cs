using System;

namespace TransportSchedule
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsolePresenter consolePresenter = new ConsolePresenter();
            consolePresenter.DisplayFlights();
            consolePresenter.DisplayOrders();
        }
    }
}
