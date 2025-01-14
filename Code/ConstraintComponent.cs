using System;
using Sandbox;

[Group("Noblox")]
public class ConstraintComponent : InstanceComponent {
    public int Part0 {get; set;}
    public int Part1 {get; set;}

    /// <summary>
    /// Override when dealing with different types of constraints (ex. Welds, Ropes and stuff)
    /// </summary>
    public virtual void ConstraintSetup(GameObject part0, GameObject part1) {}

    public override void PostApplyData() {
        //dont do anything when meeting null referent - it will be cleaned after post init.
        if (Part0 == -1 || Part1 == -1) {
            return;
        } 

        var part0 = Root.IdToInstance[Part0];
        var part1 = Root.IdToInstance[Part1];

        if (part0 == null || part1 == null) {
            Log.Warning("Error getting instances for constraints");
            return;
        }
    
        ConstraintSetup(part0.GameObject, part1.GameObject);
        Root.AddToCleanup(GameObject);
    }
}