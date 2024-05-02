namespace BlastType.Internal.Reusable;

public enum GlyphSubstitutionLookupType : ushort
{
    Single = 1,
    Multiple,
    Alternate,
    Ligature,
    Context,
    ChainingContext,
    ExtensionSubstitution,
    ReverseChainingContextSingle
}