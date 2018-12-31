using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace System.Collections.Generic
{
    /// <summary>
    /// Extention to the System.Collections.Generic namespace that creates a generic multidimensional dictionary table for use as a cross-reference or lookup table.   
    /// </summary>
    /// <typeparam name="TRows">Type used by the Row keys collection</typeparam>
    /// <typeparam name="TColumns">Type used by the Column keys collection</typeparam>
    /// <typeparam name="TData">Type used by the data items stored in the table</typeparam>
    /// <exception cref="IndexOutOfRangeException">Exception will be thrown when referencing a cell, if either of the keys provided do not exist in the collection.</exception>
    /// <exception cref="Exception">Thrown when attempting to add a null collection to the Rows or Columns</exception>
    [DefaultProperty("Table")]
    public class CrossReference<TRows, TColumns, TData>
    {
        private Dictionary<TRows, Dictionary<TColumns, TData>> _TableData = new Dictionary<TRows, Dictionary<TColumns, TData>>();
        private List<TColumns> _Columns = new List<TColumns>();
        private List<TRows> _Rows = new List<TRows>();

        /// <summary>
        /// Initialises the class with no rows, columns or data.
        /// </summary>
        public CrossReference()
        {
        }

        /// <summary>
        /// Initialises the class with the specified list of row keys.
        /// </summary>
        /// <param name="rowslist">A list of row keys.  Duplicates will cause an exception to be thrown.</param>
        public CrossReference(TRows[] rowslist)
        {
            AddRows(rowslist);
        }

        /// <summary>
        /// Initialises the class with the specified list of row keys.
        /// </summary>
        /// <param name="rowslist">A list of row keys.  Duplicates will cause an exception to be thrown.</param>
        /// <param name="columnslist">A list of column keys.  Duplicates will cause an exception to be thrown.</param>
        public CrossReference(TRows[] rowslist, TColumns[] columnslist)
        {
            AddColumns(columnslist);
            AddRows(rowslist);
        }

        /// <summary>
        /// Empties the CrossReference table of all data. 
        /// </summary>
        public void Clear()
        {
            // Reset the data   

            _TableData = new Dictionary<TRows, Dictionary<TColumns, TData>>();
            _Columns = new List<TColumns>();
            _Rows = new List<TRows>();
        }

        /// <summary>
        /// Basic Column Keys property using List<<typeparamref name="TColumns"/>>
        /// </summary>
        public List<TColumns> Columns
        {
            get { return _Columns; }
            set
            {
                AddColumns(value.ToArray());
            }
        }

        /// <summary>
        /// Basic Row Keys property using List<<typeparamref name="TRows"/>>
        /// </summary>
        public List<TRows> Rows
        {
            get { return _Rows; }
            set
            {
                AddRows(value.ToArray());
            }
        }

        /// <summary>
        /// An overload to the AddColumns method to allow for adding of one or more column keys in a comma delimited parameter list
        /// </summary>
        /// <param name="firstcolumn"></param>
        /// <param name="additionalcolumns"></param>
        /// <returns></returns>
        public int AddColumns(TColumns firstcolumn, params TColumns[] additionalcolumns)
        {
            TColumns[] _NewList = new TColumns[1 + additionalcolumns.Length];
            _NewList[0] = firstcolumn;
            Array.Copy(additionalcolumns, 0, _NewList, 1, additionalcolumns.Length);

            return AddColumns(_NewList);
        }

        /// <summary>
        /// An overload to the AddRows method to allow for adding of one or more row keys in a comma delimited parameter list
        /// </summary>
        /// <param name="firstrow"></param>
        /// <param name="additionalrows"></param>
        /// <returns></returns>
        public int AddRows(TRows firstrow, params TRows[] additionalrows)
        {
            TRows[] _NewList = new TRows[1 + additionalrows.Length];
            _NewList[0] = firstrow;
            Array.Copy(additionalrows, 0, _NewList, 1, additionalrows.Length);

            return AddRows(_NewList);
        }

        /// <summary>
        /// Replaces the existing list of columns with the list passed in the columnslist parameter.
        /// </summary>
        /// <param name="columnslist">A list of column keys.  Duplicates will cause an exception to be thrown.</param>
        /// <returns>The number of columns added to the column keys collection</returns>
        public int AddColumns(TColumns[] columnslist)
        {
            // Reset the List of columns and all of the data for each row

            if (columnslist != null)
            {
                _Columns = new List<TColumns>();

                // Populate the Columns keys from the array parameter

                for (int _Loop = 0; _Loop < columnslist.GetLength(0); _Loop++)
                {
                    if (columnslist[_Loop] != null)
                    {
                        _Columns.Add(columnslist[_Loop]);
                    }
                }

                // Reset the data in the table with the new columns but keeping all row keys

                RefillTable();

                return _Columns.Count;
            }
            else
            {
                throw new Exception("Parameter cannot be null");
            }
        }

        /// <summary>
        /// Replaces the existing list of rows with the list passed in the rowslist parameter.
        /// </summary>
        /// <param name="rowslist">A list of row keys.  Duplicates will cause an exception to be thrown.</param>
        /// <returns>The number of rows added to the row keys collection</returns>
        public int AddRows(params TRows[] rowslist)
        {
            if (rowslist != null)
            {
                _Rows = new List<TRows>();

                // Populate the Row keys from the array parameter

                for (int _Loop = 0; _Loop < rowslist.GetLength(0); _Loop++)
                {
                    if (rowslist[_Loop] != null)
                    {
                        _Rows.Add(rowslist[_Loop]);
                    }
                }

                // Reset the data in the table with the new columns but keeping all row keys

                RefillTable();

                return _Rows.Count;
            }
            else
            {
                throw new Exception("Parameter cannot be null");
            }
        }

        /// <summary>
        /// Internal method to reset the data table using the keys stored in the _Rows and _Columns Lists.
        /// </summary>
        private void RefillTable()
        {
            _TableData = new Dictionary<TRows, Dictionary<TColumns, TData>>();

            for (int _Loop = 0; _Loop < _Rows.Count; _Loop++)
            {
                // Load a row of blank data

                Dictionary<TColumns, TData> _TempRow = new Dictionary<TColumns, TData>();

                for (int _LoopB = 0; _LoopB < _Columns.Count; _LoopB++)
                {
                    _TempRow.Add(_Columns[_LoopB], default(TData));
                }

                _TableData.Add(_Rows[_Loop], _TempRow);
            }
        }

        /// <summary>
        /// Determines if the element is in the column keys list.
        /// </summary>
        /// <param name="key">The key to search for</param>
        /// <returns></returns>
        public bool ContainsColumnKey(TColumns key)
        {
            return _Columns.Contains(key);
        }

        /// <summary>
        /// Determines if the element is in the row keys list.
        /// </summary>
        /// <param name="key">The key to search for</param>
        /// <returns></returns>
        public bool ContainsRowKey(TRows key)
        {
            return _Rows.Contains(key);
        }

        /// <summary>
        /// Default parameter of the class to allow for easy filling of the table.  First line of the object[,] value will be taken as the column keys with the first item in that row being ignored.  The first column in the subsequent rows will be used as the row key. 
        /// </summary>
        public object[,] Table
        {
            set
            {
                // Reset and reload the data   

                _TableData = new Dictionary<TRows, Dictionary<TColumns, TData>>();
                _Columns = new List<TColumns>();
                _Rows = new List<TRows>();

                if (value != null)
                {
                    // Populate the Columns keys from the first line of the array parameter
                    // Loop starts at 1 as position 0 is null to accomodate the row value

                    for (int _Loop = 1; _Loop < value.GetLength(1); _Loop++)
                    {
                        if ((TColumns)value[0, _Loop] != null)
                        {
                            _Columns.Add((TColumns)value[0, _Loop]);
                        }
                    }

                    // Populate the Rows keys from the first column of each line of the array parameter
                    // and populate the data table
                    // Loop starts at 1 as line 0 contains the column headers

                    for (int _Loop = 1; _Loop < value.GetLength(0); _Loop++)
                    {
                        _Rows.Add((TRows)value[_Loop, 0]);

                        // Load the row of data

                        Dictionary<TColumns, TData> _TempRow = new Dictionary<TColumns, TData>();

                        for (int _LoopB = 1; _LoopB <= _Columns.Count; _LoopB++)
                        {
                            _TempRow.Add(_Columns[_LoopB - 1], (TData)value[_Loop, _LoopB]);
                        }

                        _TableData.Add((TRows)value[_Loop, 0], _TempRow);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value for the row and column coordinates provided.
        /// </summary>
        /// <param name="RowKey"></param>
        /// <param name="ColumnKey"></param>
        /// <exception cref="IndexOutOfRangeException">Exception will be thrown if either of the keys provided do not exist in the collection.</exception>
        /// <returns></returns>
        public TData this[TRows RowKey, TColumns ColumnKey]
        {
            // Get and set specified cells in the table.

            get
            {
                if (_TableData.ContainsKey(RowKey))
                {
                    if (_TableData[RowKey].ContainsKey(ColumnKey))
                    {
                        return _TableData[RowKey][ColumnKey];
                    }
                    else
                    {
                        throw new IndexOutOfRangeException("Column key is unknown.");
                    }
                }
                else
                {
                    throw new IndexOutOfRangeException("Row key is unknown.");
                }
            }
            set
            {
                if (_TableData.ContainsKey(RowKey) && _TableData[RowKey].ContainsKey(ColumnKey))
                {
                    _TableData[RowKey][ColumnKey] = value;
                }
            }
        }
    }
}
