using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Hy.Extensions;

namespace Hy.JsonConverters;

public class NullableDateTimeConverter : JsonConverter<DateTime?>
{
    private readonly string _dateFormatString;
    public NullableDateTimeConverter(string dateFormatString = "yyyy-MM-dd HH:mm:ss")
    {
        _dateFormatString = dateFormatString;
    }
        
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        Debug.Assert(typeToConvert == typeof(DateTime?));
        var dateTimeStr = reader.GetString();
        return dateTimeStr.IsNullOrEmpty() ? null : DateTime.Parse(dateTimeStr);
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString(_dateFormatString, CultureInfo.InvariantCulture));
    }
}