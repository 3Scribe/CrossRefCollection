# CrossRefCollection - Add a multi-dimensional dictionary collection to the language

**Eddie Gahan**

**January 2019**

The moral of this article is that you should never be afraid to extend the language.  It's something I always try to impress upon newcomers to C#, that if they're facing repetive tasks or find themselves putting the same methods into their classes over and over, they can just add an extention method and neatly wrap up their utility functions.

Currently, I'm building a library to identify audio encodings used in a byte stream and there's a lot of look-up tables required to determine bit rates, sample sizes and so on.  So, I decided to build a little extention to System.Collections.Generic that creates a multi-dimensional Dictionary collection that allows for strongly typed row/column keys and data values accessing data by a multi-parameter indexer function.  The collection listed below is the result of my work and it can be found on NuGet [here](https://www.nuget.org/packages/3Scribe.Utilities.CrossRefCollection/) with the code available openly with a little console class to demo it available [here](https://github.com/3Scribe/CrossRefCollection).


------------------------------

**CrossReference<TRows, TColumns, TData>**

Add a generic collection called CrossReference to the System.Collections.Generic namespace that allows for creation of a multi-dimensional keyed dictionary.  Values in the table are accessed by providing a row and column key.  For example, if you had a collection called PrimaryColours with the following data saved in it:

 | | Red | Green | Blue
:-----|:-----:|:------:|:-----:|
**Red**|Red|Yellow|Magenta|
**Green**|Yellow|Green|Cyan|
**Blue**|Magenta|Cyan|Blue|

then the following reference _*PrimaryColours["Blue", "Green"]*_ will return "Cyan".  The collection allows for strongly typing the row key, column key and data values when initialising, e.g.

````csharp
CrossReference<string, int, bool> _TestTable = new CrossReference<string, int, bool>();
````

creates a collection where the row keys are strings, the column keys are integers and the data values are booleans i.e.

 | | 1 | 2 | 3
:-----|:-----:|:------:|:-----:|
**X**|true|false|false|
**Y**|false|true|false|
**Z**|false|false|true|

with ["X",3] = false and ["Y", 2] = true.


**Set Up**

As this is an extention to the System.Collections.Generic namespace only that specific library will be required:

````csharp
using System.Collections.Generic;
````

**Initialising**

There are a number of ways to create and load values in to the collection.

***Example 1***  

Initialising a blank object and then passing a complete object[,] to the Table parameter to fill in row and column keys and all data values.  In this example the table data represents the bit rate of an MP3 based on values taken from the header of the MP3 file.  The column headers represent the MPEG version and the 'Layer' and the rows represent the 4 bits taken from the file that denote the bitrate of the audio.

````csharp
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
````

***Example 2***  

This is a simple 3x3 grid with a string as the row key, an int as the column key and booleans as the data type.  After the grid has been initialised and 3 of the falues changed to true (the default value of the data will be false) new columns keys are set which has the effect of resetting all of the data.

````csharp
CrossReference<string, int, bool> _TestTableB = new CrossReference<string, int, bool>(new string[] { "A", "B", "C" }, new int[] { 1, 2, 3 });

_TestTableB["A", 1] = true;
_TestTableB["B", 2] = true;
_TestTableB["C", 3] = true;

_TestTableB.Columns = new List<int>() { 9, 8, 7 };
````

***Example 3***  

This is a similiar example to the previous one but it demonstrates a different method for setting and resetting the column keys.  Row keys can be reset in the exact same way.

````csharp
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
````

**Additional Methods**

**Clear**

.Clear() resets the collection, removing any row keys, column keys and data.

**Keys**

.Rows() and .Columns() return a List collection, strongly types to the row and column key types declared.

.ContainsRowKey(TRows key) and .ContainsColumnKey(TColumns key) return a boolean to indicate if the passed key value exists in the respective collection.



