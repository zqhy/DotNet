using System.Data;
using Hy.Utils;

namespace Hy.Extensions;

public static class DataReaderExtensions
{
    public static IEnumerable<T> To<T>(this IDataReader dataReader)
    {
        var mapper = new DataReaderMapper<T>(dataReader);
        while (dataReader.Read())
        {
            yield return mapper.MapFrom(dataReader);
        }
    }
}