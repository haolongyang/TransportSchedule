using System;
namespace TransportSchedule
{
    class Logger: ILogger
    {     
        public void Log(Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }
    }
}
