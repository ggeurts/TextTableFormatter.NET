namespace TextTableFormatter
{
    using System.Collections.Generic;

    internal class Column
    {
        private readonly int _columnIndex;
        private readonly IList<Cell> _cells = new List<Cell>();
        private readonly int _minWidth;
        private readonly int _maxWidth;

        public int ActualWidth { get; private set; }

        internal Column(int columnIndex, int minWidth, int maxWidth)
        {
            _columnIndex = columnIndex;
            _minWidth = minWidth;
            _maxWidth = maxWidth;
        }

        internal void PerformLayout(IList<Column> columns, int separatorWidth)
        {
            var desiredWidth = _minWidth;
            foreach (var cell in _cells)
            {
                var cellOverlapWithPreviousColumns = 0;

                if (cell.ColumnSpan > 1)
                {
                    for (var pos = _columnIndex - cell.ColumnSpan + 1; pos < _columnIndex; pos++)
                    {
                        cellOverlapWithPreviousColumns += columns[pos].ActualWidth + separatorWidth;
                    }
                }

                cell.PerformLayout(_maxWidth);

                var cellOverlap = cell.ActualWidth - cellOverlapWithPreviousColumns;
                if (cellOverlap > desiredWidth)
                {
                    desiredWidth = cellOverlap;
                }
            }

            this.ActualWidth = desiredWidth;
        }

        internal void AddCell(Cell cell)
        {
            _cells.Add(cell);
        }
    }
}