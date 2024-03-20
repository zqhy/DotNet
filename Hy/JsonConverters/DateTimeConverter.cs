using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hy.JsonConverters;

public class DateTimeConverter : JsonConverter<DateTime>
{
    public override bool HandleNull => false;
    
    private readonly string _dateFormatString;
    public DateTimeConverter(string dateFormatString = "yyyy-MM-dd HH:mm:ss")
    {
        _dateFormatString = dateFormatString;
    }
    
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Debug.Assert(typeToConvert == typeof(DateTime));
        var dateTimeStr = reader.GetString();
        return dateTimeStr == null ? default : DateTime.Parse(dateTimeStr);
    }
    
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_dateFormatString, CultureInfo.InvariantCulture));
    }
}