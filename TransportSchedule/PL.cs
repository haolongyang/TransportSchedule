using System;
using System.Collections.Generic;

namespace TransportSchedule
{
    class ConsolePresenter
    {
        public void DisplayFlights()
        {
            FlightsService flightsService = new FlightsService();
            List<string> flights = flightsService.GetFlightSchedule();

            Console.WriteLine("Flights Information: ");
            foreach (string flight in flights)
            {
                Console.WriteLine(flight);
            }
        }

        public void DisplayOrders()
        {
            OrdersService ordersService = new OrdersService();
            List<string> orders = ordersService.GetOrders();

            Console.WriteLine();
            Console.WriteLine("Orders Information: ");
            foreach (string order in orders)
            {
                Console.WriteLine(order);
            }
        }
    }
}
