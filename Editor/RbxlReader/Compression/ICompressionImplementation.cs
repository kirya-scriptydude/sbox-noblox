namespace RbxlReader.Compression;

public interface ICompressionImplementation {
    public byte[] DecodeLZ4(byte[] src, int targetLen);
    public byte[] DecodeZSTD(byte[] src, int targetLen);
}