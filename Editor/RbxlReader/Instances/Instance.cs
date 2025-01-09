using RbxlReader.DataTypes;

namespace RbxlReader.Instances;

/// <summary>
/// Instance is a basic building block of a roblox game. Holds various variables with various data-types.
/// </summary>
public partial class Instance {
    public PlaceBinary? Rbxl {get; set;}

    public string ClassName {get; protected set;}
    public string Name {get {
        return property.ContainsKey("Name") ? (string)property["Name"].Value : ClassName;
    }}
    
    private Dictionary<string, InstanceProperty> property;

    public Instance(string className, Dictionary<string, InstanceProperty> props) {
        ClassName = className;
        property = props;
    }

    public Instance(string className) {
        ClassName = className;
        property = new();
    }

    public InstanceProperty? GetProperty(string name) {
        return property.ContainsKey(name) ? property[name] : null;
    }

    public void AddProperty(string name, PropertyType type, object value) {
        property.Add(name,
            new InstanceProperty(type, value)
        );
    }
    public void AddProperty(string name, InstanceProperty prop) {
        property.Add(name, prop);
    }
}