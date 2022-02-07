namespace System
{
    public static class StringExtensions

    {
        public static bool EqualsInsensitive(this string source, string other)
        {
            return source.Equals(other, StringComparison.OrdinalIgnoreCase);
        }

        public static IEnumerable<T> SplitAs<T>(this string input, string delimiter = "|")
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(delimiter))
            {
                return Enumerable.Empty<T>();
            }
            return input.Split(delimiter.ToCharArray()).Select(x => (T)Convert.ChangeType(x, typeof(T)));
        }

        public static string AsRegexGroupName( this string s)
        {
            return $"<{s}>";
        }

        public static bool IsFileOrDirectory(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            try
            {
                return File.Exists(input) || Directory.Exists(input);
            }
            catch
            {
                return false;
            }
        }

        public static bool IsPersistentQuery(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            var tokens = input.Split("_".ToCharArray());
            return tokens.Length == 3;
        }

        public static string ExtractSinkFromQueryId(this string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return id;
            }

            var tokens = id.Split("_".ToCharArray());
            return tokens.Length == 3 ? tokens[1] : id;
        }

        /// <summary>
        /// To the camel case.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string ToCamelCase(this string input)
        {
            if (String.IsNullOrEmpty(input) || !Char.IsUpper(input[0]))
            {
                return input;
            }

            var sb = new StringBuilder(input);
            sb[0] = Char.ToLower(sb[0]);
            return sb.ToString();
        }

        public static bool IsTable(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            return input.EndsWith("TABLE", StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsStream(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return input.EndsWith("STREAM", StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsView(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            return input.EndsWith("VIEW", StringComparison.OrdinalIgnoreCase);
        }

        public static string Generate(this string text, int repeat)
        {
            if( repeat == 0)
            {
                return String.Empty;
            }
            return  string.Join("", Enumerable.Range(0, repeat).Select(x => text));
        }
    }
}