using RbxlReader.DataTypes;

namespace RbxlReader.Instances;

public class InstanceProperty {
    public PropertyType Type {get;}

    public object Value {get; set;}
    public byte[] RawBuffer = Array.Empty<byte>();
    

    public InstanceProperty(PropertyType type, object value) {
        this.Type = type;
        Value = value;
    }
}