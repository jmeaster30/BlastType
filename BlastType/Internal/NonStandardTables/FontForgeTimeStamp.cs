using MyLib.Streams;
using Newtonsoft.Json;

namespace BlastType.Internal.NonStandardTables;

// https://fontforge.org/docs/techref/non-standard.html#non-standard-fftm
public class FontForgeTimeStamp : IFontTable
{
    public uint Version { get; set; }
    public long FFTimestamp { get; set; }
    public long CreationDate { get; set; }
    public long LastModifiedDate { get; set; }

    public static FontForgeTimeStamp Load(Stream stream)
    {
        return new FontForgeTimeStamp
        {
            Version = stream.ReadU32(),
            FFTimestamp = ReadS64(stream),
            CreationDate = ReadS64(stream),
            LastModifiedDate = ReadS64(stream),
        };
    }

    public bool Is<T>()
    {
        return typeof(T) == typeof(FontForgeTimeStamp);
    }

    public new string? ToString()
    {
        return JsonConvert.SerializeObject(this);
    }

    private static long ReadS64(Stream stream, bool skipEndianCheck = false)
    {
        var source = stream.ReadBytes(8);
        if (BitConverter.IsLittleEndian && !skipEndianCheck)
            source = source.Reverse().ToArray();
        return BitConverter.ToUInt32(source);
    }
}