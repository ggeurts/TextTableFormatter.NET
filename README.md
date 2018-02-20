# TextTableFormatter.NET

## Project Description
TextTableFormatter is a .NET port of [Java TextTableFormatter](http://texttablefmt.sourceforge.net/). 
This library renders tables made of characters. The user add cells and can add format characteristics 
like predefined/custom table styles, text alignment, abbreviation, column width, border types, colspan, 
etc.

Also available on [Nuget](https://www.nuget.org/packages/TextTableFormatter.NET/).

![Screenshot](https://dariosantarelli.files.wordpress.com/2013/03/209137.jpg)

## Some code examples

```C#
// 1. BASIC TABLE EXAMPLE
var basicTable = new TextTable()
    .AddColumns(3);
    .AddCell("Artist")
    .AddCell("Album")
    .AddCell("Year")
    .AddCell("Jamiroquai")
    .AddCell("Emergency on Planet Earth")
    .AddCell("1993")
    .AddCell("Jamiroquai")
    .AddCell("The Return of the Space Cowboy")
    .AddCell("1994");

Console.WriteLine(basicTable.Render());

// +----------+-------------------------------+-----+
// |Artist    |Album                          |Year |
// +----------+-------------------------------+-----+
// |Jamiroquai|Emergency on Planet Earth      |1993 |
// |Jamiroquai|The Return of the Space Cowboy |1994 |
// +----------+-------------------------------+-----+

// 2. ADVANCED TABLE EXAMPLE
var numberStyleAdvancedTable = new CellStyle(CellTextAlignment.Right);
var advancedTable = new TextTable(TableBorderStyle.DESIGN_FORMAL, TableBorderVisibility.SURROUND_HEADER_FOOTER_AND_COLUMNS)
    .AddColumn(new ColumnStyle(6, 14))
    .AddColumn(new ColumnStyle(4, 12, numberStyleAdvancedTable))
    .AddColumn(new ColumnStyle(4, 12, numberStyleAdvancedTable))
    .AddCells("Region", "Orders", "Sales")
    .AddCells("North", "6,345", "$87.230")
    .AddCells("Center", "837", "$12.855")
    .AddCells("South", "5,344", "$72.561")
    .AddCell("Total", numberStyleAdvancedTable, 2)
    .AddCell("$172.646");

Console.WriteLine(advancedTable.Render());

// ======================
// Region Orders    Sales
// ------ ------ --------
// North   6,345  $87.230
// Center    837  $12.855
// South   5,344  $72.561
// ------ ------ --------
//         Total $172.646
// ======================


// 3. FANCY TABLE EXAMPLE
var numberStyleFancyTable = new CellStyle(CellTextAlignment.Right);
var fancyTable = new TextTable(TableBorderStyle.DESIGN_PAPYRUS, TableBorderVisibility.SURROUND_HEADER_FOOTER_AND_COLUMNS)
    .AddColumn()
    .AddColumn(cellStyle: numberStyleFancyTable)
    .AddColumn(cellStyle: numberStyleFancyTable)
    .AddCells("Region", "Orders", "Sales")
    .AddCells("North", "6,345", "$87.230")
    .AddCells("Center", "837", "$12.855")
    .AddCells("South", "5,344", "$72.561")
    .AddCell("Total", numberStyleFancyTable, 2);
    .AddCell("$172.646");

Console.WriteLine(fancyTable.Render());

// o~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~o
//  )  Region  Orders     Sales  (
//  )  ~~~~~~  ~~~~~~  ~~~~~~~~  (
//  )  North    6,345   $87.230  (
//  )  Center     837   $12.855  (
//  )  South    5,344   $72.561  (
//  )  ~~~~~~  ~~~~~~  ~~~~~~~~  (
//  )           Total  $172.646  (
// o~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~o


// 4. UNICODE TABLE EXAMPLE
var numberStyleUnicodeTable = new CellStyle(CellTextAlignment.Right);
var unicodeTable = new TextTable(TableBorderStyle.UNICODE_BOX_DOUBLE_BORDER_WIDE, TableBorderVisibility.SURROUND_HEADER_FOOTER_AND_COLUMNS)
    .AddColumn()
    .AddColumn(cellStyle: numberStyleUnicodeTable)
    .AddColumn(cellStyle: numberStyleUnicodeTable)
    .AddCells("Region", "Orders", "Sales")
    .AddCells("North", "6,345", "$87.230")
    .AddCells("Center", "837", "$12.855")
    .AddCells("South", "5,344", "$72.561")
    .AddCell("Total", numberStyleUnicodeTable, 2);
    .AddCell("$172.646");
           
var sb = new StringBuilder("<html><body><pre>");
foreach (string line in unicodeTable.RenderLines()) 
{
    sb.Append(WebUtility.HtmlEncode(line));
    sb.Append("<br>");
}
sb.Append("</pre></body></html>");

File.WriteAllText("unicode.html", sb.ToString(), Encoding.UTF8);

// unicode.html
// ╔════════╤════════╤══════════╗
// ║ Region │ Orders │    Sales ║
// ╟────────┼────────┼──────────╢
// ║ North  │  6,345 │  $87.230 ║
// ║ Center │    837 │  $12.855 ║
// ║ South  │  5,344 │  $72.561 ║
// ╟────────┴────────┼──────────╢
// ║           Total │ $172.646 ║
// ╚═════════════════╧══════════╝
```

## License

[Apache License Version 2.0](https://github.com/dsantarelli/TextTableFormatter.NET/blob/master/LICENSE.md)
