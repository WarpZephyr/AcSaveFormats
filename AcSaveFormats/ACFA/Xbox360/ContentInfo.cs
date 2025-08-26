using Edoke.IO;

namespace AcSaveFormats.ACFA.Xbox360
{
    public struct ContentInfo
    {
        #region Properties

        public int Unk00 { get; set; }
        public int ContentSize { get; set; }
        public int InfoSize { get; set; }
        public ushort Unk0C { get; set; }
        public byte Unk0E { get; set; }
        public byte Unk0F { get; set; }

        #endregion

        #region Constructors

        public ContentInfo()
        {
            Unk00 = 0;
            ContentSize = 0;
            InfoSize = 0;
            Unk0C = 2;
            Unk0E = 96;
            Unk0F = 0;
        }

        internal ContentInfo(BinaryStreamReader br)
        {
            Unk00 = br.ReadInt32();
            ContentSize = br.ReadInt32();
            InfoSize = br.ReadInt32();
            Unk0C = br.ReadUInt16();
            Unk0E = br.ReadByte();
            Unk0F = br.ReadByte();
        }

        #endregion

        #region Read

        public static ContentInfo Read(string path)
        {
            using var br = new BinaryStreamReader(path, true);
            return new ContentInfo(br);
        }

        public static ContentInfo Read(byte[] bytes)
        {
            using var br = new BinaryStreamReader(bytes, true);
            return new ContentInfo(br);
        }

        #endregion

        #region Write

        internal readonly void Write(BinaryStreamWriter bw)
        {
            bw.WriteInt32(Unk00);
            bw.WriteInt32(ContentSize);
            bw.WriteInt32(InfoSize);
            bw.WriteUInt16(Unk0C);
            bw.WriteByte(Unk0E);
            bw.WriteByte(Unk0F);
        }

        public readonly void Write(string path)
        {
            using var bw = new BinaryStreamWriter(path, true);
            Write(bw);
        }

        public readonly byte[] Write()
        {
            var bw = new BinaryStreamWriter(true);
            Write(bw);
            return bw.FinishBytes();
        }

        #endregion
    }
}
