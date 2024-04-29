namespace BlastType.Internal.CompactFontFormatSubtables;

public class IndexData
{
    public ushort Count { get; set; }
    public byte OffSize { get; set; }
    public uint Offset { get; set; }

    public List<IIndexDataContent> Contents { get; set; }
}

public interface IIndexDataContent
{
    
}