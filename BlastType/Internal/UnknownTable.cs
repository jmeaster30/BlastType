using MyLib.Streams;
using Newtonsoft.Json;

namespace BlastType.Internal;

public class UnknownTable : IFontTable
{
    public byte[] TableData { get; set; }

    public static UnknownTable Load(Stream stream, uint length)
    {
        return new UnknownTable
        {
            TableData = stream.ReadBytes((int)length), // TODO we should be able to read a uint of bytes
        };
    }
    
    public bool Is<T>()
    {
        return typeof(T) == typeof(UnknownTable);
    }

    public new string? ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}