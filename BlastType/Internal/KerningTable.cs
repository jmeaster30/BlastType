using BlastType.Internal.KerningSubtables;
using MyLib.Streams;
using Newtonsoft.Json;

namespace BlastType.Internal;

public class KerningTable : IFontTable
{
    public ushort Version { get; set; }
    public ushort NumberOfTables { get; set; }
    public List<KerningSubTable> SubTables { get; set; }

    public static KerningTable Load(Stream stream)
    {
        var kerningTable = new KerningTable
        {
            Version = stream.ReadU16(),
            NumberOfTables = stream.ReadU16(),
            SubTables = new List<KerningSubTable>(),
        };

        for (var i = 0; i < kerningTable.NumberOfTables; i++)
        {
            kerningTable.SubTables.Add(KerningSubTable.Load(stream));
        }

        return kerningTable;
    }

    public bool Is<T>()
    {
        return typeof(T) == typeof(KerningTable);
    }

    public new string? ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}