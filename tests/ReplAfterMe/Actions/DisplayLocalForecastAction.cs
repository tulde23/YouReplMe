using Dependous;
using HtmlAgilityPack;
using Newtonsoft.Json;
using YouReplMe;
using YouReplMe.Actions;
using YouReplMe.Attributes;
using YouReplMe.Contracts;
using YouReplMe.Models;
using YouReplMe.Verbs;

namespace ReplAfterMe.Actions
{
    [NamedDependency(Key)]
    internal class DisplayLocalForecastAction : AbstractAction<DisplayLocalForecastVerb>
    {
        public const string Key = "weather";
        private readonly IPrompt prompt;
        private readonly IConfirmableOperation confirmableOperation;

        public DisplayLocalForecastAction(IHelpTextBuilderFactory helpTextBuilderFactory, IPrompt prompt, IConfirmableOperation confirmableOperation) : base(helpTextBuilderFactory)
        {
            this.prompt = prompt;
            this.confirmableOperation = confirmableOperation;
        }

        public override string ActionName => Key;

        public override CommandHelpText<DisplayLocalForecastVerb> GetHelpTextForVerb(CommandHelpTextBuilder<DisplayLocalForecastVerb> builder)
        {
            return builder
                .Parameters((text, p) => p.FromVerb())
                .CommandUsage(this)
                .CommandName(Key)
                .CommandDescription("get your local forecast by zip code.")
                .Build();
        }

        protected override RegexBuilder<DisplayLocalForecastVerb> BuildRegex(RegexBuilder<DisplayLocalForecastVerb> builder)
        {
            return builder.GroupWithIdentifier(x => x.For, false);
        }

        protected override async Task RunAsync(DisplayLocalForecastVerb verb, CancellationToken cancellationToken)
        {
            var geocoords = await GeocodeZip(verb.For);
            await this.confirmableOperation.ConfirmAsync(() => prompt.Prompt("Continue", "yes"), async () =>
           {
               var lat = geocoords.lat;
               var lon = geocoords.lon;
               var key = "54313fd2b57e211810fd03e9518fd427";
               var endpoint = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={key}&units=imperial";
               using (var client = new HttpClient())
               {
                   var data = await client.GetStringAsync(endpoint);
                   var forecast = JsonConvert.DeserializeObject<WeatherForecast>(data);
                   forecast.Print();
               }
           }
            );
        }

        private async Task<(double lat, double lon)> GeocodeZip(string zipCode)
        {
            var url = "https://www.melissa.com/v2/lookups/geocoder/address/?address=" + zipCode;
            var web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(url);
            var htmlTable = doc.GetElementbyId("tableInfo50");
            double lat = 33.7737;
            double lon = -84.2933;

            return (
                lat, lon
                );
            try
            {
                var tableDataCells = htmlTable.Descendants("td").Select(x => x.InnerText).ToList();

                if (!tableDataCells.Any())
                {
                    return (
                        lat, lon
                        );
                }
                else
                {
                    var data = new Dictionary<string, double>();
                    var keys = new Queue<string>();
                    keys.Enqueue("lat");
                    keys.Enqueue("lon");
                    string currentKey = keys.Dequeue();
                    foreach (var item in tableDataCells)
                    {
                        if (double.TryParse(item, out var value))
                        {
                            data[currentKey] = value;
                            keys.TryDequeue(out currentKey);
                        }
                    }

                    lat = data["lat"];
                    lon = data["lon"];

                    return (lat, lon);
                }
            }
            catch
            {
                return (lat, lon);
            }
        }
    }

    public class DisplayLocalForecastVerb : RegexVerb
    {
        [JsonProperty("for"), Parameter("for", "enter a zip code")]
        public string For { get; set; }
    }

    public class WeatherForecast
    {
        public Coord coord { get; set; }
        public Weather[] weather { get; set; }
        public string _base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }

        public void Print()
        {
            Console.WriteLine($"{"Date:".Primary()} {DateTime.Now}  {name}");
            Console.WriteLine($"{"Current Temp:".Primary()} {main.temp.ToString().Success()}f");
            Console.WriteLine($"{"Hig Temp:".Primary()} {main.temp_max.ToString().Warning()}f");
            Console.WriteLine($"{"Low Temp:".Primary()} {main.temp_min.ToString().Info()}f");

            weather.Tap(x => Console.WriteLine($"{x.description}"));
        }
    }

    public class Coord
    {
        public float lon { get; set; }
        public float lat { get; set; }
    }

    public class Main
    {
        public float temp { get; set; }
        public float feels_like { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
    }

    public class Wind
    {
        public float speed { get; set; }
        public int deg { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
}