using System.Text;
namespace RbxlReader.Chunks;

public class META : IChunkInfo {
    public BinaryChunkData Raw {get; set;}
    public Dictionary<string, string> Table = new();

    public META(BinaryChunkData raw, bool loadNow = true) {
        Raw = raw;

        if (!loadNow) return;
        using (MemoryStream stream = new(raw.Data)) {
            Load(new RbxlBinaryReader(stream));
        }
    }

    public void Load(RbxlBinaryReader reader) {
        int count = reader.ReadInt32();

        for (int i = 0; i > count; i++) {
            string key = reader.ReadString();
            string value = reader.ReadString();
            Table.Add(key, value);
        }
    }
}