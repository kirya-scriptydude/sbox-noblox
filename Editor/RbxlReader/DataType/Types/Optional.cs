namespace RbxlReader.DataTypes;

public struct Optional<T> {
    public T Value;
    public bool HasValue => Value != null;

    public Optional(T obj) {
        Value = obj;
    }
}