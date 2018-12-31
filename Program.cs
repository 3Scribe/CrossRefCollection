using System;
using System.Collections.Generic;

namespace CrossRefCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            // Example 1.  Initialising a blank object and then passing a complete object[,] to the Table parameter to 
            // fill in row and column keys and all data values.  In this example the table data represents the available bit rates
            // 

            CrossReference<int, int, int> _TestTable = new CrossReference<int, int, int>();

            _TestTable.Table = new object[,]
            {
                { null, 1111, 1110, 1101, 1011, 1010, 1001 },
                { 0000,    0,    0,    0,    0,    0,    0 },
                { 0001,   32,   32,   32,   32,    8,    8 },
                { 0010,   64,   48,   40,   48,   16,   16 },
                { 0011,   96,   56,   48,   56,   24,   24 },
                { 0100,  128,   64,   56,   64,   32,   32 },
                { 0101,  160,   80,   64,   80,   40,   40 },
                { 0110,  192,   96,   80,   96,   48,   48 },
                { 0111,  224,  112,   96,  112,   56,   56 },
                { 1000,  256,  128,  112,  128,   64,   64 },
                { 1001,  288,  160,  128,  144,   80,   80 },
                { 1010,  320,  192,  160,  160,   96,   96 },
                { 1011,  352,  224,  192,  176,  112,  112 },
                { 1100,  384,  256,  224,  192,  128,  128 },
                { 1101,  416,  320,  256,  224,  144,  144 },
                { 1110,  448,  384,  320,  256,  160,  160 },
                { 1111,   -1,   -1,   -1,   -1,   -1,   -1 }
            };

            PrintOutTable(_TestTable);

            CrossReference<string, int, bool> _TestTableB = new CrossReference<string, int, bool>(new string[] { "A", "B", "C" }, new int[] { 1, 2, 3 });

            _TestTableB["A", 1] = true;
            _TestTableB["B", 2] = true;
            _TestTableB["C", 3] = true;



            _TestTableB.Columns = new List<int>() { 9, 8, 7 };


            CrossReference<string, string, double> _TestTableC = new CrossReference<string, string, double>();

            _TestTableC.AddColumns(new string[] { "X", "Y", "Z" });
            _TestTableC.AddRows(new string[] { "X", "Y", "Z" });

            _TestTableC["X", "Z"] = 1.5;
            _TestTableC["Y", "Y"] = 1.5;
            _TestTableC["Z", "X"] = 1.5;

            _TestTableC.AddColumns("A", "B", "C");

            _TestTableC["X", "A"] = 2.1;
            _TestTableC["Y", "B"] = 2.1;
            _TestTableC["Z", "C"] = 2.1;

        }

        static void PrintOutTable<TRows, TColumns, TData>(CrossReference<TRows, TColumns, TData> TargetTable)
        {
            foreach(TRows _Row in TargetTable.Rows)
            {
                foreach(TColumns _Column in TargetTable.Columns)
                {
                    Console.WriteLine(TargetTable[_Row, _Column]);
                }
            }


        }
    }
}
