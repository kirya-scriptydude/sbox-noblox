namespace RbxlReader.DataTypes;

public class Vector2 {
    public float X, Y;
    public override string ToString() => $"{X}, {Y}";

    public Vector2() {}

    public Vector2(float x, float y) {
        X = x;
        Y = y;
    }
}