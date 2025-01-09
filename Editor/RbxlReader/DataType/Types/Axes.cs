namespace RbxlReader.DataTypes;

public enum Axis {
    X,
    Y,
    Z
}
public enum Axes {
    X = 1 << Axis.X,
    Y = 1 << Axis.Y,
    Z = 1 << Axis.Z,
}