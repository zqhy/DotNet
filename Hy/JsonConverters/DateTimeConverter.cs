using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hy.JsonConverters;

public class DateTimeConverter : JsonConverter<DateTime>
{
    private readonly string _dateFormatString;
    public DateTimeConverter(string dateFormatString = "yyyy-MM-dd HH:mm:ss")
    {
        _dateFormatString = dateFormatString;
    }
        
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Debug.Assert(typeToConvert == typeof(DateTime));
        return DateTime.Parse(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_dateFormatString, CultureInfo.InvariantCulture));
    }
}