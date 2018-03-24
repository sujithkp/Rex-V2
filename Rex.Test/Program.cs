using Rex.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rex.Test
{
    class Program
    {

        private IList<string> SplitSensorsBatteryPairs(string sensorBatteryPair)
        {
            var list = new List<string>();

            var insideBatteryPart = false;
            var sensorPart = new StringBuilder();

            for (int i = 0; i < sensorBatteryPair.Length; i++)
            {
                char currChar = sensorBatteryPair[i];

                if (currChar == '{')
                {
                    insideBatteryPart = true;
                    sensorPart.Append(sensorBatteryPair[i]);

                    continue;
                }

                if (currChar == '}')
                {
                    insideBatteryPart = false;
                    sensorPart.Append(sensorBatteryPair[i]);

                    list.Add(sensorPart.ToString().Trim());
                    sensorPart.Clear();
                    continue;
                }

                if (currChar == ',' && !insideBatteryPart && sensorPart.ToString().Trim().Length > 0)
                {
                    list.Add(sensorPart.ToString().Trim());

                    sensorPart.Clear();
                    continue;
                }

                if (currChar == ',' && insideBatteryPart)
                {
                    sensorPart.Append(currChar);
                }

                if (currChar != ',')
                    sensorPart.Append(currChar);

            }

            if (sensorPart.ToString().Trim().Length > 0)
                list.Add(sensorPart.ToString().Trim());

            return list;
        }

        private string ParseSensorFrom(string sensorBatteryPair)
        {
            var index = sensorBatteryPair.IndexOf('{');

            if (index == -1)
                index = sensorBatteryPair.Length;

            return sensorBatteryPair.Substring(0, index).Trim();
        }

        private IList<string> ParseBatteriesFrom(string sensorBatteryPair)
        {
            IList<string> batteries = new List<string>();

            int startIndex = sensorBatteryPair.IndexOf('{');

            if (startIndex == -1)
                return batteries;

            int endIndex = sensorBatteryPair.IndexOf('}');

            if (endIndex == -1)
                endIndex = sensorBatteryPair.Length - 1;

            batteries = sensorBatteryPair.Substring(startIndex + 1, endIndex - startIndex - 1).Split(',').Select(x => x.Trim()).ToList();

            return batteries;
        }

        private IDictionary<string, IList<string>> GetManuallyCalibratedSensorBatteryPair()
        {
            var dictionary = new Dictionary<string, IList<string>>();

            var manuallyCalibratedSensors = "17124975-8M, 17124975-NM{17131038-4, 17131038-5, 17131046-6} ,17124975-AM";

            var sensorBatteryPairs = SplitSensorsBatteryPairs(manuallyCalibratedSensors);

            foreach (var sensorBatteryPair in sensorBatteryPairs)
            {
                string sensor = ParseSensorFrom(sensorBatteryPair);
                IList<string> batteries = ParseBatteriesFrom(sensorBatteryPair);

                dictionary.Add(sensor, batteries);
            }

            return dictionary;
        }



































        static void Main(string[] args)
        {
            new Program().GetManuallyCalibratedSensorBatteryPair();











            var store = new DataStore();
            store.Connect();

            var startTime = DateTime.Now;
            var paths = store.FindPaths("LoanRequest", "Par_Gender");
            var endTime = DateTime.Now;

            var diff = endTime.Subtract(startTime);

            Console.WriteLine("Time taken : " + diff.TotalSeconds + "secs" + " (" + diff.TotalMilliseconds + ")");
            Console.WriteLine(paths.Count() + " paths found.");

            foreach (var path in paths)
                Console.WriteLine(string.Join(" > ", path));

            Console.ReadKey();
        }


        static void Tes2(string[] args)
        {
            var tableName = "Sensors";

            var store = new DataStore();

            var referencedTables = store.GetTablesReferencedBy(tableName);
            var referencingTables = store.GetTablesReferencing(tableName);

            Console.WriteLine("Tables referenced by " + tableName);
            foreach (var table in referencedTables)
                Console.WriteLine(table);

            Console.WriteLine("\nTables referencing " + tableName);
            foreach (var table in referencingTables)
                Console.WriteLine(table);

            Console.ReadLine();
        }

        static void Test1()
        {
            /*
            var adapter = new SqlServerAdapter("Data Source=SVRSQL1;Initial Catalog=Manufacturing;Persist Security Info=True;User ID=MdomUser;Password=HHeLiBe1");

            var constraints = adapter.GetReferentialConstraints();


            var tablename = "Items";

            Console.WriteLine(tablename + " reference these tables..");
            foreach (var constraint in constraints)
            {
                constraint.Participators.Where(x => x.Source.Table.Equals(tablename)).ToList().ForEach(x => Console.WriteLine(x.Source.Column + "->" + x.Target.Table + "." + x.Target.Column));
            }


            Console.WriteLine("these table references " + tablename + "..");
            foreach (var constraint in constraints)
            {
                constraint.Participators.Where(x => x.Target.Table.Equals(tablename)).ToList().ForEach(x => Console.WriteLine(x.Source.Table + "." + x.Source.Column + "->" + x.Target.Column));
            }

            Console.WriteLine(constraints.Count());
            */
        }
    }
}
