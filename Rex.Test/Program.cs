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

        static void Main(string[] args)
        {
            var store = new DataStore();
            store.Connect();

            var paths = store.FindPath("Employees", "Suppliers");


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
