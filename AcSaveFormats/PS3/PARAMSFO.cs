using Edoke.IO;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace AcSaveFormats.PS3
{
    public class PARAMSFO
    {
        public Dictionary<string, Parameter> Parameters { get; set; }
        public FormatVersion Version { get; set; }

        #region Constructors

        public PARAMSFO()
        {
            Parameters = [];
            Version = new FormatVersion(1, 1, 0, 0);
        }

        internal PARAMSFO(BinaryStreamReader br)
        {
            br.BigEndian = false;
            br.AssertASCII("\0PSF");
            Version = new FormatVersion(br);
            uint keyTableStart = br.ReadUInt32();
            uint dataTableStart = br.ReadUInt32();
            uint tableEntryCount = br.ReadUInt32();

            Parameters = new Dictionary<string, Parameter>((int)tableEntryCount);
            for (int i = 0; i < tableEntryCount; i++)
            {
                _ = new Parameter(br, Parameters, keyTableStart, dataTableStart);
            }
        }

        #endregion

        #region Read

        public static PARAMSFO Read(string path)
        {
            using var br = new BinaryStreamReader(path, true);
            return new PARAMSFO(br);
        }

        public static PARAMSFO Read(byte[] bytes)
        {
            using var br = new BinaryStreamReader(bytes, true);
            return new PARAMSFO(br);
        }

        #endregion

        #region Write

        internal void Write(BinaryStreamWriter bw)
        {
            bw.BigEndian = false;
            bw.WriteASCII("\0PSF", false);
            Version.Write(bw);
            bw.ReserveUInt32("KeyTableStart");
            bw.ReserveUInt32("DataTableStart");

            uint count = (uint)Parameters.Count;
            bw.WriteUInt32(count);

            List<string> keys = new List<string>(Parameters.Keys);
            List<Parameter> parameters = new List<Parameter>(Parameters.Values);

            for (int i = 0; i < count; i++)
            {
                parameters[i].WriteEntry(bw, i);
            }

            long keyTableStart = bw.Position;
            bw.FillUInt32("KeyTableStart", (uint)keyTableStart);
            for (int i = 0; i < count; i++)
            {
                bw.FillUInt16($"KeyOffset_{i}", (ushort)(bw.Position - keyTableStart));
                bw.WriteUTF8(keys[i], true);
            }
            bw.Pad(4);

            long dataTableStart = bw.Position;
            bw.FillUInt32("DataTableStart", (uint)dataTableStart);
            for (int i = 0; i < count; i++)
            {
                bw.FillUInt32($"DataOffset_{i}", (uint)(bw.Position - dataTableStart));
                Parameter parameter = parameters[i];
                DataFormat format = parameter.Format;

                switch (format)
                {
                    case DataFormat.UTF8S:
                        long textStartPos = bw.Position;
                        bw.WriteUTF8(parameter.Data, false);
                        bw.FillUInt32($"DataLength_{i}", (uint)(bw.Position - textStartPos));
                        bw.WritePattern((int)(parameter.DataMaxLength - parameter.Data.Length), 0);
                        break;
                    case DataFormat.UTF8:
                        textStartPos = bw.Position;
                        bw.WriteUTF8(parameter.Data, true);
                        bw.FillUInt32($"DataLength_{i}", (uint)(bw.Position - textStartPos));
                        bw.WritePattern((int)(parameter.DataMaxLength - (parameter.Data.Length + 1)), 0);
                        break;
                    case DataFormat.UInt32:
                        bw.WriteUInt32(uint.Parse(parameter.Data));
                        break;
                    default:
                        throw new InvalidDataException($"{nameof(DataFormat)} {format} is not supported or implemented.");
                }
            }
        }

        public void Write(string path)
        {
            using var bw = new BinaryStreamWriter(path, true);
            Write(bw);
        }

        public byte[] Write()
        {
            var bw = new BinaryStreamWriter(true);
            Write(bw);
            return bw.FinishBytes();
        }

        #endregion

        #region Is

        internal static bool Is(BinaryStreamReader br)
        {
            br.BigEndian = true;
            if (br.Length < 20)
            {
                return false;
            }

            string magic = br.GetASCII(0, 4);
            return magic == "\0PSF";
        }

        public static bool Is(string path)
        {
            using var br = new BinaryStreamReader(path, true);
            return Is(br);
        }

        public static bool Is(byte[] bytes)
        {
            using var br = new BinaryStreamReader(bytes, true);
            return Is(br);
        }

        #endregion

        #region Is Read

        public static bool IsRead(string path, [NotNullWhen(true)] PARAMSFO? result)
        {
            using var br = new BinaryStreamReader(path, true);
            if (Is(br))
            {
                result = new PARAMSFO(br);
                return true;
            }

            result = null;
            return false;
        }

        public static bool IsRead(byte[] bytes, [NotNullWhen(true)] PARAMSFO? result)
        {
            using var br = new BinaryStreamReader(bytes, true);
            if (Is(br))
            {
                result = new PARAMSFO(br);
                return true;
            }

            result = null;
            return false;
        }

        #endregion

        #region Structs

        public class Parameter
        {
            public string Data { get; set; }
            public DataFormat Format { get; set; }
            public uint DataMaxLength { get; set; }

            #region Constructors

            public Parameter(string value)
            {
                Data = value;
                Format = DataFormat.UTF8;
            }

            public Parameter(string value, DataFormat dataFormat)
            {
                Data = value;
                Format = dataFormat;
                DataMaxLength = (uint)value.Length;
            }

            public Parameter(string value, DataFormat dataFormat, uint dataMaxLength)
            {
                Data = value;
                Format = dataFormat;
                DataMaxLength = dataMaxLength;
            }

            internal Parameter(BinaryStreamReader br, Dictionary<string, Parameter> dictionary, uint keyTableStart, uint dataTableStart)
            {
                ushort keyOffset = br.ReadUInt16();
                Format = br.ReadEnum16<DataFormat>();
                uint dataLength = br.ReadUInt32();
                DataMaxLength = br.ReadUInt32();
                uint dataOffset = br.ReadUInt32();

                long end = br.Position;
                br.Position = keyTableStart + keyOffset;
                string key = br.ReadUTF8();

                br.Position = dataTableStart + dataOffset;
                switch (Format)
                {
                    case DataFormat.UTF8S:
                        Data = br.ReadUTF8((int)dataLength);
                        break;
                    case DataFormat.UTF8:
                        Data = br.ReadUTF8();
                        break;
                    case DataFormat.UInt32:
                        Data = br.ReadUInt32().ToString();
                        break;
                    default:
                        throw new InvalidDataException($"{nameof(DataFormat)} {Format} is not supported or implemented.");
                }

                dictionary.Add(key, this);
                br.Position = end;
            }

            #endregion

            #region Write

            internal void WriteEntry(BinaryStreamWriter bw, int index)
            {
                bw.ReserveUInt16($"KeyOffset_{index}");
                bw.WriteUInt16((ushort)Format);

                if (Format == DataFormat.UInt32)
                {
                    bw.WriteUInt32(4);
                    bw.WriteUInt32(4);
                }
                else
                {
                    bw.ReserveUInt32($"DataLength_{index}");
                    bw.WriteUInt32(DataMaxLength);
                }

                bw.ReserveUInt32($"DataOffset_{index}");
            }

            #endregion
        }

        public struct FormatVersion
        {
            public byte Major { get; set; }
            public byte Minor { get; set; }
            public byte Unk03 { get; set; }
            public byte Unk04 { get; set; }

            #region Constructors

            public FormatVersion()
            {
                Major = 1;
                Minor = 1;
                Unk03 = 0;
                Unk04 = 0;
            }

            public FormatVersion(byte major, byte minor)
            {
                Major = major;
                Minor = minor;
                Unk03 = 0;
                Unk04 = 0;
            }

            public FormatVersion(byte major, byte minor, byte unk03, byte unk04)
            {
                Major = major;
                Minor = minor;
                Unk03 = unk03;
                Unk04 = unk04;
            }

            internal FormatVersion(BinaryStreamReader br)
            {
                Major = br.ReadByte();
                Minor = br.ReadByte();
                Unk03 = br.ReadByte();
                Unk04 = br.ReadByte();
            }

            #endregion

            #region Write

            internal void Write(BinaryStreamWriter bw)
            {
                bw.WriteByte(Major);
                bw.WriteByte(Minor);
                bw.WriteByte(Unk03);
                bw.WriteByte(Unk04);
            }

            #endregion
        }

        #endregion

        #region Enums

        public enum DataFormat : ushort
        {
            /// <summary>
            /// UTF8 without null termination.
            /// </summary>
            UTF8S = 0x0004,

            /// <summary>
            /// UTF8.
            /// </summary>
            UTF8 = 0x0204,

            /// <summary>
            /// UInt32.
            /// </summary>
            UInt32 = 0x0404
        }

        #endregion
    }
}
