using System.Collections.Generic;
using Sandbox;

public enum PartShape {
    Ball,
    Block,
    Cylinder,
    Wedge,
    CornerWedge
}

[Group("Noblox Instances")]
public sealed class PartComponent : InstanceComponent {

    public static IReadOnlyDictionary<int, Model> ShapeToMdl = new Dictionary<int, Model> {
        {0, Model.Load("stud-ball.vmdl")},
        {1, Model.Load("stud.vmdl")},
        {2, Model.Load("stud-cylinder.vmdl")},
        {3, Model.Load("stud-wedge.vmdl")},
        {4, Model.Load("stud-wedge.vmdl")},
    };


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

    [Property, ReadOnly, Group("Part")]
    public PartShape Shape {get; set;} = PartShape.Block;

    public override void ApplyData() {
        Model mdl = ShapeToMdl[(int)Shape];

        var renderer = GameObject.GetOrAddComponent<ModelRenderer>();
        renderer.Model = mdl;

        LocalPosition = ConvertStudVector(StudPosition);
        WorldScale = StudSize;
        WorldRotation = new Angles(StudRotation);
        renderer.Tint = BrickColor;

        if (CanCollide) {
            var collider = GameObject.GetOrAddComponent<ModelCollider>();
            collider.Model = mdl;
        }
    }

    
} 