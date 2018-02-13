namespace TextTableFormatter
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the text table
    /// </summary>
    public class TextTable
    {
        private const int DEFAULT_MIN_WIDTH = 0;
        private const int DEFAULT_MAX_WIDTH = int.MaxValue;
        private readonly TableStyle _tableStyle;

        internal IList<Row> Rows { get; private set; } = new List<Row>();
        internal IList<Column> Columns { get; private set; } = new List<Column>();

        /// <summary>
        /// Gets the cell corresponding to the given row index and column index
        /// </summary>
        /// <param name="rowIndex">The table row index</param>
        /// <param name="columnIndex">The table column index</param>
        /// <returns></returns>
        internal Cell this[int rowIndex, int columnIndex]
        {
            get { return Rows[rowIndex].Cells[columnIndex]; }
        }

        /// <summary>
        /// Initializes a new instance of TextTable class
        /// </summary>
        /// <param name="borderStyle">The table border style</param>
        /// <param name="borderVisibility">The table visible borders</param>
        /// <param name="leftMargin">The table left margin</param>
        public TextTable(TableBorderStyle borderStyle = null, TableBorderVisibility borderVisibility = null, int leftMargin = 0)
        {
            _tableStyle = new TableStyle(borderStyle, borderVisibility, leftMargin);
        }

        public TextTable AddColumns(int columnCount)
        {
            for (var i = 0; i < columnCount; i++) AddColumn();
            return this;
        }

        public TextTable AddColumn()
        {
            return AddColumn(DEFAULT_MIN_WIDTH, DEFAULT_MAX_WIDTH);
        }

        public TextTable AddColumn(int width)
        {
            return AddColumn(width, width);
        }

        public TextTable AddColumn(int minWidth, int maxWidth)
        {
            this.Columns.Add(new Column(this.Columns.Count, minWidth, maxWidth));
            return this;
        }

        /// <summary>
        /// Adds a cell with the given content
        /// </summary>
        /// <param name="content">The cell content</param>
        public TextTable AddCell(string content)
        {
            return AddCell(content, new CellStyle());
        }

        /// <summary>
        /// Adds a cell with the given content and column span
        /// </summary>
        /// <param name="content">The cell content</param>
        /// <param name="columnSpan">The cell column span</param>
        public TextTable AddCell(string content, int columnSpan)
        {
            return AddCell(content, new CellStyle(), columnSpan);
        }

        /// <summary>
        /// Adds a cell with a given content, a style and a column span.
        /// The cell will be arranged in a new row if necessary
        /// </summary>
        /// <param name="content">The cell content</param>
        /// <param name="style">The cell style</param>
        /// <param name="columnSpan">The cell column span</param>
        public TextTable AddCell(string content, CellStyle style, int columnSpan = 1)
        {
            if (columnSpan < 1) throw new ArgumentException("columnSpan must be greater or equal to 0");

            var rowCount = this.Rows.Count;
            var currentRow = rowCount == 0 
                ? null
                : this.Rows[rowCount - 1];

            var columnCount = this.Columns.Count;
            if (currentRow == null || currentRow.ColumnSpan >= columnCount)
            {
                currentRow = new Row();
                this.Rows.Add(currentRow);
            }

            columnSpan = Math.Min(Math.Max(1, columnSpan), columnCount - currentRow.ColumnSpan);
            currentRow.Cells.Add(new Cell(content, style, columnSpan));

            return this;
        }

        /// <summary>
        /// Renders the table as a string
        /// </summary>
        /// <returns></returns>
        public string Render()
        {
            PerformLayout();
            return _tableStyle.Render(this);
        }

        /// <summary>
        /// Renders the table as a string array (each array element is a row)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> RenderLines()
        {
            PerformLayout();
            return _tableStyle.RenderLines(this);
        }

        private void PerformLayout()
        {
            // First we connect the columns with the cells.
            foreach (var row in Rows)
            {
                var startCol = 0;
                foreach (var cell in row.Cells)
                {
                    var endCol = startCol + cell.ColumnSpan - 1;
                    if (endCol < this.Columns.Count)
                    {
                        var col = Columns[endCol];
                        col.AddCell(cell);
                        startCol = startCol + cell.ColumnSpan;
                    }
                }
            }

            // Then we calculate the appropriate column width for each one.
            foreach (var col in this.Columns)
            {
                col.PerformLayout(Columns, _tableStyle.BorderStyle.TopCenterCorner.Length);
            }
        }
    }
}