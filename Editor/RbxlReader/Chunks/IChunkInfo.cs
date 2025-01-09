namespace RbxlReader.Chunks;

public interface IChunkInfo {
    public BinaryChunkData Raw {get; set;}

    /// <summary>
    /// Read data from "Raw" and apply it here.
    /// </summary>
    void Load(RbxlBinaryReader reader);
}