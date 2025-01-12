using Sandbox;

[Group("Noblox Instances")]
public class RbxlPointLightComponent : InstanceComponent {
    [Property, ReadOnly, Group("PointLight")]
    public float Strength {get; set;}

    [Property, ReadOnly, Group("PointLight")]
    public float Range {get; set;}

    public override void ApplyData() {
        var point = Components.GetOrCreate<PointLight>();

        point.Attenuation = Strength;
        point.Radius = Range * 11;
    }
}