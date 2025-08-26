using AcSaveFormats.ArmoredCoreForAnswer.Options;
using Edoke.IO;
using System;

namespace AcSaveFormats.ArmoredCoreForAnswer
{
    public class OptionsSettings
    {
        #region Properties

        public byte ControlType { get; set; }
        public byte Vibration { get; set; }
        public ushort Unk02 { get; set; }
        public KeySet KeysRegularA { get; set; }
        public KeySet KeysRegularB { get; set; }
        public KeySet KeysSimple { get; set; }
        public AutoOptionFlags AutoFlags { get; set; }
        public byte[] Unk65 { get; private set; }
        public byte Brightness { get; set; }
        public byte Unk85 { get; set; }
        public byte VolumeMusic { get; set; }
        public byte VolumeEffects { get; set; }
        public byte VolumeVoice { get; set; }
        public byte Unk89 { get; set; }
        public byte Unk8A { get; set; }
        public byte Unk8B { get; set; }
        public int Unk8C { get; set; }
        public int Unk90 { get; set; }
        public Radar RadarType { get; set; }
        public int CockpitColorID { get; set; }
        public string Regulation { get; set; }
        public byte[] UnkAC { get; private set; }

        #endregion

        #region Constructors

        public OptionsSettings()
        {
            ControlType = 0;
            Vibration = 100;
            KeysRegularA = KeySet.CreateDefaultRegularA();
            KeysRegularB = KeySet.CreateDefaultRegularB();
            KeysSimple = KeySet.CreateDefaultSimple();
            AutoFlags = AutoOptionFlags.AutoSighting | AutoOptionFlags.AutoBoost | AutoOptionFlags.AutoSwitch;
            Unk65 = new byte[31];
            Brightness = 50;
            VolumeMusic = 80;
            VolumeEffects = 80;
            VolumeVoice = 80;
            RadarType = Radar.Normal;
            CockpitColorID = 0;
            Regulation = "1.20"; // 16 bytes (including null terminator)
            UnkAC = new byte[32];
        }

        internal OptionsSettings(BinaryStreamReader br)
        {
            ControlType = br.ReadByte();
            Vibration = br.ReadByte();
            Unk02 = br.ReadUInt16();
            KeysRegularA = new KeySet(br);
            KeysRegularB = new KeySet(br);
            KeysSimple = new KeySet(br);
            AutoFlags = (AutoOptionFlags)br.ReadByte();
            Unk65 = br.ReadBytes(31);
            Brightness = br.ReadByte();
            Unk85 = br.ReadByte();
            VolumeMusic = br.ReadByte();
            VolumeEffects = br.ReadByte();
            VolumeVoice = br.ReadByte();
            Unk89 = br.ReadByte();
            Unk8A = br.ReadByte();
            Unk8B = br.ReadByte();
            Unk8C = br.ReadInt32();
            Unk90 = br.ReadInt32();
            RadarType = br.ReadEnumInt32<Radar>();
            CockpitColorID = br.ReadInt32();
            Regulation = br.ReadShiftJIS(16);
            UnkAC = br.ReadBytes(32);
        }

        #endregion

        #region Read

        public static OptionsSettings Read(string path)
        {
            using var br = new BinaryStreamReader(path, true);
            return new OptionsSettings(br);
        }

        public static OptionsSettings Read(byte[] bytes)
        {
            using var br = new BinaryStreamReader(bytes, true);
            return new OptionsSettings(br);
        }

        #endregion

        #region Write

        internal void Write(BinaryStreamWriter bw)
        {
            bw.WriteByte(ControlType);
            bw.WriteByte(Vibration);
            bw.WriteUInt16(Unk02);
            KeysRegularA.Write(bw);
            KeysRegularB.Write(bw);
            KeysSimple.Write(bw);
            bw.WriteByte((byte)AutoFlags);
            bw.WriteBytes(Unk65);
            bw.WriteByte(Brightness);
            bw.WriteByte(Unk85);
            bw.WriteByte(VolumeMusic);
            bw.WriteByte(VolumeEffects);
            bw.WriteByte(VolumeVoice);
            bw.WriteByte(Unk89);
            bw.WriteByte(Unk8A);
            bw.WriteByte(Unk8B);
            bw.WriteInt32(Unk8C);
            bw.WriteInt32(Unk90);
            bw.WriteInt32((int)RadarType);
            bw.WriteInt32(CockpitColorID);
            bw.WriteShiftJIS(Regulation, 16, 0);
            bw.WriteBytes(UnkAC);
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

        #region Enums

        [Flags]
        public enum AutoOptionFlags : byte
        {
            None = 0,
            AutoSighting = 1 << 7,
            AutoBoost = 1 << 6,
            AutoSwitch = 1 << 5
        }

        public enum Radar : int
        {
            Classic = 0,
            Normal = 1
        }

        #endregion
    }
}
