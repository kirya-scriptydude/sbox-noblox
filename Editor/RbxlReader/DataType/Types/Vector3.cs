namespace RbxlReader.DataTypes;

public class Vector3 {
    public float X, Y, Z;
    public override string ToString() => $"{X}, {Y}, {Z}";

    public Vector3() {}

    public Vector3(float x, float y, float z) {
        X = x;
        Y = y;
        Z = z;
    }
    
    public Vector3(float[] cords) {
        X = cords[0];
        Y = cords[1];
        Z = cords[2];
    }

    public static Vector3 FromNormalId(NormalId normalId) {
        float[] coords = new float[3] { 0f, 0f, 0f };

        int index = (int)normalId;
        coords[index % 3] = index > 2 ? -1f : 1f;

        return new Vector3(coords);
    }

     public Vector3 Cross(Vector3 other){
        float crossX = Y * other.Z - other.Y * Z;
        float crossY = Z * other.X - other.Z * X;
        float crossZ = X * other.Y - other.X * Y;

        return new Vector3(crossX, crossY, crossZ);
    }
}