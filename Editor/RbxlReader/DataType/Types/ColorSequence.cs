namespace RbxlReader.DataTypes;

public class ColorSequenceKeypoint {
    public float Time;
    public Color3uint8 Value;
    public int Envelope;

    public ColorSequenceKeypoint(int time, Color3uint8 color) {
        Value = color;
        Time = time;
    }
}

public class ColorSequence {
    public ColorSequenceKeypoint[] Keypoints;

    public ColorSequence(ColorSequenceKeypoint[] keys) {
        Keypoints = keys;
    }
}