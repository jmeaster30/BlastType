using BlastType.Internal.CompactFontFormatSubtables;
using MyLib.Enumerables;
using MyLib.Streams;
using Newtonsoft.Json;

namespace BlastType.Internal;

public class CompactFontFormatTable : IFontTable
{
    public CompactFontFormatHeader Header { get; set; }

    public static CompactFontFormatTable Load(Stream stream)
    {
        var offset = stream.Position;
        
        var header = CompactFontFormatHeader.Load(stream);
        // TODO there are more pieces to this data structure

        stream.Seek(offset, SeekOrigin.Begin);
        Console.WriteLine(stream.ReadBytes(50).Select(x => x.ToString("X2")).Join(" "));
        
        return new CompactFontFormatTable
        {
            Header = header
        };
    }
    
    public bool Is<T>()
    {
        return typeof(T) == typeof(CompactFontFormatTable);
    }
    
    public new string? ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}