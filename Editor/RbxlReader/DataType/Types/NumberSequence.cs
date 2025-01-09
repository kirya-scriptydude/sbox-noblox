namespace RbxlReader.DataTypes;

public class NumberSequenceKeypoint {
    public float Time;
    public float Value;
    public float Envelope;
}

public class NumberSequence {
    public NumberSequenceKeypoint[] Keypoints;

    public NumberSequence(NumberSequenceKeypoint[] points) {
        Keypoints = points;
    }
}