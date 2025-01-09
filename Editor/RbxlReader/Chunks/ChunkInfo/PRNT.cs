using RbxlReader.Chunks;
using RbxlReader.Instances;

public class PRNT : IChunkInfo {
    public BinaryChunkData Raw {get; set;}

    public byte Version {get; protected set;}
    public int InstanceCount {get; protected set;}

    /// <summary>
    /// Where key - child instance ID, value - parent instance ID
    /// </summary>
    public Dictionary<int, int> ChildParentIds = new();


    public PRNT(BinaryChunkData raw, bool loadNow = true) {
        Raw = raw;

        if (!loadNow) return;
        using (MemoryStream stream = new(raw.Data)) {
            Load(new RbxlBinaryReader(stream));
        }
    }

    public void Load(RbxlBinaryReader reader) {
        Version = reader.ReadByte();
        InstanceCount = reader.ReadInt32();

        List<int> childIds = reader.ReadInstanceIds(InstanceCount);
        List<int> parentIds = reader.ReadInstanceIds(InstanceCount);

        ChildParentIds = new(InstanceCount);

        for (int i = 0; i < InstanceCount; i++) {
            Instance child = Raw.Rbxl.IdToInstance[childIds[i]];
            Instance parent;
            if (parentIds[i] == -1) {
                //null parent (parent is root)
                parent = Raw.Rbxl.Root;
            } else {
                parent = Raw.Rbxl.IdToInstance[parentIds[i]];
            }


            ChildParentIds.Add(childIds[i], parentIds[i]);
            parent.AddChild(child);
        }
    }
}