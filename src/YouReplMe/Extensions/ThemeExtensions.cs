namespace System
{
    public static class ThemeExtensions
    {
        public const string ColorLight = "#c1e0f8";
        public const string ColorWhite = "#ffffff";
        public const string ColorDark = "#c22d1d";
        public const string ColorPrimary = "#cc4979";
        public const string ColorSecondary = "#576a73";
        public const string ColorInfo = "#6ba24f";
        public const string ColorSuccess = "#34a87b";
        public const string ColorWarning = "#d4d231";
        public const string ColorDanger = "#de471b";

        public static ANSIString Light(this string text)
        {
            return text.Color(ColorLight);
        }

        public static ANSIString White(this string text)
        {
            return text.Color(ColorWhite);
        }

        public static ANSIString Dark(this string text)
        {
            return text.Color(ColorDark);
        }

        public static ANSIString Primary(this string text)
        {
            return text.Color(ColorPrimary);
        }

        public static ANSIString Secondary(this string text)
        {
            return text.Color(ColorSecondary);
        }

        public static ANSIString Info(this string text)
        {
            return text.Color(ColorInfo);
        }

        public static ANSIString Success(this string text)
        {
            return text.Color(ColorSuccess);
        }

        public static ANSIString Warning(this string text)
        {
            return text.Color(ColorWarning);
        }

        public static ANSIString NotFuckingAround(this string text)
        {
            return text.Color(ColorDanger);
        }

        public static ANSIString Light(this ANSIString text)
        {
            return text.Color(ColorLight);
        }

        public static ANSIString White(this ANSIString text)
        {
            return text.Color(ColorWhite);
        }

        public static ANSIString Dark(this ANSIString text)
        {
            return text.Color(ColorDark);
        }

        public static ANSIString Primary(this ANSIString text)
        {
            return text.Color(ColorPrimary);
        }

        public static ANSIString Secondary(this ANSIString text)
        {
            return text.Color(ColorSecondary);
        }

        public static ANSIString Info(this ANSIString text)
        {
            return text.Color(ColorInfo);
        }

        public static ANSIString Success(this ANSIString text)
        {
            return text.Color(ColorSuccess);
        }

        public static ANSIString Warning(this ANSIString text)
        {
            return text.Color(ColorWarning);
        }

        public static ANSIString Danger(this ANSIString text)
        {
            return text.Color(ColorDanger);
        }
    }
}