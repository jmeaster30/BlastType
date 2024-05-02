using MyLib.Streams;

namespace BlastType.Internal.Reusable;

public class LookupListTable
{
    public ushort LookupCount { get; set; }
    public List<ushort> LookupOffsets { get; set; }

    public List<LookupTable> LookupTables { get; set; }

    public static LookupListTable Load(Stream stream)
    {
        //var startOfLookupListTable = 
        var lookupListTable = new LookupListTable
        {
            LookupCount = stream.ReadU16(),
            LookupOffsets = new List<ushort>(),
        };

        for (var i = 0; i < lookupListTable.LookupCount; i++)
        {
            lookupListTable.LookupOffsets.Add(stream.ReadU16());
        }

        return lookupListTable;
    }
}