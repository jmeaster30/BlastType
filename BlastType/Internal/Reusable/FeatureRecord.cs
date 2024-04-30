using MyLib.Streams;

namespace BlastType.Internal.Reusable;

public class FeatureRecord
{
    public uint FeatureTag { get; set; }
    public ushort FeatureOffset { get; set; } // byte offset to feature table from beginning of feature list... how to make this not depend on the stream once loaded??

    public static FeatureRecord Load(Stream stream)
    {
        return new FeatureRecord
        {
            FeatureTag = stream.ReadU32(),
            FeatureOffset = stream.ReadU16(),
        };
    }
}