using System;
using System.Collections.Generic;
using ConsoleColours; 

namespace CrossRefCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            ANSIConsole.ActivateConsole();

            // Example 1.  Initialising a blank object and then passing a complete object[,] to the Table parameter to 
            // fill in row and column keys and all data values.  In this example the table data represents the bit rate
            // of an MP3 based on values taken from the header of the MP3 file.  The column headers represent the MPEG version 
            // and the 'Layer' and the rows represent the 4 bits taken from the file that denote the bitrate of the audio.

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

            PrintOutTable("TestTable", _TestTable);

            // Example 2.  This is a simple 3x3 grid with a string as the row key, an int as the column key and booleans as the data type.  After the grid
            // has been initialised and 3 of the falues changed to true (the default value of the data will be false) new columns keys are set which has
            // the effect of resetting all of the data.

            CrossReference<string, int, bool> _TestTableB = new CrossReference<string, int, bool>(new string[] { "A", "B", "C" }, new int[] { 1, 2, 3 });

            _TestTableB["A", 1] = true;
            _TestTableB["B", 2] = true;
            _TestTableB["C", 3] = true;

            PrintOutTable("TestTableB", _TestTableB);

            _TestTableB.Columns = new List<int>() { 9, 8, 7 };

            PrintOutTable("TestTableB (with new column headers)", _TestTableB);

            // Example 3.  This is a similiar example to the previous one but it demonstrates a different method for setting and resetting the column keys
            // Row keys can be reset in the exact same way.

            CrossReference<string, string, double> _TestTableC = new CrossReference<string, string, double>();

            _TestTableC.AddColumns(new string[] { "X", "Y", "Z" });
            _TestTableC.AddRows(new string[] { "X", "Y", "Z" });

            _TestTableC["X", "Z"] = 1.5;
            _TestTableC["Y", "Y"] = 1.5;
            _TestTableC["Z", "X"] = 1.5;

            PrintOutTable("TestTableC", _TestTableC);

            _TestTableC.AddColumns("A", "B", "C");

            _TestTableC["X", "A"] = 2.1;
            _TestTableC["Y", "B"] = 2.1;
            _TestTableC["Z", "C"] = 2.1;

            PrintOutTable("TestTableC (with new column headers and values)", _TestTableC);

            Console.ReadLine();
        }

        static void PrintOutTable<TRows, TColumns, TData>(string Label, CrossReference<TRows, TColumns, TData> TargetTable)
        {
            Console.Write(ConsoleEffects.Bright_Foreground_White + ConsoleEffects.Bold + ConsoleEffects.Underline);
            Console.Write(Label);
            Console.Write(ConsoleEffects.Default);
            Console.Write("\r\n");
            Console.Write("\r\n");

            foreach (TColumns _Column in TargetTable.Columns)
            {
                Console.Write(ConsoleEffects.Bright_Foreground_White);
                Console.Write("\t");
                Console.Write(_Column);
                Console.Write(ConsoleEffects.Default);
            }

            Console.Write("\r\n");

            foreach (TRows _Row in TargetTable.Rows)
            {
                Console.Write(ConsoleEffects.Bright_Foreground_White);
                Console.Write(_Row);
                Console.Write(ConsoleEffects.Default);

                foreach (TColumns _Column in TargetTable.Columns)
                {
                    Console.Write("\t");
                    Console.Write(TargetTable[_Row, _Column]);
                }
                Console.Write("\r\n");
            }

            Console.Write("\r\n");
        }
    }
}
