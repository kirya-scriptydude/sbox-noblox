using RbxlReader.Chunks;

namespace RbxlReader;

public struct ChunkStruct {
    public META? META;
    /// <summary>
    /// Where key == ClassName
    /// </summary>
    public Dictionary<string, INST> INST = new();
    public List<PROP> PROP = new();
    public PRNT? PRNT;

    public ChunkStruct() {}
}