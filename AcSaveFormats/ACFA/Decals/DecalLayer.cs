using AcSaveFormats.ACFA.Emblems;
using Edoke.IO;
using System.Numerics;

namespace AcSaveFormats.ACFA.Decals
{
    public class DecalLayer
    {
        public Emblem Image { get; set; }
        public byte Width { get; set; }
        public byte Height { get; set; }
        public byte Unk86 { get; set; }
        public byte Unk87 { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Position { get; set; }
        public float Scale { get; set; }

        public DecalLayer()
        {
            Image = new Emblem();
        }

        internal DecalLayer(BinaryStreamReader br)
        {
            Image = new Emblem(br);
            Width = br.ReadByte();
            Height = br.ReadByte();
            Unk86 = br.ReadByte();
            Unk87 = br.ReadByte();
            Rotation = br.ReadVector3();
            Position = br.ReadVector3();
            Scale = br.ReadSingle();
        }

        internal void Write(BinaryStreamWriter bw)
        {
            Image.Write(bw);
            bw.WriteByte(Width);
            bw.WriteByte(Height);
            bw.WriteByte(Unk86);
            bw.WriteByte(Unk87);
            bw.WriteVector3(Rotation);
            bw.WriteVector3(Position);
            bw.WriteSingle(Scale);
        }
    }
}
