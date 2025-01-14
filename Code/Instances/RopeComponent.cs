using Sandbox;

[Group("Noblox Instances")]
public class RopeComponent : ConstraintComponent {

    [Property, ReadOnly]
    public int Length {get; set;} 

    public override void ConstraintSetup(GameObject part0, GameObject part1) {
        if (part0.GetComponent<InstanceComponent>().ClassName == "Attachment") {
            part0 = part0.Parent;
        }
        if (part1.GetComponent<InstanceComponent>().ClassName == "Attachment") {
            part1 = part1.Parent;
        }

        var joint = part0.Components.GetOrCreate<SpringJoint>();
        joint.MaxLength = Length;
        joint.Body = part1;
    }
}