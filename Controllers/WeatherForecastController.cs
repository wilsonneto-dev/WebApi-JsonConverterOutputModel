using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace testjson_converter.Controllers
{
    public class DictionaryCompatibilityWithWCFConverter : JsonConverter<Dictionary<string, string>>
    {

        public override Dictionary<string, string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException("The Dictionary<string, string> Read converter isn't implemented.");
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<string, string> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var key in value.Keys)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("key");
                writer.WriteStringValue(key);
                writer.WritePropertyName("value");
                writer.WriteStringValue(value[key]);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }
    }

    public class SectionViewModel {
        public int Id { get; set; }
        public Dictionary<string, string> Images { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public SectionViewModel Post()
        {
            var dic = new SectionViewModel() {
                Id = 100,
                Images = new Dictionary<string, string> {
                    { "kTeste", "vTeste" },
                    { "kTeste2", "vTeste2" }
                }
            };

            return dic;
        }
        
    }
}
