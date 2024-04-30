using BlastType.Internal.GlyphDefinitionSubtables;
using MyLib.Streams;

namespace BlastType.Internal.Reusable;

public class ClassDefinitionTable
{
    public ushort ClassFormat { get; set; }

    // format1
    public ushort StartGlyphId { get; set; }
    public ushort GlyphCount { get; set; }
    public List<ushort> ClassValueArray { get; set; }

    // format2
    public ushort ClassRangeCount { get; set; }
    public List<ClassRangeRecord> ClassRangeRecords { get; set; }

    public static ClassDefinitionTable Load(Stream stream)
    {
        var classDefinitionTable = new ClassDefinitionTable
        {
            ClassFormat = stream.ReadU16(),
        };

        if (classDefinitionTable.ClassFormat == 1)
        {
            classDefinitionTable.StartGlyphId = stream.ReadU16();
            classDefinitionTable.GlyphCount = stream.ReadU16();
            classDefinitionTable.ClassValueArray = new List<ushort>();
            for (var i = 0; i < classDefinitionTable.GlyphCount; i++)
            {
                classDefinitionTable.ClassValueArray.Add(stream.ReadU16());
            }
        }
        else if (classDefinitionTable.ClassFormat == 2)
        {
            classDefinitionTable.ClassRangeCount = stream.ReadU16();
            classDefinitionTable.ClassRangeRecords = new List<ClassRangeRecord>();
            for (var i = 0; i < classDefinitionTable.ClassRangeCount; i++)
            {
                classDefinitionTable.ClassRangeRecords.Add(ClassRangeRecord.Load(stream));
            }
        }

        return classDefinitionTable;
    }



}