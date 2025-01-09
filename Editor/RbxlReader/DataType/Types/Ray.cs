namespace RbxlReader.DataTypes;

public class Ray {
    public Vector3 Origin;
    public Vector3 Direction;

    public Ray(Vector3 origin, Vector3 dir) {
        Origin = origin;
        Direction = dir;
    } 
}