using BlastType.Internal.Reusable;
using MyLib.Streams;
using Newtonsoft.Json;

namespace BlastType.Internal;

public class GlyphSubstitutionTable : IFontTable
{
    public ushort MajorVersion { get; set; }
    public ushort MinorVersion { get; set; }
    public ushort ScriptListOffset { get; set; }
    public ushort FeatureListOffset { get; set; }
    public ushort LookupListOffset { get; set; }
    public ushort FeatureVariationsOffset { get; set; }

    public ScriptListTable? ScriptListTable { get; set; }
    public FeatureListTable? FeatureListTable { get; set; }
    public LookupListTable? LookupListTable { get; set; }
    public FeatureVariationsTable? FeatureVariationsTable { get; set; }
    
    public static GlyphSubstitutionTable Load(Stream stream)
    {
        var startOfTable = stream.Position;
        var gsub = new GlyphSubstitutionTable
        {
            MajorVersion = stream.ReadU16(),
            MinorVersion = stream.ReadU16(),
            ScriptListOffset = stream.ReadU16(),
            FeatureListOffset = stream.ReadU16(),
            LookupListOffset = stream.ReadU16(),
        };

        if (gsub.MinorVersion == 1)
        {
            gsub.FeatureVariationsOffset = stream.ReadU16();
        }

        if (gsub.ScriptListOffset != 0)
        {
            stream.Seek(startOfTable + gsub.ScriptListOffset, SeekOrigin.Begin);
            gsub.ScriptListTable = ScriptListTable.Load(stream);
        }
        
        if (gsub.FeatureListOffset != 0)
        {
            stream.Seek(startOfTable + gsub.FeatureListOffset, SeekOrigin.Begin);
            gsub.FeatureListTable = FeatureListTable.Load(stream);
        }
        
        if (gsub.LookupListOffset != 0)
        {
            stream.Seek(startOfTable + gsub.LookupListOffset, SeekOrigin.Begin);
            gsub.LookupListTable = LookupListTable.Load(stream);
        }
        
        if (gsub.FeatureVariationsOffset != 0)
        {
            stream.Seek(startOfTable + gsub.FeatureVariationsOffset, SeekOrigin.Begin);
            gsub.FeatureVariationsTable = FeatureVariationsTable.Load(stream);
        }
        
        return gsub;
    }
    
    public bool Is<T>()
    {
        return typeof(T) == typeof(GlyphPositioningTable);
    }

    public new string? ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}