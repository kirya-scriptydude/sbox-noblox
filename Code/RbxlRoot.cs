using System.Collections.Generic;
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

    private List<GameObject> debris = new();

    /// <summary>
    /// add to cleanup list, so it will be deleted. Do not use by itself
    /// </summary>
    public void AddToCleanup(GameObject obj) => debris.Add(obj);

    /// <summary>
    /// Cleans up a debris list.
    /// </summary>
    /// <returns>Instances removed total</returns>
    public int Cleanup() {
        int i = 0;
        foreach(var obj in debris) {
            obj.Destroy();
            i++;
        }

        debris = new List<GameObject>();
        return i;
    }
}