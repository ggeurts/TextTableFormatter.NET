using System.Collections.Generic;

namespace TextTableFormatter
{
    internal class Column
    {
        private readonly int _columnIndex;
        private readonly IList<Cell> _cells = new List<Cell>();
        private int _minWidth;
        private int _maxWidth;

        internal int ActualWidth { get; private set; } = -1;

        internal Column(int columnIndex, int minWidth, int maxWidth)
        {
            _columnIndex = columnIndex;
            _minWidth = minWidth;
            _maxWidth = maxWidth;
        }

        internal Column(int columnIndex, int width)
        {
            _columnIndex = columnIndex;
            _minWidth = width;
            _maxWidth = width;
        }

        internal void PerformLayout(IList<Column> columns, int separatorWidth)
        {
            this.ActualWidth = _minWidth;
            foreach (var cell in _cells)
            {
                var previousWidth = 0;
                if (cell.ColumnSpan > 1)
                {
                    for (var pos = _columnIndex - cell.ColumnSpan + 1; pos < _columnIndex; pos++)
                    {
                        previousWidth += columns[pos].ActualWidth + separatorWidth;
                    }
                }

                cell.PerformLayout(_maxWidth);
                var desiredColumWidth = cell.ActualWidth - previousWidth;
                if (desiredColumWidth > this.ActualWidth) this.ActualWidth = desiredColumWidth;
            }
        }

        internal void AddCell(Cell cell)
        {
            _cells.Add(cell);
        }

        internal void SetWidthRange(int minWidth, int maxWidth)
        {
            _minWidth = minWidth;
            _maxWidth = maxWidth;
        }
    }
}