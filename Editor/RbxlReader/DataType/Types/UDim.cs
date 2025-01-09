namespace RbxlReader.DataTypes;

public class UDim {
    public float Scale;
    public int Offset;

    public UDim() {}

    public UDim(float scale, int offset) {
        Scale = scale;
        Offset = offset;
    }
}