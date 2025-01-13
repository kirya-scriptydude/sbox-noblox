using  Sandbox;

/// <summary>
/// Component describing rbxl's Instance
/// </summary>
[Title("Noblox - Instance"), Description("Component describing rbxl's Instance."), Group("Noblox")]
public class InstanceComponent : Component {
    [Property, ReadOnly]
    public int InstanceId {get; set;}

    [Property, ReadOnly]
    public string ClassName {get; set;}
    
    public RbxlRoot Root {get; set;}

    /// <summary>
    /// Applies rbxl data to sbox component. Override me
    /// </summary>
    public virtual void ApplyData() {}

    public virtual void PostApplyData() {}

    public static float ConvertStudFloat(float studs) {
        return studs * 11;
    }
    public static Vector3 ConvertStudVector(Vector3 vector) {
        return new(ConvertStudFloat(vector.x), ConvertStudFloat(vector.y), ConvertStudFloat(vector.z));
    }
}