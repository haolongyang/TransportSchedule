using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
namespace TransportSchedule
{
    class Order
    {
        public string OrderNumber { get; set; }

        public string Destination { get; set; }

        public string Departure { get; set; }

        public string FlightNumber { get; set; }

        public int Day { get; set; }
    }

    class OrderOfRaw
    {
        [JsonPropertyName("destination")]
        public string Destination { get; set; }
    }

    class Flight
    {
        public int Id { get; set; }

        public int Day { get; set; }

        public string Departure { get; set; }

        public string Arrival { get; set; }

        public int Capacity { get; set; }

        public List<Order> Orders { get; set; }

        public Flight(int id, int day, string departure, string arrival,int capacity)
        {
            this.Id = id;
            this.Day = day;
            this.Departure = departure;
            this.Arrival = arrival;
            this.Capacity = capacity;
            Orders = new List<Order>();
        }

        public bool AddOrder(Order order)
        {
            if(Orders.Count < 20)
            {
                Orders.Add(order);
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}