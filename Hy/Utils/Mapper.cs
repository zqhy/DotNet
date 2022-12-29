using System.Data;
using System.Reflection;

namespace Hy.Utils;

// danke schoen https://codereview.stackexchange.com/a/98736
public class DataReaderMapper<T>
{
    private readonly Dictionary<int, Either<FieldInfo, PropertyInfo>> _mappings;
    private readonly bool _isPrimitive;
    public DataReaderMapper(IDataReader reader)
    {
        var u = Nullable.GetUnderlyingType(typeof(T));
        _isPrimitive = typeof(T).IsPrimitive || u is { IsPrimitive: true };
        _mappings = !_isPrimitive ? Mappings(reader) : new Dictionary<int, Either<FieldInfo, PropertyInfo>>();
    }
    
    public class MapMismatchException : Exception
    {
        public MapMismatchException(string arg) : base(arg) { }
    }
    
    private record JoinInfo
    {
        public Either<FieldInfo, PropertyInfo> Info { get; }
        public string Name { get; }
        
        public JoinInfo(Either<FieldInfo, PropertyInfo> info, string name)
        {
            Info = info;
            Name = name;
        }
    }
    // int keys are column indices (ordinals)
    private static Dictionary<int, Either<FieldInfo, PropertyInfo>> Mappings(IDataReader reader)
    {
        var columns = Enumerable.Range(0, reader.FieldCount).ToArray();
        var fieldsAndProps = typeof(T).FieldsAndProps()
            .Select(fp => fp.Match(
                f => new JoinInfo(f, f.Name),
                p => new JoinInfo(p, p.Name)
            ))
            .ToArray();
        var joined = columns
            .Join(fieldsAndProps, reader.GetName, x => x.Name, (index, x) => new
            {
                index,
                info = x.Info
            }, StringComparer.InvariantCultureIgnoreCase).ToList();
        if (columns.Length != joined.Count || fieldsAndProps.Count() != joined.Count)
        {
            throw new MapMismatchException($"""
            Expected to map every column in the result.
            Instead, {columns.Length} columns and {fieldsAndProps.Length} fields produced only {joined.Count} matches.
            Hint: be sure all your columns have _names_, and the names match up.
            """);
        }
        return joined.ToDictionary(x => x.index, x => x.info);
    }

    public T MapFrom(IDataRecord record)
    {
        if (_isPrimitive)
        {
            // Primitive values will always have a single column, indexed by 0
            return (T)record.GetValue(0);
        }
        var element = Activator.CreateInstance<T>();
        foreach (var (key, value) in _mappings)
            value.Match(
                f => f.SetValue(element, ChangeType(record[key], f.FieldType)),
                p => p.SetValue(element, ChangeType(record[key], p.PropertyType))
            );

        return element;
    }

    private static object? ChangeType(object? value, Type targetType)
    {
        if (value == null || value == DBNull.Value)
            return null;

        return targetType.IsEnum 
            ? Enum.ToObject(targetType, value) 
            : Convert.ChangeType(value, Nullable.GetUnderlyingType(targetType) ?? targetType);
    }
}

public static class FieldAndPropsExtension
{
    public static IEnumerable<Either<FieldInfo, PropertyInfo>> FieldsAndProps(this Type t)
    {
        var lst = new List<Either<FieldInfo, PropertyInfo>>();
        lst.AddRange(
            t.GetFields()
                .Select(field => new Either<FieldInfo, PropertyInfo>.Left(field))
        );
        lst.AddRange(
            t.GetProperties()
                .Select(prop => new Either<FieldInfo, PropertyInfo>.Right(prop))
        );
        return lst;
    }
}