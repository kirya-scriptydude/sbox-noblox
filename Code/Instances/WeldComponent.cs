using Sandbox;

[Group("Noblox Instances")]
public class WeldComponent : ConstraintComponent {
    public override void ConstraintSetup(GameObject part0, GameObject part1) {
        var joint = part0.Components.Create<FixedJoint>();
        joint.Body = part1;
    }
}