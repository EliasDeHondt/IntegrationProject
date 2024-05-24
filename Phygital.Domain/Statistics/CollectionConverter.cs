namespace Domain.Statistics;

using FileHelpers;
using System.Collections.Generic;
using System.Text;

public class CollectionConverter<T> : ConverterBase
{
    public override string FieldToString(object from)
    {
        var collection = (ICollection<T>)from;
        if (collection == null || collection.Count == 0)
            return string.Empty;

        var sb = new StringBuilder();
        foreach (var item in collection)
        {
            sb.Append(item.ToString()); 
            sb.Append("|");
        }
        sb.Length--; // laatste weg
        return sb.ToString();
    }

    public override object StringToField(string from)
    {
        if (string.IsNullOrEmpty(from))
            return null;

        var items = from.Split('|');
        var collection = new List<T>();
        foreach (var item in items)
        {
            collection.Add((T)System.Convert.ChangeType(item, typeof(T)));
        }
        return collection;
    }
}
