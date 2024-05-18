using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace Domain.Converters;

public class LongListToStringConverter : ValueConverter<ICollection<long>, string>
{
    
    public LongListToStringConverter() : base(
        v => JsonConvert.SerializeObject(v),
        v => JsonConvert.DeserializeObject<ICollection<long>>(v)
        ){}
    
}