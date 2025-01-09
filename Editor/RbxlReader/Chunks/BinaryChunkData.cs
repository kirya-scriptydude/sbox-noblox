using System.Text;
using RbxlReader.Compression;

namespace RbxlReader.Chunks;

/// <summary>
/// Chunk raw data 
/// </summary>
public class BinaryChunkData {
    public PlaceBinary Rbxl;

    public readonly string ChunkName;

    public readonly int Size;
    public readonly int CompressedSize;
    public readonly byte[] Data;
    public readonly byte[] CompressedData;

    public bool IsCompressed => CompressedSize > 0;

    public BinaryChunkData(RbxlBinaryReader reader, PlaceBinary place) {
        Rbxl = place;
        
        byte[] chunkTypeRaw = reader.ReadBytes(4);
        ChunkName = Encoding.ASCII.GetString(chunkTypeRaw);

        CompressedSize = reader.ReadInt32();
        Size = reader.ReadInt32();
        reader.ReadInt32(); //reserved

        if (IsCompressed) {
            var compress = CompressionSingleton.GetInstance();
            CompressedData = reader.ReadBytes(CompressedSize);

            if (BitConverter.ToString(CompressedData, 1, 3) == "B5-2F-FD") {
                //it's zstd
                Data = compress.DecodeZSTD(CompressedData, Size);
            } else {
                //it's lz4
                Data = compress.DecodeLZ4(CompressedData, Size);
            }

        } else {
            Data = reader.ReadBytes(Size);
            CompressedData = new byte[Size];
        }
    }

}