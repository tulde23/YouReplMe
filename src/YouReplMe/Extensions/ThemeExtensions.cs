namespace System
{
    public static class ThemeExtensions
    {
        public static string ColorLight => _themeModel.ColorLight;
        public static string ColorWhite => _themeModel.ColorWhite;
        public static string ColorDark => _themeModel.ColorDark;
        public static string ColorPrimary => _themeModel.ColorPrimary;
        public static string ColorSecondary => _themeModel.ColorSecondary;
        public static string ColorInfo => _themeModel.ColorInfo;
        public static string ColorSuccess => _themeModel.ColorSuccess;
        public static string ColorWarning => _themeModel.ColorWarning;
        public static string ColorDanger => _themeModel.ColorDanger;

        private static ThemeModel _themeModel = new ThemeModel();


        internal  static void Initialize( ThemeModel themeModel)
        {
            _themeModel = themeModel;
        } 

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

        public static ANSIString Danger(this string text)
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