using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Finance.Framework.Types;
using Finance.Framework.DataAccess.Reuters;
using Finance.Framework.DataAccess.Network;

namespace Tester
{
    class Program
    {
        public static bool ReutersRealtimeTest()
        {
            bool success = false;
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            GenericAccess access = new ReutersAccess();
            access.Connect(false);
            access.RealtimeMarketDataEvent += delegate(object sender, Data data)
            {
                access.Disconnect();
                if (data.Count > 0 && data.Contains(Field.F_Ask) && data.Security == "EURJPY=")
                {
                    Console.WriteLine(data);
                    success = true;
                }
                manualEvent.Set();
            };
            access.RequestRealtime("EURJPY=", new object[] { Field.F_Ask, Field.F_Currency});
            manualEvent.WaitOne(10000);
            return success;
        }

        public static bool ReutersHistoryTest()
        {
            bool success = false;
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            GenericAccess access = new ReutersAccess();
            access.Connect(false);
            access.HistoryMarketDataEvent += delegate(object sender, HistoryData data)
            {
                access.Disconnect();
                if (data.Count > 5 && data.Security == "EURJPY=")
                    success = true;
                manualEvent.Set();
            };
            access.RequestHistory("EURJPY=", HistoryField.F_Ask, DateTime.Today.Subtract(new TimeSpan(50, 0, 0, 0)), DateTime.Today);
            manualEvent.WaitOne(10000);
            return success;
        }

        static void Main(string[] args)
        {
            bool res = ReutersRealtimeTest();
            Console.WriteLine("Reuters Realtime: " + (res ? "OK" : "Failed"));
            //res = ReutersHistoryTest();
            //Console.WriteLine("Reuters History: " + (res ? "OK" : "Failed"));
            Console.ReadKey();
        }
    }
}
