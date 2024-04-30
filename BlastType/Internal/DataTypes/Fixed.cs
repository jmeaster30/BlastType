using MyLib.Bytes;
using MyLib.Enumerables;

namespace BlastType.Internal.DataTypes;

public class Fixed
{
    private int _integerSize;
    private int _fractionalSize;

    private int _integerPart;
    private int _fractionalPart;

    public decimal Value => ToDecimal();

    public static Fixed FromBytes(int integerSize, int fractionalSize, byte[] bytes)
    {
        var bitList = bytes.ToBitList();
        var integerPart = bitList.ReadBitsAt(0, integerSize).AsEnumerable().PadLeft(4, (byte)0);
        //?? Should this function be agnostic to endianness or is this useful?
        if (BitConverter.IsLittleEndian)
        {
            integerPart = integerPart.Reverse();
        }
        var fractionalPart = bitList.ReadBitsAt(integerSize, fractionalSize).AsEnumerable().PadLeft(4, (byte)0);
        //?? Should this function be agnostic to endianness or is this useful?
        if (BitConverter.IsLittleEndian)
        {
            fractionalPart = fractionalPart.Reverse();
        }
        return new Fixed
        {
            _integerSize = integerSize,
            _fractionalSize = fractionalSize,
            _integerPart = BitConverter.ToInt32(integerPart.ToArray()),
            _fractionalPart = BitConverter.ToInt32(fractionalPart.ToArray())
        };
    }

    public byte[] ToBytes()
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return ToDecimal().ToString();
    }

    private decimal ToDecimal()
    {
        return _integerPart + _fractionalPart / (decimal)(1 << _fractionalSize);
    }
}