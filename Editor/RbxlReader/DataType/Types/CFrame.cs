namespace RbxlReader.DataTypes;

public struct RbxlEulerAngles {
    public float Yaw;
    public float Pitch;
    public float Roll;
}

public class CFrame {
    private readonly float m14, m24, m34;
    private readonly float m11 = 1, m12, m13;
    private readonly float m21, m22 = 1, m23;
    private readonly float m31, m32, m33 = 1;

    private const float m41 = 0, m42 = 0, m43 = 0, m44 = 1;
    public float X => m14;
    public float Y => m24;
    public float Z => m34;


    public Vector3 Position => new Vector3(X, Y, Z);

    public CFrame() {
        m14 = 0;
        m24 = 0;
        m34 = 0;
    }
    public CFrame(Vector3 pos) {
        m14 = pos.X;
        m24 = pos.Y;
        m34 = pos.Z;
    }

    public CFrame(float[] comp) {
        m14 = comp[0]; m24 = comp[1];  m34 = comp[2];
        m11 = comp[3]; m12 = comp[4];  m13 = comp[5];
        m21 = comp[6]; m22 = comp[7];  m23 = comp[8];
        m31 = comp[9]; m32 = comp[10]; m33 = comp[11];
    }

    public RbxlEulerAngles ToEulerAngles() => new RbxlEulerAngles {
        Yaw   = (float)Math.Asin(m13),
        Pitch = (float)Math.Atan2(-m23, m33),
        Roll  = (float)Math.Atan2(-m12, m11),
    };

    public float[] GetComponents() {
        return new float[] {
            m14, m24, m34,
            m11, m12, m13,
            m21, m22, m23,
            m31, m32, m33
        };
    }

    public static CFrame FromOrientId(byte rawOrientId) {
        int orientId = rawOrientId;
        var xColumn = (NormalId)(orientId / 6);
        var yColumn = (NormalId)(orientId % 6);

        var R0 = Vector3.FromNormalId(xColumn);
        var R1 = Vector3.FromNormalId(yColumn);
        var R2 = R0.Cross(R1);

        var matrix = new float[12] {
            0,    0,    0,
            R0.X, R0.Y, R0.Z,
            R1.X, R1.Y, R1.Z,
            R2.X, R2.Y, R2.Z
        };

        return new CFrame(matrix);
    }
}