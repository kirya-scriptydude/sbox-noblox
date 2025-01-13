using Sandbox;

[Group("Noblox")]
public class RbxlRoot : Component {
    public InstanceComponent[] IdToInstance {get; set;}

    [Property, ReadOnly]
    public int InstanceCount {get; set;}
    [Property, ReadOnly]
    public int ClassCount {get; set;}

    [Property, ReadOnly]
    public int ImportedObjectCount {get; set;}
}