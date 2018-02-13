namespace TextTableFormatter
{
    using System;
    using System.Text;

    /// <summary>
    /// Represents a table borders style
    /// </summary>
    public class TableBorderStyle
    {
        public static TableBorderStyle DEMO = new TableBorderStyle("1a234b567c89xyztu");
        public static TableBorderStyle BLANKS = new TableBorderStyle("    " + "    " + "    " + "   " + "  ");
        public static TableBorderStyle DOTS = new TableBorderStyle("...." + "...." + "...." + "..." + "..");
        public static TableBorderStyle ASTERISKS = new TableBorderStyle("****" + "****" + "****" + "***" + "**");

        public static TableBorderStyle HORIZONTAL_ONLY =
            new TableBorderStyle("", '-', " ", "", "", '-', " ", "", "", '-', " ", "", "", " ", "", " ", " ");

        public static TableBorderStyle CLASSIC = new TableBorderStyle("+-++" + "+-++" + "+-++" + "|||" + "--");

        public static TableBorderStyle CLASSIC_WIDE = new TableBorderStyle("+-", '-', "-+-", "-+", "+-", '-', "-+-",
            "-+", "+-", '-', "-+-", "-+", "| ", " | ", " |", "---", "---");

        public static TableBorderStyle CLASSIC_LIGHT = new TableBorderStyle("+--+" + "+--+" + "+--+" + "| |" + "--");

        public static TableBorderStyle CLASSIC_LIGHT_WIDE = new TableBorderStyle("+-", '-', "--", "-+", "+-", '-',
            "--", "-+", "+-", '-', "--", "-+", "| ", "  ", " |", "--", "--");

        public static TableBorderStyle CLASSIC_COMPATIBLE =
            new TableBorderStyle("+-++" + "+-++" + "+-++" + "!!!" + "--");

        public static TableBorderStyle CLASSIC_COMPATIBLE_WIDE = new TableBorderStyle("+-", '-', "-+-", "-+", "+-",
            '-', "-+-", "-+", "+-", '-', "-+-", "-+", "! ", " ! ", " !", "---", "---");

        public static TableBorderStyle CLASSIC_COMPATIBLE_LIGHT_WIDE = new TableBorderStyle("+-", '-', "--", "-+",
            "+-", '-', "--", "-+", "+-", '-', "--", "-+", "! ", "  ", " !", "--", "--");

        public static TableBorderStyle HEAVY = new TableBorderStyle("+==+" + "+==+" + "+==+" + "|||" + "==");

        public static TableBorderStyle HEAVY_TOP_AND_BOTTOM =
            new TableBorderStyle("+==+" + "+--+" + "+==+" + "|||" + "--");

        public static TableBorderStyle DESIGN_FORMAL =
            new TableBorderStyle("", '=', "=", "", "", '-', " ", "", "", '=', "=", "", "", " ", "", " ", " ");

        public static TableBorderStyle DESIGN_FORMAL_INVERSE =
            new TableBorderStyle("", '-', "-", "", "", '=', " ", "", "", '-', "-", "", "", " ", "", " ", " ");

        public static TableBorderStyle DESIGN_CASUAL =
            new TableBorderStyle("", '=', "=", "", "", '~', " ", "", "", '=', "=", "", "", " ", "", " ", " ");

        public static TableBorderStyle DESIGN_CAFE =
            new TableBorderStyle("", '~', "~", "", "", '~', " ", "", "", '~', "~", "", "", " ", "", " ", " ");

        public static TableBorderStyle DESIGN_SLASH =
            new TableBorderStyle("", '/', "/", "", "", '-', " ", "", "", '/', "/", "", "", " ", "", " ", " ");

        public static TableBorderStyle DESIGN_TUBES = new TableBorderStyle(" __ " + "|_||" + "|_||" + "|||" + "__");

        public static TableBorderStyle DESIGN_DOTS = new TableBorderStyle("\u00b7", '\u00b7', "\u00b7", "\u00b7",
            "\u00b7", '\u00b7', "\u00b7", "\u00b7", "\u00b7", '\u00b7', "\u00b7", "\u00b7", "\u00b7", "\u00b7",
            "\u00b7", "\u00b7", "\u00b7");

        public static TableBorderStyle DESIGN_DIM = new TableBorderStyle("", '\u00a8', "\u00a8", "", "", '\u00a8',
            " ", "", "", '\u00a8', "\u00a8", "", "", " ", "", "\u00a8", "\u00a8");

        public static TableBorderStyle DESIGN_CURTAIN = new TableBorderStyle("o~", '~', "~", "~o", "  ", '-', " ",
            "  ", "o~", '~', "~", "~o", "  ", " ", "  ", " ", " ");

        public static TableBorderStyle DESIGN_CURTAIN_HEAVY = new TableBorderStyle("o=", '=', "=", "=o", "  ", '-',
            " ", "  ", "o=", '=', "=", "=o", "  ", " ", "  ", " ", " ");

        public static TableBorderStyle DESIGN_PAPYRUS = new TableBorderStyle("o~~~", '~', "~~", "~~~o", " )  ", '~',
            "  ", "  (", "o~~~", '~', "~~", "~~~o", " )  ", "  ", "  (", "  ", "  ");

        public static TableBorderStyle DESIGN_FORMAL_WIDE =
            new TableBorderStyle("", '=', "==", "", "", '-', "  ", "", "", '=', "==", "", "", "  ", "", "  ", "  ");

        public static TableBorderStyle DESIGN_FORMAL_INVERSE_WIDE =
            new TableBorderStyle("", '-', "--", "", "", '=', "  ", "", "", '-', "--", "", "", "  ", "", "  ", "  ");

        public static TableBorderStyle DESIGN_CASUAL_WIDE =
            new TableBorderStyle("", '=', "==", "", "", '~', "  ", "", "", '=', "==", "", "", "  ", "", "  ", "  ");

        public static TableBorderStyle DESIGN_CAFE_WIDE =
            new TableBorderStyle("", '~', "~~", "", "", '~', "  ", "", "", '~', "~~", "", "", "  ", "", "  ", "  ");

        public static TableBorderStyle DESIGN_SLASH_WIDE =
            new TableBorderStyle("", '/', "//", "", "", '-', "  ", "", "", '/', "//", "", "", "  ", "", "  ", "  ");

        public static TableBorderStyle DESIGN_TUBES_WIDE = new TableBorderStyle(" _", '_', "___", "_ ", "|_", '_',
            "_|_", "_|", "|_", '_', "_|_", "_|", "| ", " | ", " |", "___", "___");

        public static TableBorderStyle DESIGN_DOTS_WIDE = new TableBorderStyle("\u00b7\u00b7", '\u00b7',
            "\u00b7\u00b7\u00b7", "\u00b7\u00b7", "\u00b7\u00b7", '\u00b7', "\u00b7\u00b7\u00b7", " \u00b7",
            "\u00b7\u00b7", '\u00b7', "\u00b7\u00b7\u00b7", "\u00b7\u00b7", "\u00b7 ", " \u00b7 ", " \u00b7",
            "\u00b7\u00b7\u00b7", "\u00b7\u00b7\u00b7");

        public static TableBorderStyle DESIGN_DIM_WIDE = new TableBorderStyle("", '\u00a8', "\u00a8\u00a8", "", "",
            '\u00a8', "  ", "", "", '\u00a8', "\u00a8\u00a8", "", "", "  ", "", "  ", "  ");

        public static TableBorderStyle DESIGN_CURTAIN_WIDE = new TableBorderStyle("o~", '~', "~~", "~o", "  ", '-',
            "  ", "  ", "o~", '~', "~~", "~o", "  ", "  ", "  ", "  ", "  ");

        public static TableBorderStyle DESIGN_CURTAIN_HEAVY_WIDE = new TableBorderStyle("o=", '=', "==", "=o", "  ",
            '-', "  ", "  ", "o=", '=', "==", "=o", "  ", "  ", "  ", "  ", "  ");

        public static TableBorderStyle UNICODE_BOX =
            new TableBorderStyle("\u250c\u2500\u252c\u2510" + "\u251c\u2500\u253c\u2524" + "\u2514\u2500\u2534\u2518" +
                                  "\u2502\u2502\u2502" + "\u2534\u252c");

        public static TableBorderStyle UNICODE_ROUND_BOX =
            new TableBorderStyle("\u256d\u2500\u252c\u256e" + "\u251c\u2500\u253c\u2524" + "\u2570\u2500\u2534\u256f" +
                                  "\u2502\u2502\u2502" + "\u2534\u252c");

        public static TableBorderStyle UNICODE_HEAVY_BOX =
            new TableBorderStyle("\u250f\u2501\u2533\u2513" + "\u2523\u2501\u254b\u252b" + "\u2517\u2501\u253b\u251b" +
                                  "\u2503\u2503\u2503" + "\u253b\u2533");

        public static TableBorderStyle UNICODE_BOX_HEAVY_BORDER =
            new TableBorderStyle("\u250f\u2501\u252f\u2513" + "\u2520\u2500\u253c\u2528" + "\u2517\u2501\u2537\u251b" +
                                  "\u2503\u2502\u2503" + "\u2534\u252c");

        public static TableBorderStyle UNICODE_DOUBLE_BOX =
            new TableBorderStyle("\u2554\u2550\u2566\u2557" + "\u2560\u2550\u256c\u2563" + "\u255a\u2550\u2569\u255d" +
                                  "\u2551\u2551\u2551" + "\u2569\u2566");

        public static TableBorderStyle UNICODE_BOX_DOUBLE_BORDER =
            new TableBorderStyle("\u2554\u2550\u2564\u2557" + "\u255f\u2500\u253c\u2562" + "\u255a\u2550\u2567\u255d" +
                                  "\u2551\u2502\u2551" + "\u2534\u252c");

        public static TableBorderStyle UNICODE_BOX_WIDE = new TableBorderStyle("\u250c\u2500", '\u2500',
            "\u2500\u252c\u2500", "\u2500\u2510", "\u251c\u2500", '\u2500', "\u2500\u253c\u2500", "\u2500\u2524",
            "\u2514\u2500", '\u2500', "\u2500\u2534\u2500", "\u2500\u2518", "\u2502 ", " \u2502 ", " \u2502",
            "\u2500\u2534\u2500", "\u2500\u252c\u2500");

        public static TableBorderStyle UNICODE_ROUND_BOX_WIDE = new TableBorderStyle("\u256d\u2500", '\u2500',
            "\u2500\u252c\u2500", "\u2500\u256e", "\u251c\u2500", '\u2500', "\u2500\u253c\u2500", "\u2500\u2524",
            "\u2570\u2500", '\u2500', "\u2500\u2534\u2500", "\u2500\u256f", "\u2502 ", " \u2502 ", " \u2502",
            "\u2500\u2534\u2500", "\u2500\u252c\u2500");

        public static TableBorderStyle UNICODE_HEAVY_BOX_WIDE = new TableBorderStyle("\u250f\u2501", '\u2501',
            "\u2501\u2533\u2501", "\u2501\u2513", "\u2523\u2501", '\u2501', "\u2501\u254b\u2501", "\u2501\u252b",
            "\u2517\u2501", '\u2501', "\u2501\u253b\u2501", "\u2501\u251b", "\u2503 ", " \u2503 ", " \u2503",
            "\u2501\u253b\u2501", "\u2501\u2533\u2501");

        public static TableBorderStyle UNICODE_BOX_HEAVY_BORDER_WIDE = new TableBorderStyle("\u250f\u2501", '\u2501',
            "\u2501\u252f\u2501", "\u2501\u2513", "\u2520\u2500", '\u2500', "\u2500\u253c\u2500", "\u2500\u2528",
            "\u2517\u2501", '\u2501', "\u2501\u2537\u2501", "\u2501\u251b", "\u2503 ", " \u2502 ", " \u2503",
            "\u2500\u2534\u2500", "\u2500\u252c\u2500");

        public static TableBorderStyle UNICODE_DOUBLE_BOX_WIDE = new TableBorderStyle("\u2554\u2550", '\u2550',
            "\u2550\u2566\u2550", "\u2550\u2557", "\u2560\u2550", '\u2550', "\u2550\u256c\u2550", "\u2550\u2563",
            "\u255a\u2550", '\u2550', "\u2550\u2569\u2550", "\u2550\u255d", "\u2551 ", " \u2551 ", " \u2551",
            "\u2550\u2569\u2550", "\u2550\u2566\u2550");

        public static TableBorderStyle UNICODE_BOX_DOUBLE_BORDER_WIDE = new TableBorderStyle("\u2554\u2550", '\u2550',
            "\u2550\u2564\u2550", "\u2550\u2557", "\u255f\u2500", '\u2500', "\u2500\u253c\u2500", "\u2500\u2562",
            "\u255a\u2550", '\u2550', "\u2550\u2567\u2550", "\u2550\u255d", "\u2551 ", " \u2502 ", " \u2551",
            "\u2500\u2534\u2500", "\u2500\u252c\u2500");

        private const string DEFAULT_TILE = "*";

        /// <summary>
        /// Gets or sets the bottom-center corner
        /// </summary>
        public string BottomCenterCorner { get; set; }

        /// <summary>
        /// Gets or sets the bottom-left corner
        /// </summary>
        public string BottomLeftCorner { get; set; }

        /// <summary>
        /// Gets or sets the bottom
        /// </summary>
        public char Bottom { get; set; }

        /// <summary>
        /// Gets or sets the bottom-right corner
        /// </summary>
        public string BottomRightCorner { get; set; }

        /// <summary>
        /// Gets or sets the center
        /// </summary>
        public string Center { get; set; }

        /// <summary>
        /// Gets or sets the left
        /// </summary>
        public string Left { get; set; }

        /// <summary>
        /// Gets or sets the middle-center corner
        /// </summary>
        public string MiddleCenterCorner { get; set; }

        /// <summary>
        /// Gets or sets the middle
        /// </summary>
        public char Middle { get; set; }

        /// <summary>
        /// Gets or sets the middle-left corner
        /// </summary>
        public string MiddleLeftCorner { get; set; }

        /// <summary>
        /// Gets or sets the middle-right corner
        /// </summary>
        public string MiddleRightCorner { get; set; }

        /// <summary>
        /// Gets or sets the right
        /// </summary>
        public string Right { get; set; }

        /// <summary>
        /// Gets or sets the top
        /// </summary>
        public char Top { get; set; }

        /// <summary>
        /// Gets or sets the top-center corner
        /// </summary>
        public string TopCenterCorner { get; set; }

        /// <summary>
        /// Gets or sets the top-left corner
        /// </summary>
        public string TopLeftCorner { get; set; }

        /// <summary>
        /// Gets or sets the top-right corner
        /// </summary>
        public string TopRightCorner { get; set; }

        /// <summary>
        /// Gets or sets the upper column span
        /// </summary>
        public string UpperColumnSpan { get; set; }

        /// <summary>
        /// Gets or sets the lower column span
        /// </summary>
        public string LowerColumnSpan { get; set; }

        /// <summary>
        /// Gets or sets the left width
        /// </summary>
        public int LeftWidth { get; set; }

        /// <summary>
        /// Gets or sets the horizontal width
        /// </summary>
        public int HorizontalWidth { get; set; }

        /// <summary>
        /// Gets or sets the center width
        /// </summary>
        public int CenterWidth { get; set; }

        /// <summary>
        /// Gets or sets the right width
        /// </summary>
        public int RightWidth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TableBorderStyle()
        {
        }

        private TableBorderStyle(string tLCorner, char top,
            string tCCorner, string tRCorner, string mLCorner,
            char middle, string mCCorner, string mRCorner,
            string bLCorner, char bottom, string bCCorner,
            string bRCorner, string left, string center,
            string right, string upperColSpan, string lowerColSpan)
        {
            LeftWidth = MaxWidth(tLCorner, mLCorner, bLCorner, left);
            CenterWidth = MaxWidth(tCCorner, mCCorner, bCCorner, center, upperColSpan, lowerColSpan);
            RightWidth = MaxWidth(tRCorner, mRCorner, bRCorner, right);

            TopLeftCorner = AdjustString(tLCorner, LeftWidth);
            Top = top;
            TopCenterCorner = AdjustString(tCCorner, CenterWidth);
            TopRightCorner = AdjustString(tRCorner, RightWidth);
            MiddleLeftCorner = AdjustString(mLCorner, LeftWidth);
            Middle = middle;
            MiddleCenterCorner = AdjustString(mCCorner, CenterWidth);
            MiddleRightCorner = AdjustString(mRCorner, RightWidth);
            BottomLeftCorner = AdjustString(bLCorner, LeftWidth);
            Bottom = bottom;
            BottomCenterCorner = AdjustString(bCCorner, CenterWidth);
            BottomRightCorner = AdjustString(bRCorner, RightWidth);
            Left = AdjustString(left, LeftWidth);
            Center = AdjustString(center, CenterWidth);
            Right = AdjustString(right, RightWidth);
            UpperColumnSpan = AdjustString(upperColSpan, CenterWidth);
            LowerColumnSpan = AdjustString(lowerColSpan, CenterWidth);
        }

        private TableBorderStyle(string customStyle)
        {
            LeftWidth = 1;
            CenterWidth = 1;
            RightWidth = 1;

            TopLeftCorner = Get(customStyle, 0);
            Top = Get(customStyle, 1)[0];
            TopCenterCorner = Get(customStyle, 2);
            TopRightCorner = Get(customStyle, 3);
            MiddleLeftCorner = Get(customStyle, 4);
            Middle = Get(customStyle, 5)[0];
            MiddleCenterCorner = Get(customStyle, 6);
            MiddleRightCorner = Get(customStyle, 7);
            BottomLeftCorner = Get(customStyle, 8);
            Bottom = Get(customStyle, 9)[0];
            BottomCenterCorner = Get(customStyle, 10);
            BottomRightCorner = Get(customStyle, 11);
            Left = Get(customStyle, 12);
            Center = Get(customStyle, 13);
            Right = Get(customStyle, 14);
            UpperColumnSpan = Get(customStyle, 15);
            LowerColumnSpan = Get(customStyle, 16);
        }

        private static string Get(string style, int index)
        {
            if (style == null) return DEFAULT_TILE;
            if (index < 0 || index >= style.Length) return DEFAULT_TILE;
            return style.Substring(index, 1);
        }

        private static int MaxWidth(string a, string b, string c)
        {
            return Math.Max(Math.Max(TileWidth(a), TileWidth(b)), TileWidth(c));
        }

        private static int MaxWidth(string a, string b, string c, string d)
        {
            return Math.Max(Math.Max(Math.Max(TileWidth(a), TileWidth(b)), TileWidth(c)), TileWidth(d));
        }

        private static int MaxWidth(string a, string b, string c, string d, string e, string f)
        {
            return Math.Max(MaxWidth(a, b, c), MaxWidth(d, e, f));
        }

        private static string AdjustString(string txt, int width)
        {
            if (txt == null) return new string(' ', width);
            if (txt.Length == width) return txt;
            if (txt.Length > width) return txt.Substring(0, width);
            return new StringBuilder(txt, width)
                .Append(' ', width - txt.Length)
                .ToString();
        }

        private static int TileWidth(string tile)
        {
            return tile?.Length ?? 0;
        }
    }
}