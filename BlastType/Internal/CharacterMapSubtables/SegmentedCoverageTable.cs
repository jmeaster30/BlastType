using MyLib.Streams;
using Newtonsoft.Json;

namespace BlastType.Internal.CharacterMapSubtables;

// cmap format 12
public class SegmentedCoverageTable : ICharacterMapSubtable
{
    public ushort Format { get; set; }
    public ushort Reserved { get; set; }
    public uint Length { get; set; }
    public uint Language { get; set; }
    public uint NumGroups { get; set; }
    public List<SequentialMapGroup> SequentialMapGroups { get; set; }

    public static SegmentedCoverageTable Load(Stream stream)
    {
        var segmentedCoverageTable = new SegmentedCoverageTable
        {
            Format = stream.ReadU16(),
            Reserved = stream.ReadU16(),
            Length = stream.ReadU32(),
            Language = stream.ReadU32(),
            NumGroups = stream.ReadU32(),
            SequentialMapGroups = new List<SequentialMapGroup>()
        };

        for (var i = 0; i < segmentedCoverageTable.NumGroups; i++)
        {
            segmentedCoverageTable.SequentialMapGroups.Add(SequentialMapGroup.Load(stream));
        }

        return segmentedCoverageTable;
    }

    public bool Is<T>()
    {
        return typeof(T) == typeof(SegmentedCoverageTable);
    }

    public new string? ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}