namespace BlastType.Internal.Reusable;

public enum GlyphPositioningLookupType : ushort
{
    SingleAdjustment = 1,
    PairAdjustment,
    CursiveAttachment,
    MarkToBaseAttachment,
    MarkToLigatureAttachment,
    MarkToMarkAttachment,
    ContextPositioning,
    ChainedContextPositioning,
    ExtensionPositioning,
}