using Newtonsoft.Json;

namespace BlastType.Internal.KerningSubtables;

//130
public class KerningSubTableFormatTwo : KerningSubTable
{
    public ushort RowWidth { get; set; }
    public ushort LeftClassTable { get; set; }
    public ushort RightClassTable { get; set; }
    public ushort ArrayOffset { get; set; } // from beginning of the subtable 

    public static KerningSubTableFormatTwo Load(Stream stream, ushort version, ushort length, ushort coverage)
    {
        throw new NotImplementedException();
    }
    
    public override bool Is<T>()
    {
        return typeof(T) == typeof(KerningSubTableFormatTwo) || typeof(T) == typeof(KerningSubTable);
    }

    public new string? ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}