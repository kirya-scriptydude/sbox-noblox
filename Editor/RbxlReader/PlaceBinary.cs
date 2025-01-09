#pragma warning disable SYSLIB0001 // Type or member is obsolete. Need to be using UTF-7

using System.ComponentModel.Design.Serialization;
using System.Text;
using RbxlReader.Chunks;
using RbxlReader.Instances;
namespace RbxlReader;

/// <summary>
/// Low-level .rbxl file representation
/// </summary>
public class PlaceBinary {

    public static readonly string MAGIC_HEADER = "<roblox!\x89\xff\x0d\x0a\x1a\x0a";

    public ushort Version {get; protected set;}
    public int NumberClasses {get; protected set;}
    public int NumberInstances {get; protected set;}
    public long Reserved {get; protected set;}

    /// <summary>
    /// Raw chunk data
    /// </summary>
    public List<BinaryChunkData> Chunks = new();
    /// <summary>
    /// ChunkInfo instance struct, containing chunk information such as INST classes, or PROP properties.
    /// </summary>
    public ChunkStruct ChunkInfo = new();

    /// <summary>
    /// Empty Instance that serves as a root for place's entire hierarchy. Throws an error if you try to reference Root's parent (does not exist)
    /// </summary>
    public Instance Root {get;} = new("Root");
    public Instance? Workspace => Root.FindFirstChildOfClass("Workspace");
    
    public INST[] IdToINST {get; protected set;}
    public Instance[] IdToInstance {get; protected set;}

    /// <summary>
    /// Create class and parse from file
    /// </summary>
    /// <param name="path"></param>
    public PlaceBinary(string path) {
        Root.Rbxl = this;

        using (FileStream file = File.Open(path, FileMode.Open)) {
            RbxlBinaryReader reader = new(file);
            parseHeader(file, reader);

            IdToINST = new INST[NumberClasses];
            IdToInstance = new Instance[NumberInstances];

            // parse chunks
            bool endReached = false;
            while (!endReached) {
                BinaryChunkData chunk = new(reader, this);
                Chunks.Add(chunk);

                evaluateChunk(chunk);

                if (chunk.ChunkName == "END\0") endReached = true;
            }
        }
    }

    /// <summary>
    /// Create empty PlaceBinary
    /// </summary>
    public PlaceBinary() {
        Root.Rbxl = this;

        IdToINST = new INST[1];
        IdToInstance = new Instance[1];
    }

    private void parseHeader(Stream stream, RbxlBinaryReader reader) {
        byte[] signature = reader.ReadBytes(14);
        string signatureString = Encoding.UTF7.GetString(signature);

        if (signatureString != MAGIC_HEADER) throw new InvalidDataException("Data signature does not match file header!");

        Version = reader.ReadUInt16();
        NumberClasses = reader.ReadInt32();
        NumberInstances = reader.ReadInt32();
        Reserved = reader.ReadInt64();
    }

    private IChunkInfo? evaluateChunk(BinaryChunkData chunk) {
        switch(chunk.ChunkName) {
            case "META":
                META info = new(chunk);
                ChunkInfo.META = info;
                return info;
            
            case "INST":
                INST inst = new(chunk);
                ChunkInfo.INST.Add(inst.ClassName, inst);

                IdToINST[inst.Index] = inst;

                foreach (KeyValuePair<int, Instance> keyval in inst.LinkedInstances) {
                    IdToInstance[keyval.Key] = keyval.Value;

                    keyval.Value.Rbxl = this;
                }
                
                return inst;

            case "PROP":
                PROP propchunk = new(chunk);
                ChunkInfo.PROP.Add(propchunk);
                return propchunk;

            case "PRNT":
                PRNT parent = new(chunk);
                ChunkInfo.PRNT = parent;
                return parent;

            default:
                return null;
        }
    }
}