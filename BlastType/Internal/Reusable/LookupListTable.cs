using MyLib.Streams;

namespace BlastType.Internal.Reusable;

public class LookupListTable
{
    public ushort LookupCount { get; set; }
    public List<ushort> Lookups { get; set; }

    public static LookupListTable Load(Stream stream)
    {
        var lookupListTable = new LookupListTable
        {
            LookupCount = stream.ReadU16(),
            Lookups = new List<ushort>(),
        };

        for (var i = 0; i < lookupListTable.LookupCount; i++)
        {
            lookupListTable.Lookups.Add(stream.ReadU16());
        }

        return lookupListTable;
    }
}