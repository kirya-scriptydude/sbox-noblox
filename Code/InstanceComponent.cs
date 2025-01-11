using  Sandbox;

/// <summary>
/// Component describing rbxl's Instance
/// </summary>
[Title("Noblox - Instance"), Description("Component describing rbxl's Instance.")]
public class InstanceComponent : Component {
    [Property, ReadOnly]
    public int InstanceId {get; set;}

    [Property, ReadOnly]
    public string ClassName {get; set;}


}