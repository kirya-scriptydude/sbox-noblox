using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
namespace RbxlReader.Chunks;

/// <summary>
/// Binary Reader best suited for reading .rbxl files.
/// 
/// Credits to Roblox-File-Format (MIT) repo. By CloneTrooper1019 (MaximumADHD)
/// </summary>
public class RbxlBinaryReader(Stream stream) : BinaryReader(stream) {

    private byte[] lastStringBuffer = Array.Empty<byte>();

    /// <summary>
    /// credits to CloneTrooper for this code is stole
    /// </summary>
    public T[] ReadInterleaved<T>(int count, Func<byte[], int, T> transform) where T : struct {
        int sizeof_T = Marshal.SizeOf<T>();
        int blobSize = count * sizeof_T;

        var blob = ReadBytes(blobSize);
        var work = new byte[sizeof_T];
        var values = new T[count];

        for (int offset = 0; offset < count; offset++) {
            for (int i = 0; i < sizeof_T; i++) {
                int index = (i * count) + offset;
                work[sizeof_T - i - 1] = blob[index];
            }

            values[offset] = transform(work, 0);
        }

        return values;
    }

    public int RotateInt32(byte[] buffer, int startIndex) {
        int value = BitConverter.ToInt32(buffer, startIndex);
        return (int)((uint)value >> 1) ^ (-(value & 1));
    }

    public long RotateInt64(byte[] buffer, int startIndex) {
        long value = BitConverter.ToInt64(buffer, startIndex);
        return (long)((ulong)value >> 1) ^ (-(value & 1));
    }

    public float RotateFloat(byte[] buffer, int startIndex) {
        uint u = BitConverter.ToUInt32(buffer, startIndex);
        uint i = (u >> 1) | (u << 31);

        byte[] b = BitConverter.GetBytes(i);
        return BitConverter.ToSingle(b, 0);
    }

    public List<int> ReadInstanceIds(int count) {
        int[] values = ReadInterleaved(count, RotateInt32);

        for (int i = 1; i < count; ++i)
            values[i] += values[i - 1];

        return values.ToList();
    }

    /// <summary>
    /// Overriden ReadString()
    /// </summary>
    /// <returns></returns>
    public override string ReadString() {
        int length = ReadInt32();
        byte[] buffer = ReadBytes(length);

        return Encoding.UTF8.GetString(buffer);
    }

    public float ReadFloat() => ReadSingle();

    public byte[] GetLastStringBuffer() {
        return lastStringBuffer;
    }
}