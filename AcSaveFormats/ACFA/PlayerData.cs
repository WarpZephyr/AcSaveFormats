using Edoke.IO;

namespace AcSaveFormats.ACFA
{
    public class PlayerData
    {
        #region Constants

        public const string DefaultLynxName = "Unknown";
        public const string DefaultAcName = "Strayed";
        public const byte CollaredStartingRank = 31;
        public const byte OrcaStartingRank = 13;
        public const int IndependentStartingCoam = 65000;

        #endregion

        #region Properties

        public string LynxName { get; set; }
        public TotalRank Rank { get; set; }
        public byte Completed { get; set; }
        public byte CollaredRank { get; set; }
        public byte OrcaRank { get; set; }
        public int Coam { get; set; }
        public float PlayTimeSeconds { get; set; }

        #endregion

        #region Constructor

        public PlayerData()
        {
            LynxName = DefaultLynxName;
            Rank = TotalRank.None;
            CollaredRank = CollaredStartingRank;
            OrcaRank = 0;
            Coam = IndependentStartingCoam;
            PlayTimeSeconds = 0.0f;
        }

        public PlayerData(string name)
        {
            LynxName = name;
            Rank = TotalRank.None;
            CollaredRank = CollaredStartingRank;
            OrcaRank = 0;
            Coam = IndependentStartingCoam;
            PlayTimeSeconds = 0.0f;
        }

        internal PlayerData(BinaryStreamReader br)
        {
            LynxName = br.ReadUTF16BigEndian(32);
            Rank = br.ReadEnumSByte<TotalRank>();
            Completed = br.ReadByte();
            CollaredRank = br.ReadByte();
            OrcaRank = br.ReadByte();
            Coam = br.ReadInt32();
            PlayTimeSeconds = br.ReadSingle();
            br.AssertPattern(256, 0);
        }

        #endregion

        #region Read

        public static PlayerData Read(string path)
        {
            using var br = new BinaryStreamReader(path, true);
            return new PlayerData(br);
        }

        public static PlayerData Read(byte[] bytes)
        {
            using var br = new BinaryStreamReader(bytes, true);
            return new PlayerData(br);
        }

        #endregion

        #region Write

        internal void Write(BinaryStreamWriter bw)
        {
            bw.WriteUTF16BigEndian(LynxName, 32, 0);
            bw.WriteSByte((sbyte)Rank);
            bw.WriteByte(Completed);
            bw.WriteByte(CollaredRank);
            bw.WriteByte(OrcaRank);
            bw.WriteInt32(Coam);
            bw.WriteSingle(PlayTimeSeconds);
            bw.WritePattern(256, 0);
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

        public enum TotalRank : sbyte
        {
            None = -1,
            E = 0,
            D = 1,
            C = 2,
            B = 3,
            A = 4,
            S = 5,
            SS = 6
        }

        #endregion
    }
}
