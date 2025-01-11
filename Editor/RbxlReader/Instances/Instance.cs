using RbxlReader.DataTypes;

namespace RbxlReader.Instances;

/// <summary>
/// Instance is a basic building block of a roblox game. Holds various variables with various data-types.
/// </summary>
public partial class Instance {
    public PlaceBinary? Rbxl {get; set;}

    public int Id {get; protected set;}
    public string ClassName {get; protected set;}
    public string Name {get {
        return property.ContainsKey("Name") ? (string)property["Name"].Value : ClassName;
    }}
    
    private Dictionary<string, InstanceProperty> property;

    public Instance(string className, Dictionary<string, InstanceProperty> props, int id = 0) {
        ClassName = className;
        property = props;
        Id = id;
    }

    public Instance(string className, int id = 0) {
        ClassName = className;
        property = new();
        Id = id;
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