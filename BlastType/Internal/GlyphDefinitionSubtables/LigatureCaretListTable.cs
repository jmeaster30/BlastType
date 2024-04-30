using MyLib.Streams;
using Newtonsoft.Json;

namespace BlastType.Internal.GlyphDefinitionSubtables;

// Page 214
public class LigatureCaretListTable
{
    public ushort CoverageOffset { get; set; }
    public ushort LigatureGlyphCount { get; set; }
    public List<ushort> LigatureGlyphOffsets { get; set; } // These point to entries in the Item Variation Store in the GDEF section

    public static LigatureCaretListTable Load(Stream stream)
    {
        var ligatureCaretListTable = new LigatureCaretListTable
        {
            CoverageOffset = stream.ReadU16(),
            LigatureGlyphCount = stream.ReadU16(),
            LigatureGlyphOffsets = new List<ushort>()
        };

        for (var i = 0; i < ligatureCaretListTable.LigatureGlyphCount; i++)
        {
            ligatureCaretListTable.LigatureGlyphOffsets.Add(stream.ReadU16());
        }

        return ligatureCaretListTable;
    }

    public new string? ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}