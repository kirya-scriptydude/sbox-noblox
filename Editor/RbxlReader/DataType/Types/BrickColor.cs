namespace RbxlReader.DataTypes;

/// <summary>
/// todo figure it out
/// </summary>
public class BrickColor {
    public int Number;
    public string Name;
    public Color3 Color;

    private const string DefaultName = "Medium stone grey";
    private const int DefaultNumber = 194;
    
    public BrickColor(Color3 color, string name = DefaultName, int num = DefaultNumber) {
        Name = name;
        Number = num;
        Color = color;
    }
}