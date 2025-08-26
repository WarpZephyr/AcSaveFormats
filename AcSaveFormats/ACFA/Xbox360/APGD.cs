using Edoke.IO;

namespace AcSaveFormats.ACFA.Xbox360
{
    public class APGD
    {
        #region Properties

        public ushort Unk00 { get; set; }
        public ushort Unk02 { get; set; }
        public ushort Unk04 { get; set; }
        public ushort Unk06 { get; set; }
        public Design Design { get; set; }
        public GameProgress GameProgress { get; set; }
        public OptionsSettings OptionsSettings { get; set; }
        public PlayerData PlayerData { get; set; }

        #endregion

        #region Constructors

        public APGD()
        {
            Unk00 = 1;
            Unk02 = 0;
            Unk04 = 255;
            Unk06 = 37449;
            Design = new Design();
            GameProgress = new GameProgress();
            OptionsSettings = new OptionsSettings();
            PlayerData = new PlayerData();
        }

        public APGD(string name)
        {
            Unk00 = 1;
            Unk02 = 0;
            Unk04 = 255;
            Unk06 = 37449;
            Design = new Design();
            GameProgress = new GameProgress();
            OptionsSettings = new OptionsSettings();
            PlayerData = new PlayerData(name);
        }

        public APGD(Design design, GameProgress gameProgress, OptionsSettings optionsSettings, PlayerData playerData)
        {
            Unk00 = 1;
            Unk02 = 0;
            Unk04 = 255;
            Unk06 = 37449;
            Design = design;
            GameProgress = gameProgress;
            OptionsSettings = optionsSettings;
            PlayerData = playerData;
        }

        internal APGD(BinaryStreamReader br)
        {
            Unk00 = br.ReadUInt16();
            Unk02 = br.ReadUInt16();
            Unk04 = br.ReadUInt16();
            Unk06 = br.ReadUInt16();
            Design = new Design(br, true, true);
            GameProgress = new GameProgress(br);
            OptionsSettings = new OptionsSettings(br);
            PlayerData = new PlayerData(br);
        }

        #endregion

        #region Read

        public static APGD Read(string path)
        {
            using var br = new BinaryStreamReader(path, true);
            return new APGD(br);
        }

        public static APGD Read(byte[] bytes)
        {
            using var br = new BinaryStreamReader(bytes, true);
            return new APGD(br);
        }

        #endregion

        #region Write

        internal void Write(BinaryStreamWriter bw)
        {
            bw.WriteUInt16(Unk00);
            bw.WriteUInt16(Unk02);
            bw.WriteUInt16(Unk04);
            bw.WriteUInt16(Unk06);
            Design.Write(bw, true, true);
            GameProgress.Write(bw);
            OptionsSettings.Write(bw);
            PlayerData.Write(bw);
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
