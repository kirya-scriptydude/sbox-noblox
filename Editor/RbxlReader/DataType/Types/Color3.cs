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

    public static Color3 FromRGB(uint r, uint g, uint b) {
        return new Color3(r / 255f, g / 255f, b / 255f);
    }
}