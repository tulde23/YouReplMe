namespace YouReplMe.Verbs
{
    public class RegexVerb
    {
        [JsonProperty("verbose")]
        public bool? Verbose { get; set; }

        [JsonProperty("whatif")]
        public bool? WhatIf { get; set; }

        public virtual void AfterCreation()
        { }

        public static T Create<T>(IEnumerable<Dictionary<string, string>> keyValuePairs) where T : RegexVerb
        {
            try
            {
                var document = new JObject();
                var options = keyValuePairs.Where(x => x.ContainsKey("options") && x["options"].Length > 1).FirstOrDefault();

                if (options != null)
                {
                    var optionString = options["options"];
                    if (!string.IsNullOrEmpty(optionString?.Trim()))
                    {
                        var tokens = optionString.Split(",".ToCharArray()).Select(x => x.Trim());

                        foreach (var t in tokens)
                        {
                            var keys = t.Split("=".ToCharArray());
                            if (keys.Length == 2)
                            {
                                // keyValuePairs.Add(keys[0], keys[1]);
                                document.Add(new JProperty(keys[0], keys[1]));
                            }
                            else if (keys.Length == 1)
                            {
                                //assume its a flag
                                document.Add(new JProperty(keys[0], true));
                            }
                        }
                    }
                }

                document.Add(new JProperty("commands", new JArray(keyValuePairs.Select(z => JObject.FromObject(z)))));

                var first = keyValuePairs.First();
                foreach (var item in first)
                {
                    document.Add(new JProperty(item.Key, item.Value));
                }

                var model = document.ToObject<T>();
                model.AfterCreation();
                return model;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}