namespace TextTableFormatter
{
    public class ColumnStyle
    {
        public static readonly ColumnStyle Default = new ColumnStyle(0, int.MaxValue);

        public ColumnStyle(
            CellStyle headerStyle = null,
            CellStyle cellStyle = null,
            CellStyle footerStyle = null) : this(0, int.MaxValue, cellStyle, headerStyle, footerStyle)
        { }

        public ColumnStyle(
            int width,
            CellStyle headerStyle = null,
            CellStyle cellStyle = null,
            CellStyle footerStyle = null) : this(width, width, cellStyle, headerStyle, footerStyle)
        {}

        public ColumnStyle(
            int minWidth, 
            int maxWidth,
            CellStyle cellStyle = null,
            CellStyle headerStyle = null, 
            CellStyle footerStyle = null)
        {
            MinWidth = minWidth;
            MaxWidth = maxWidth;
            CellStyle = cellStyle;
            HeaderStyle = headerStyle;
            FooterStyle = footerStyle;
        }

        public int MinWidth { get; }
        public int MaxWidth { get; }
        public CellStyle HeaderStyle { get; }
        public CellStyle CellStyle { get; }
        public CellStyle FooterStyle { get; }
    }
}