using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;

namespace TransportSchedule
{
    class FlightStore
    {
        private ILogger logger;
        public FlightStore(ILogger logger)
        {
            this.logger = logger;
        }

        public List<Flight> GetFlights()
        {
            List<Flight> flights = new List<Flight>();
            flights.Add(new Flight(1, 1, "YUL", "YYZ",20));
            flights.Add(new Flight(2, 1, "YUL", "YYC",20));
            flights.Add(new Flight(3, 1, "YUL", "YVR",20));
            flights.Add(new Flight(4, 2, "YUL", "YYZ",20));
            flights.Add(new Flight(5, 2, "YUL", "YYC",20));
            flights.Add(new Flight(6, 2, "YUL", "YVR",20));

            return flights;
        }

    }
    class OrderStore
    {
        private ILogger logger;

        public OrderStore(ILogger logger)
        {
            this.logger = logger;
        }
        
        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            try
            {
                string jsonString = ReadOrdersFromJsonFile();
                Dictionary<string, OrderOfRaw> parsedJson = JsonSerializer.Deserialize<Dictionary<string, OrderOfRaw>>(jsonString);

                foreach (KeyValuePair<string, OrderOfRaw> kvp in parsedJson)
                {
                    Order order = new Order();
                    order.OrderNumber = kvp.Key;
                    order.Destination = kvp.Value.Destination;
                    orders.Add(order);
                }
            }
            catch(Exception ex)
            {
                this.logger.Log(ex);
            }
            return orders;
        }
        private string ReadOrdersFromJsonFile()           
        {
            using (StreamReader r = new StreamReader("Orders.json"))
            {
                string json = "";
                json = r.ReadToEnd();
                return json;
            }
        }
    }

}
