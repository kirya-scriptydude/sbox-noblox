namespace RbxlReader.DataTypes;

public class Color3 {
    public float R, G, B;
    public override string ToString() => $"{R}, {G}, {B}";

    public Color3() {}

    public Color3(float r, float g, float b) {
        R = r;
        G = g;
        B = b;
    }
}