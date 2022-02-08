namespace YouReplMe.Themes
{
    internal static  class ThemeProvider
    {
        private const string _ns = "YouReplMe.Themes";

        public static ThemeModel GetTheme(ThemeNames themeNames, string path = null)
        {
            try
            {
                if (themeNames == ThemeNames.Custom)
                {
                    if (File.Exists(path))
                    {
                        return JsonConvert.DeserializeObject<ThemeModel>(File.ReadAllText(path));
                    }
                    return new ThemeModel();
                }
                else
                {
                    path = $"{_ns}.{themeNames.ToString().ToLowerInvariant()}.json";
                    var assembly = typeof(ThemeProvider).Assembly;
                    using (var sr = new StreamReader(assembly.GetManifestResourceStream(path)))
                    {
                        return JsonConvert.DeserializeObject<ThemeModel>(sr.ReadToEnd());
                    }
                }
            }
            catch
            {
                return new ThemeModel();
            }
        }
    }
}