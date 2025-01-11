using System.Linq;

namespace RbxlReader.Instances;

public partial class Instance {
    public Instance Parent {get {
        if (parentInst == null)
            throw new NullReferenceException("Parent not found, Instance is destroyed");
        
        return parentInst;
    } set {
        parentInst = value;
    }}

    public bool IsDestroyed => parentInst == null;

    public Instance? parentInst;
    private List<Instance> children = new();

    public void AddChild(Instance child) {
        if (Rbxl == null) 
            throw new NullReferenceException();

        children.Add(child);
        child.Parent = this;
    }
    /// <summary>
    /// Remove child. Parents it to root, removing from child list. Does not destroy by default.
    /// </summary>
    public void RemoveChild(Instance child, bool destroyOnRemoval = false) {
        if (Rbxl == null) 
            throw new NullReferenceException();

        children.Remove(child);
        child.Parent = Rbxl.Root;

        if (destroyOnRemoval) child.Destroy();
    }
    /// <summary>
    /// Remove this Instance from the hierarchy entirely by parenting to null.
    /// </summary>
    public void Destroy() {
        parentInst = null;
    }

    public Instance? FindFirstChildOfClass(string className) {
        return children.FirstOrDefault(x => x.ClassName == className);
    }

    public Instance[] GetChildren() => children.ToArray();
}