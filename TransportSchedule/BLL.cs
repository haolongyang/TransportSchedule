using System;
using System.Collections.Generic;
using System.Linq;

namespace TransportSchedule
{
    class FlightsService
    {
        public List<string> GetFlightSchedule()
        {
            List<string> formattedFlights = new List<string>();
            List<Flight> flights = GetFlights();

            foreach (Flight flight in flights)
            {
                string formattedFlight = string.Format("Flight: {0}, departure: {1}, arrival: {2}, day: {3}",
                    flight.Id, flight.Departure, flight.Arrival, flight.Day);
                formattedFlights.Add(formattedFlight);
            }

            return formattedFlights;
        }

        public List<Flight> GetFlights()
        {
            FlightStore flightStore = new FlightStore(new Logger());
            List<Flight> flights = flightStore.GetFlights();
            return flights;
        }
    }
    class OrdersService
    {
        const string toronto = "YYZ";
        const string calgory = "YYC";
        const string vancouver = "YVR";
        const string noSchedule = "not scheduled";

        public List<string> GetOrders()
        {
            List<string> formattedOrders = new List<string>();

            OrderStore orderStore = new OrderStore(new Logger());
            List<Order> orders = orderStore.GetOrders();
            DistributeOrdersToFlights(orders);

            return ConvertOrderToPrintFormat(orders);

        }

        private List<Order> DistributeOrdersToFlights(List<Order> orders)
        { 
            FlightsService flightsService = new FlightsService();
            List<Flight> flights = flightsService.GetFlights();

            foreach (Order order in orders)
            {
                switch(order.Destination)
                {
                    case toronto:
                        Flight torontoFlightOne = flights.FirstOrDefault(f => f.Arrival == toronto && f.Day == 1);
                        Flight torontoFlightTwo = flights.FirstOrDefault(f => f.Arrival == toronto && f.Day == 2);

                        if (torontoFlightOne != null && torontoFlightOne.AddOrder(order))
                        {
                            PopulateOrder(order, torontoFlightOne);
                        }
                        else if(torontoFlightTwo != null && torontoFlightTwo.AddOrder(order))
                        {
                            PopulateOrder(order, torontoFlightTwo);
                        }
                        else
                        {
                            order.FlightNumber = noSchedule;
                        }
                        break;

                    case calgory:
                        Flight calgoryFlightOne = flights.FirstOrDefault(f => f.Arrival == calgory && f.Day == 1);
                        Flight calgoryFlightTwo = flights.FirstOrDefault(f => f.Arrival == calgory && f.Day == 2);

                        if (calgoryFlightOne != null && calgoryFlightOne.AddOrder(order))
                        {
                            PopulateOrder(order, calgoryFlightOne);
                        }
                        else if (calgoryFlightTwo != null && calgoryFlightTwo.AddOrder(order))
                        {
                            PopulateOrder(order, calgoryFlightTwo);
                        }
                        else
                        {
                            order.FlightNumber = noSchedule;
                        }
                        break;

                    case vancouver:
                        Flight vancouverFlightOne = flights.FirstOrDefault(f => f.Arrival == vancouver && f.Day == 1);
                        Flight vancouverFlightTwo = flights.FirstOrDefault(f => f.Arrival == vancouver && f.Day == 2);

                        if (vancouverFlightOne != null && vancouverFlightOne.AddOrder(order))
                        {
                            PopulateOrder(order, vancouverFlightOne);
                        }
                        else if (vancouverFlightTwo != null && vancouverFlightTwo.AddOrder(order))
                        {
                            PopulateOrder(order, vancouverFlightTwo);
                        }
                        else
                        {
                            order.FlightNumber = noSchedule;
                        }
                        break;

                    default:
                        break;
                } 
            }
            return orders;
        }

        private void PopulateOrder(Order order, Flight flight)
        {
            order.FlightNumber = flight.Id.ToString();
            order.Departure = flight.Departure;
            order.Day = flight.Day;
        }

        private List<string> ConvertOrderToPrintFormat(List<Order> orders)
        {
            List<string> printFormattedOrders = new List<string>();

            foreach(Order order in orders)
            {
                if(order.FlightNumber == noSchedule || string.IsNullOrWhiteSpace(order.FlightNumber))
                {
                    printFormattedOrders.Add(string.Format("order: {0}, flightNumber: {1}", order.OrderNumber, noSchedule));
                }
                else
                {
                    printFormattedOrders.Add(string.Format("order: {0}, flightNumber: {1}, departure: {2}, arrival: {3}, day: {4}",
                        order.OrderNumber, order.FlightNumber, order.Departure, order.Destination, order.Day));
                }

            }

            return printFormattedOrders;
        }
    }
}
