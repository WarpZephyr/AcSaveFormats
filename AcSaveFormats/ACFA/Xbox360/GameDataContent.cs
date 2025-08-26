using Edoke.IO;
using static AcSaveFormats.ACFA.PlayerData;

namespace AcSaveFormats.ACFA.Xbox360
{
    public class GameDataContent
    {
        #region Properties

        public int Index { get; set; }
        public string LynxName { get; set; }
        public string AcName { get; set; }
        public byte[] UnkA4 { get; private set; }
        public TotalRank Rank { get; set; }
        public byte Complete { get; set; }
        public byte CollaredRank { get; set; }
        public byte OrcaRank { get; set; }
        public int Coam { get; set; }
        public float PlayTimeSeconds { get; set; }

        #endregion

        #region Constructors

        public GameDataContent()
        {
            Index = 0;
            LynxName = DefaultLynxName;
            AcName = DefaultAcName;

            // Seems to be filled with junk data
            UnkA4 = [0x00, 0x00, 0x00, 0x00, 0x8E, 0x65, 0x90, 0x00, 0xFF, 0x24, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                     0x00, 0x00, 0x00, 0x00, 0x3C, 0x14, 0x6D, 0xB6, 0x00, 0xDB, 0x24, 0x90, 0xFF, 0xFF, 0xFF, 0xFF,
                     0x00, 0x00, 0x00, 0x00, 0x8E, 0x65, 0x90, 0x00, 0xFF, 0x24, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                     0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0x92, 0x49, 0x49, 0x24, 0x24, 0x92, 0xFF, 0xFF, 0xFF, 0xFF,
                     0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0x92, 0x49, 0x49, 0x24, 0x24, 0x92, 0xFF, 0xFF, 0xFF, 0xFF,
                     0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0x92, 0x49, 0x49, 0x24, 0x24, 0x92, 0xFF, 0xFF, 0xFF, 0xFF];

            Rank = TotalRank.None;
            Complete = 0;
            CollaredRank = CollaredStartingRank;
            OrcaRank = 0;
            Coam = IndependentStartingCoam;
            PlayTimeSeconds = 0.0f;
        }

        internal GameDataContent(BinaryStreamReader br)
        {
            Index = br.ReadInt32();
            LynxName = br.ReadUTF16BigEndian(32);
            AcName = br.ReadUTF16BigEndian(48);
            UnkA4 = br.ReadBytes(96);
            Rank = br.ReadEnumSByte<TotalRank>();
            Complete = br.ReadByte();
            CollaredRank = br.ReadByte();
            OrcaRank = br.ReadByte();
            Coam = br.ReadInt32();
            PlayTimeSeconds = br.ReadSingle();
        }

        #endregion

        #region Read

        public static GameDataContent Read(string path)
        {
            using var br = new BinaryStreamReader(path, true);
            return new GameDataContent(br);
        }

        public static GameDataContent Read(byte[] bytes)
        {
            using var br = new BinaryStreamReader(bytes, true);
            return new GameDataContent(br);
        }

        #endregion

        #region Write

        internal void Write(BinaryStreamWriter bw)
        {
            bw.WriteInt32(Index);
            bw.WriteUTF16BigEndian(LynxName, 32, 0);
            bw.WriteUTF16BigEndian(AcName, 48, 0);
            bw.WriteBytes(UnkA4);
            bw.WriteSByte((sbyte)Rank);
            bw.WriteByte(Complete);
            bw.WriteByte(CollaredRank);
            bw.WriteByte(OrcaRank);
            bw.WriteInt32(Coam);
            bw.WriteSingle(PlayTimeSeconds);
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
    }
}
