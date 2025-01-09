using System.Text;

namespace RbxlReader.DataTypes;

public class ProtectedString {
    public bool IsCompiled;
    public byte[] RawBuffer;

    public ProtectedString(string value) {
        IsCompiled = false;
        RawBuffer = Encoding.UTF8.GetBytes(value);
    }

    public ProtectedString(byte[] compiled) {
        IsCompiled = true;

        if (compiled.Length > 0)
            if (compiled[0] >= 32)
                IsCompiled = false;

        RawBuffer = compiled;
    }
}