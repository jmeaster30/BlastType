using MyLib.Streams;
using Newtonsoft.Json;

namespace BlastType.Internal.KerningSubtables;

public class KerningSubTableFormatZero : KerningSubTable
{
    public ushort NumberOfPairs { get; set; }
    public ushort SearchRange { get; set; }
    public ushort EntrySelector { get; set; }
    public ushort RangeShift { get; set; }
    public List<KerningSubTableFormatZeroKerningPair> KerningPairs { get; set; } 

    public static KerningSubTableFormatZero Load(Stream stream, ushort version, ushort length, ushort coverage)
    {
        var formatZero = new KerningSubTableFormatZero
        {
            Version = version,
            Length = length,
            Coverage = coverage,
            NumberOfPairs = stream.ReadU16(),
            SearchRange = stream.ReadU16(),
            EntrySelector = stream.ReadU16(),
            RangeShift = stream.ReadU16(),
            KerningPairs = new List<KerningSubTableFormatZeroKerningPair>()
        };

        for (var i = 0; i < formatZero.NumberOfPairs; i++)
        {
            formatZero.KerningPairs.Add(KerningSubTableFormatZeroKerningPair.Load(stream));
        }

        return formatZero;
    }
    
    public override bool Is<T>()
    {
        return typeof(T) == typeof(KerningSubTableFormatZero) || typeof(T) == typeof(KerningSubTable);
    }

    public new string? ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public class KerningSubTableFormatZeroKerningPair
{
    public ushort Left { get; set; }
    public ushort Right { get; set; }
    public short Value { get; set; }

    public static KerningSubTableFormatZeroKerningPair Load(Stream stream)
    {
        return new KerningSubTableFormatZeroKerningPair
        {
            Left = stream.ReadU16(),
            Right = stream.ReadU16(),
            Value = stream.ReadS16(),
        };
    }
}