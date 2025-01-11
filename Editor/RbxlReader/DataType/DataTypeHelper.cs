using System.Linq;
using System.Runtime.CompilerServices;
using RbxlReader.Chunks;
using RbxlReader.Instances;

namespace RbxlReader.DataTypes;

public static class DataTypeHelper {
    /// <summary>
    /// PropertyType enum entry to actual C# Type
    /// </summary>
    public static readonly IReadOnlyDictionary<PropertyType, Type> Types = new Dictionary<PropertyType, Type>() {
        // Basics
        {PropertyType.Int, typeof(int)},
        {PropertyType.Float, typeof(float)},
        {PropertyType.Int64, typeof(long)},
        {PropertyType.Double, typeof(double)},
        {PropertyType.String, typeof(string)},
        {PropertyType.Bool, typeof(bool)},

        // Positional
        {PropertyType.Vector2, typeof(Vector2)},
        {PropertyType.Vector3, typeof(Vector3)},
        {PropertyType.Vector3int16, typeof(Vector3int16)},
        {PropertyType.Ray, typeof(Ray)},
        {PropertyType.Rect, typeof(Rect)},
        {PropertyType.UDim, typeof(UDim)},
        {PropertyType.UDim2, typeof(UDim2)},
        {PropertyType.CFrame, typeof(CFrame)},
        {PropertyType.Quaternion, typeof(Quaternion)},

        // Color
        {PropertyType.Color3, typeof(Color3)},
        {(PropertyType)26, typeof(Color3)},
        {PropertyType.BrickColor, typeof(BrickColor)},

        // Sequence
        {PropertyType.NumberSequence, typeof(NumberSequence)},
        {PropertyType.NumberRange, typeof(NumberRange)},

        //  random stuff lol
        {PropertyType.OptionalCFrame, typeof(Optional<CFrame>)},
        {PropertyType.SecurityCapabilities, typeof(ulong)},
        {PropertyType.ProtectedString, typeof(ProtectedString)},
        {PropertyType.SharedString, typeof(SharedString)},

        {PropertyType.Axes, typeof(Axes)},
        {PropertyType.Faces, typeof(Faces)}

    };

    /// <summary>
    /// Property Type whitelist that indicates what properties to look for.
    /// Maybe permanent or maybe temporary idk, dont wanna work on something i wont use.
    /// </summary>
    public static readonly IReadOnlyList<PropertyType> UsedTypes = new List<PropertyType>() {
        PropertyType.String,
        PropertyType.Bool,
        PropertyType.Int,
        PropertyType.Int64,
        PropertyType.Float,
        PropertyType.Double,

        PropertyType.Vector3,
        PropertyType.CFrame,
        
        PropertyType.Color3,
        PropertyType.BrickColor,
        (PropertyType)26
    };

    /// <summary>
    /// Parse the data left in PROP chunk. Need to read PROP header first (Class, PropertyName, etc.) before calling this method.
    /// Writes into props array.
    /// </summary>
    public static void ParsePropertiesInChunk(RbxlBinaryReader reader, PropertyType type, int instCount, InstanceProperty[] props) {
        Type? typeClass = Types[type];


        if (typeClass == null)
            throw new ArgumentNullException($"Type {type} isn't implemented.");

        //  shorthand functions
        var readInts = new Func<int[]>(() => reader.ReadInterleaved(instCount, reader.RotateInt32));
        var readFloats = new Func<float[]>(() => reader.ReadInterleaved(instCount, reader.RotateFloat));

        switch(type) {

            case PropertyType.String:
                readProps(props, instCount, i => {

                    string value = reader.ReadString();
                    
                    byte[] buffer = reader.GetLastStringBuffer();
                    props[i].RawBuffer = buffer;

                    return value;

                });
                break;
            
            case PropertyType.Bool: {
                readProps(props, instCount, i => reader.ReadBoolean());
                break;
            }
            
            case PropertyType.Int: {
                int[] ints = readInts();
                readProps(props, instCount, i => ints[i]);
                break;
            }

            case PropertyType.Int64: {
                long[] ints = reader.ReadInterleaved(instCount, reader.RotateInt64);
                readProps(props, instCount, i => ints[i]);
                break;
            }
            
            case PropertyType.Float: {
                float[] floats = readFloats();
                readProps(props, instCount, i => floats[i]);
                break;
            }
            
            case PropertyType.Double: {
                readProps(props, instCount, i => reader.ReadDouble());
                break;
            }


            case PropertyType.Vector3: {
                float[] Xtable = readFloats(),
                        Ytable = readFloats(),
                        Ztable = readFloats();
                
                readProps(props, instCount, i => {
                    float x = Xtable[i],
                        y = Ytable[i],
                        z = Ztable[i];
                    
                    return new Vector3(x, y, z);
                });

                break;
            }

            case PropertyType.CFrame: {
                float[][] matrices = new float[instCount][];

                for (int i = 0; i < instCount; i++) {
                    byte rawOrientId = reader.ReadByte();
                    

                    if (rawOrientId > 0) {
                        int orientId = (rawOrientId - 1) % 36;
                        var cf = CFrame.FromOrientId(orientId);
                        matrices[i] = cf.GetComponents();

                    } else {

                        float[] matrix = new float[9];

                        for (int v = 0; v < 9; v++) {
                            float value = reader.ReadFloat();
                            matrix[v] = value;
                        }

                        matrices[i] = matrix;
                    }
                }

                float[] cfX = readFloats(),
                        cfY = readFloats(),
                        cfZ = readFloats();
                    
                CFrame[] cframes = new CFrame[instCount];
                for (int i = 0; i < instCount; i++) {
                    float[] matrix = matrices[i];

                    float x = cfX[i],
                        y = cfY[i],
                        z = cfZ[i];

                    float[] components;

                    if (matrix.Length == 12) {
                        matrix[0] = x;
                        matrix[1] = y;
                        matrix[2] = z;

                        components = matrix; 
                    } else {
                        float[] pos = new float[] {x,y,z};
                        components = pos.Concat(matrix).ToArray();
                    }

                    cframes[i] = new(components);
                }

                readProps(props, instCount, i => cframes[i]);
                break;
            }

            case PropertyType.Color3: {
                float[] arrR = readFloats(),
                        arrG = readFloats(),
                        arrB = readFloats();

                readProps(props, instCount, i => {
                    float r = arrR[i],
                        g = arrG[i],
                        b = arrB[i];
                    
                    return new Color3(r, g, b);
                });
                
                break;
            }
            
            case PropertyType.BrickColor: {
                int[] colorIds = readInts();
                readProps(props, instCount, i => {
                    BrickColor color = BrickColor.FromNumber(i);
                    return color;
                });

                break;
            }

            case PropertyType.Color3uint8: {
                byte[] arrR = reader.ReadBytes(instCount),
                    arrG = reader.ReadBytes(instCount),
                    arrB = reader.ReadBytes(instCount);

                readProps(props, instCount, i => {
                    float r = arrR[i],
                        g = arrG[i],
                        b = arrB[i];
                    
                    return new Color3(r, g, b);
            });
                
            break;
            }

            default: {
                break;
            }

        }
    }

    private static void readProps(InstanceProperty[] props, int instCount, Func<int, object> read) {
        for (int i = 0; i < instCount; i++) {
            var prop = props[i];

            if (prop == null)
                continue;
            
            prop.Value = read(i);
        }
    }

    public static bool ContainsUsedTypes(byte num) {
        foreach (PropertyType type in UsedTypes) {
            if ((PropertyType)num == type) {
                return true;
            }
        }

        return false;
    }
}