using MyLib.Streams;

namespace BlastType.Internal.KerningSubtables;

public abstract class KerningSubTable
{
    public ushort Version { get; set; }
    public ushort Length { get; set; }
    public ushort Coverage { get; set; }

    public static KerningSubTable Load(Stream stream)
    {
        var version = stream.ReadU16();
        var length = stream.ReadU16();
        var coverage = stream.ReadU16();

        var format = (byte)((coverage >> 8) & 8);
        return format switch
        {
            0 => KerningSubTableFormatZero.Load(stream, version, length, coverage),
            2 => KerningSubTableFormatTwo.Load(stream, version, length, coverage),
        };
    }

    public bool HasHorizontalData() => (Coverage & 1) == 1;
    public bool HasMinimumValues() => ((Coverage >> 1) & 1) == 1;
    public bool CrossStreamKerning() => ((Coverage >> 2) & 1) == 1;
    public bool Override() => ((Coverage >> 3) & 1) == 1;
    public byte Format() => (byte)((Coverage >> 8) & 8);

    public KerningSubTableFormatZero GetFormatZeroTable() => Format() switch
    {
        0 => (KerningSubTableFormatZero)this,
        _ => throw new InvalidCastException(
            "Cannot cast this instance of KerningSubTable to KerningSubTableFormatZero."),
    };
    
    public KerningSubTableFormatTwo GetFormatTwoTable() => Format() switch
    {
        2 => (KerningSubTableFormatTwo)this,
        _ => throw new InvalidCastException(
            "Cannot cast this instance of KerningSubTable to KerningSubTableFormatTwo."),
    };
    
    public abstract bool Is<T>();
    
}