using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace System.Collections.Generic
{
    [DefaultProperty("Table")]
    public class CrossReference<TRows, TColumns, TData>
    {
        private Dictionary<TRows, Dictionary<TColumns, TData>> _TableData = new Dictionary<TRows, Dictionary<TColumns, TData>>();
        private List<TColumns> _Columns = new List<TColumns>();
        private List<TRows> _Rows = new List<TRows>();

        public CrossReference(int ColumnCount)
        {


        }

        public bool ContainsColumnKey(TColumns key)
        {
            return _Columns.Contains(key);
        }

        public bool ContainsRowKey(TRows key)
        {
            return _Rows.Contains(key);
        }

        public object[,] Table
        {
            get
            {
                return null; 
            }
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
                        _Columns.Add((TColumns)value[0, _Loop]);
                    }

                    // Populate the Rows keys from the first column of each line of the array parameter

                    // Loop starts at 1 as line 0 contains the column headers

                    for (int _Loop = 1; _Loop < value.GetLength(0); _Loop++)
                    {
                        _Rows.Add((TRows)value[_Loop, 0]);
                    }

                    




                }
            }
        }

        public TData this[TColumns ColumnKey, TRows RowKey]
        {
            get => default(TData);
            set
            {
            }
        }
    }
}
