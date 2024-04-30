using MyLib.Streams;

namespace BlastType.Internal.Reusable;

public class FeatureListTable
{
    public ushort FeatureCount { get; set; }
    public List<FeatureRecord> FeatureRecords { get; set; }

    public static FeatureListTable Load(Stream stream)
    {
        var featureListTable = new FeatureListTable
        {
            FeatureCount = stream.ReadU16(),
            FeatureRecords = new List<FeatureRecord>(),
        };

        for (var i = 0; i < featureListTable.FeatureCount; i++)
        {
            featureListTable.FeatureRecords.Add(FeatureRecord.Load(stream));
        }

        return featureListTable;
    }
}