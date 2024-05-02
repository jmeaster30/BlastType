using MyLib.Streams;

namespace BlastType.Internal.Reusable;

public class LookupTable
{
    public ushort LookupType { get; set; }
    public ushort LookupFlag { get; set; }
    public ushort SubTableCount { get; set; }
    public List<ushort> LookupSubTableOffsets { get; set; }
    public ushort MarkFilteringSet { get; set; }
    
    // TODO add lookup sub tables here

    public static LookupTable Load(Stream stream)
    {
        var lookupTable = new LookupTable
        {
            LookupType = stream.ReadU16(),
            LookupFlag = stream.ReadU16(),
            SubTableCount = stream.ReadU16(),
        };

        for (var i = 0; i < lookupTable.SubTableCount; i++)
        {
            lookupTable.LookupSubTableOffsets.Add(stream.ReadU16());
        }

        lookupTable.MarkFilteringSet = stream.ReadU16();
        return lookupTable;
    }

    public bool RightToLeft => (LookupFlag & 1) == 1;
    public bool IgnoreBaseGlyphs => ((LookupFlag >> 1) & 1) == 1;
    public bool IgnoreLigatures => ((LookupFlag >> 2) & 1) == 1;
    public bool IgnoreMarks => ((LookupFlag >> 3) & 1) == 1;
    public bool UseMarkFilteringSet => ((LookupFlag >> 4) & 1) == 1;
    public byte MarkAttachmentType => (byte)((LookupFlag >> 8) & 8);
}