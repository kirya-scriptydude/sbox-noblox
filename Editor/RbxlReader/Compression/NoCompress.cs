namespace RbxlReader.Compression;

public class NoCompress : ICompressionImplementation {
    public byte[] DecodeLZ4(byte[] src, int targetLen) {
        return src;
    }
    
    public byte[] DecodeZSTD(byte[] src, int targetLen) {
       return src;
    }
}