using Sandbox;

[Group("Noblox Instances")]
public sealed class PartComponent : InstanceComponent {
    
    public static readonly Model Stud = Model.Load("stud.vmdl");

    [Property, ReadOnly, Group("Part")]
    public Vector3 StudPosition {get; set;}
    [Property, ReadOnly, Group("Part")]
    public Vector3 StudRotation {get; set;}
    [Property, ReadOnly, Group("Part")]
    public Vector3 StudSize {get; set;}
    [Property, ReadOnly, Group("Part")]
    public Color BrickColor {get; set;}

    [Property, ReadOnly, Group("Part")]
    public bool CanCollide {get; set;} = true;

    public override void ApplyData() {
        var renderer = GameObject.GetOrAddComponent<ModelRenderer>();
        renderer.Model = Stud;

        LocalPosition = ConvertStudVector(StudPosition);
        WorldScale = StudSize;
        WorldRotation = new Angles(StudRotation);
        renderer.Tint = BrickColor;

        if (CanCollide) {
            var collider = GameObject.GetOrAddComponent<ModelCollider>();
            collider.Model = Stud;
        }
    }

    
} 