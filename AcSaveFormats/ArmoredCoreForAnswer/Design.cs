using AcSaveFormats.ArmoredCoreForAnswer.Colors;
using AcSaveFormats.ArmoredCoreForAnswer.Decals;
using AcSaveFormats.ArmoredCoreForAnswer.Designs;
using AcSaveFormats.ArmoredCoreForAnswer.Emblems;
using Edoke.IO;
using System;

namespace AcSaveFormats.ArmoredCoreForAnswer
{
    public class Design
    {
        #region Constants

        internal const int FileSizeShiftJIS = 24184;
        internal const int FileSizeUTF16 = 24280;

        #endregion

        #region Properties

        public string DesignName { get; set; }
        public string DesignerName { get; set; }
        public DateTime CreationTimeStamp { get; set; }
        public bool Protect { get; set; }
        public byte Category { get; set; }
        public byte[] UnkC9 { get; private set; }
        public DesignParts Parts { get; set; }
        public DesignTuning Tuning { get; set; }
        public ushort Unk14A { get; set; }
        public AcColorSet Colors { get; set; }
        public DesignDecals Decals { get; set; }
        public Emblem Emblem { get; set; }
        public Thumbnail Thumbnail { get; set; }
        public int Unk5ED4 { get; set; }
        public bool UTF16 { get; set; }
        public bool Xbox { get; set; }

        #endregion

        #region Constructor

        public Design()
        {
            DesignName = "Strayed";
            DesignerName = "Serene Haze";
            CreationTimeStamp = DateTime.Now;
            Protect = false;
            Category = 0;
            UnkC9 = new byte[15];
            Parts = new DesignParts();
            Tuning = new DesignTuning();
            Unk14A = 0;
            Colors = new AcColorSet();
            Decals = new DesignDecals();
            Emblem = new Emblem();
            Thumbnail = new Thumbnail();
            Unk5ED4 = 0;
        }

        public Design(string name, string designerName)
        {
            DesignName = name;
            DesignerName = designerName;
            CreationTimeStamp = DateTime.Now;
            Protect = false;
            Category = 0;
            UnkC9 = new byte[15];
            Parts = new DesignParts();
            Tuning = new DesignTuning();
            Unk14A = 0;
            Colors = new AcColorSet();
            Decals = new DesignDecals();
            Emblem = new Emblem();
            Thumbnail = new Thumbnail();
            Unk5ED4 = 0;
        }

        internal Design(BinaryStreamReader br, bool utf16, bool xbox)
        {
            UTF16 = utf16;
            Xbox = xbox;

            if (utf16)
            {
                DesignName = br.ReadUTF16BigEndian(48 * 2);
                DesignerName = br.ReadUTF16BigEndian(48 * 2);
            }
            else
            {
                DesignName = br.ReadShiftJIS(48);
                DesignerName = br.ReadShiftJIS(48);
            }

            if (xbox)
            {
                long fileTime = br.ReadInt64();

                try
                {
                    CreationTimeStamp = DateTime.FromFileTime(fileTime);
                }
                catch
                {
                    CreationTimeStamp = DateTime.Now;
                }
            }
            else
            {
                long gregorianTime = br.ReadInt64();
                long ticks = gregorianTime * 10;

                try
                {
                    CreationTimeStamp = new DateTime(ticks);
                }
                catch
                {
                    CreationTimeStamp = DateTime.Now;
                }
            }

            byte categoryByte = br.ReadByte();
            Protect = (categoryByte & 0b10000000) != 0;
            Category = (byte)(categoryByte & 0b01111111);
            UnkC9 = br.ReadBytes(15);
            Parts = new DesignParts(br);
            Tuning = new DesignTuning(br);
            Unk14A = br.ReadUInt16();
            Colors = new AcColorSet(br);
            Decals = new DesignDecals(br);
            Emblem = new Emblem(br);
            Thumbnail = new Thumbnail(br, xbox);
            Unk5ED4 = br.ReadInt32();
        }

        #endregion

        #region Read

        public static Design Read(string path)
        {
            using var br = new BinaryStreamReader(path, true);
            return new Design(br, br.Length >= FileSizeUTF16, false);
        }

        public static Design Read(string path, bool utf16)
        {
            using var br = new BinaryStreamReader(path, true);
            return new Design(br, utf16, false);
        }

        public static Design Read(string path, bool utf16, bool xbox)
        {
            using var br = new BinaryStreamReader(path, true);
            return new Design(br, utf16, xbox);
        }

        public static Design Read(byte[] bytes, bool utf16, bool xbox)
        {
            using var br = new BinaryStreamReader(bytes, true);
            return new Design(br, utf16, xbox);
        }

        #endregion

        #region Write

        internal void Write(BinaryStreamWriter bw, bool utf16, bool xbox)
        {
            if (utf16)
            {
                bw.WriteUTF16BigEndian(DesignName, 48, 0);
                bw.WriteUTF16BigEndian(DesignerName, 48, 0);
            }
            else
            {
                bw.WriteShiftJIS(DesignName, 48, 0);
                bw.WriteShiftJIS(DesignerName, 48, 0);
            }

            if (xbox)
            {
                bw.WriteInt64(CreationTimeStamp.ToFileTime());
            }
            else
            {
                long gregorianTime = CreationTimeStamp.Ticks / 10;
                bw.WriteInt64(gregorianTime);
            }

            bw.WriteByte((byte)(((Protect ? 1 : 0) << 7) | Category)); // Combine Protect and Category
            bw.WriteBytes(UnkC9);
            Parts.Write(bw);
            Tuning.Write(bw);
            bw.WriteUInt16(Unk14A);
            Colors.Write(bw);
            Decals.Write(bw);
            Emblem.Write(bw);
            Thumbnail.Write(bw, xbox);
            bw.WriteInt32(Unk5ED4);
        }

        public void Write(string path)
        {
            using var bw = new BinaryStreamWriter(path, true);
            Write(bw, UTF16, Xbox);
        }

        public void Write(string path, bool utf16, bool xbox)
        {
            using var bw = new BinaryStreamWriter(path, true);
            Write(bw, utf16, xbox);
        }

        public byte[] Write()
        {
            var bw = new BinaryStreamWriter(true);
            Write(bw, UTF16, Xbox);
            return bw.FinishBytes();
        }

        public byte[] Write(bool utf16, bool xbox)
        {
            var bw = new BinaryStreamWriter(true);
            Write(bw, utf16, xbox);
            return bw.FinishBytes();
        }

        #endregion

        #region Objects

        public class DesignParts
        {
            #region Properties

            public ushort Head { get; set; }
            public ushort Core { get; set; }
            public ushort Arms { get; set; }
            public ushort Legs { get; set; }
            public ushort Fcs { get; set; }
            public ushort Generator { get; set; }
            public ushort MainBooster { get; set; }
            public ushort BackBooster { get; set; }
            public ushort SideBooster { get; set; }
            public ushort OveredBooster { get; set; }
            public ushort RightArmUnit { get; set; }
            public ushort LeftArmUnit { get; set; }
            public ushort RightBackUnit { get; set; }
            public ushort LeftBackUnit { get; set; }
            public ushort ShoulderUnit { get; set; }
            public ushort RightHangarUnit { get; set; }
            public ushort LeftHangarUnit { get; set; }
            public ushort StabilizerHeadTop { get; set; }
            public ushort StabilizerHeadRight { get; set; }
            public ushort StabilizerHeadLeft { get; set; }
            public ushort StabilizerCoreUpperRight { get; set; }
            public ushort StabilizerCoreUpperLeft { get; set; }
            public ushort StabilizerCoreLowerRight { get; set; }
            public ushort StabilizerCoreLowerLeft { get; set; }
            public ushort StabilizerArmRight { get; set; }
            public ushort StabilizerArmLeft { get; set; }
            public ushort StabilizerLegsBack { get; set; }
            public ushort StabilizerLegsUpperRight { get; set; }
            public ushort StabilizerLegsUpperLeft { get; set; }
            public ushort StabilizerLegsUpperRightBack { get; set; }
            public ushort StabilizerLegsUpperLeftBack { get; set; }
            public ushort StabilizerLegsMiddleRight { get; set; }
            public ushort StabilizerLegsMiddleLeft { get; set; }
            public ushort StabilizerLegsMiddleRightBack { get; set; }
            public ushort StabilizerLegsMiddleLeftBack { get; set; }
            public ushort StabilizerLegsLowerRight { get; set; }
            public ushort StabilizerLegsLowerLeft { get; set; }
            public ushort StabilizerLegsLowerRightBack { get; set; }
            public ushort StabilizerLegsLowerLeftBack { get; set; }

            #endregion

            #region Constructors

            public DesignParts()
            {

            }

            internal DesignParts(BinaryStreamReader br)
            {
                Head = br.ReadUInt16();
                Core = br.ReadUInt16();
                Arms = br.ReadUInt16();
                Legs = br.ReadUInt16();
                Fcs = br.ReadUInt16();
                Generator = br.ReadUInt16();
                MainBooster = br.ReadUInt16();
                BackBooster = br.ReadUInt16();
                SideBooster = br.ReadUInt16();
                OveredBooster = br.ReadUInt16();
                RightArmUnit = br.ReadUInt16();
                LeftArmUnit = br.ReadUInt16();
                RightBackUnit = br.ReadUInt16();
                LeftBackUnit = br.ReadUInt16();
                ShoulderUnit = br.ReadUInt16();
                RightHangarUnit = br.ReadUInt16();
                LeftHangarUnit = br.ReadUInt16();
                StabilizerHeadTop = br.ReadUInt16();
                StabilizerHeadRight = br.ReadUInt16();
                StabilizerHeadLeft = br.ReadUInt16();
                StabilizerCoreUpperRight = br.ReadUInt16();
                StabilizerCoreUpperLeft = br.ReadUInt16();
                StabilizerCoreLowerRight = br.ReadUInt16();
                StabilizerCoreLowerLeft = br.ReadUInt16();
                StabilizerArmRight = br.ReadUInt16();
                StabilizerArmLeft = br.ReadUInt16();
                StabilizerLegsBack = br.ReadUInt16();
                StabilizerLegsUpperRight = br.ReadUInt16();
                StabilizerLegsUpperLeft = br.ReadUInt16();
                StabilizerLegsUpperRightBack = br.ReadUInt16();
                StabilizerLegsUpperLeftBack = br.ReadUInt16();
                StabilizerLegsMiddleRight = br.ReadUInt16();
                StabilizerLegsMiddleLeft = br.ReadUInt16();
                StabilizerLegsMiddleRightBack = br.ReadUInt16();
                StabilizerLegsMiddleLeftBack = br.ReadUInt16();
                StabilizerLegsLowerRight = br.ReadUInt16();
                StabilizerLegsLowerLeft = br.ReadUInt16();
                StabilizerLegsLowerRightBack = br.ReadUInt16();
                StabilizerLegsLowerLeftBack = br.ReadUInt16();
            }

            #endregion

            #region Write

            internal void Write(BinaryStreamWriter bw)
            {
                bw.WriteUInt16(Head);
                bw.WriteUInt16(Core);
                bw.WriteUInt16(Arms);
                bw.WriteUInt16(Legs);
                bw.WriteUInt16(Fcs);
                bw.WriteUInt16(Generator);
                bw.WriteUInt16(MainBooster);
                bw.WriteUInt16(BackBooster);
                bw.WriteUInt16(SideBooster);
                bw.WriteUInt16(OveredBooster);
                bw.WriteUInt16(RightArmUnit);
                bw.WriteUInt16(LeftArmUnit);
                bw.WriteUInt16(RightBackUnit);
                bw.WriteUInt16(LeftBackUnit);
                bw.WriteUInt16(ShoulderUnit);
                bw.WriteUInt16(RightHangarUnit);
                bw.WriteUInt16(LeftHangarUnit);
                bw.WriteUInt16(StabilizerHeadTop);
                bw.WriteUInt16(StabilizerHeadRight);
                bw.WriteUInt16(StabilizerHeadLeft);
                bw.WriteUInt16(StabilizerCoreUpperRight);
                bw.WriteUInt16(StabilizerCoreUpperLeft);
                bw.WriteUInt16(StabilizerCoreLowerRight);
                bw.WriteUInt16(StabilizerCoreLowerLeft);
                bw.WriteUInt16(StabilizerArmRight);
                bw.WriteUInt16(StabilizerArmLeft);
                bw.WriteUInt16(StabilizerLegsBack);
                bw.WriteUInt16(StabilizerLegsUpperRight);
                bw.WriteUInt16(StabilizerLegsUpperLeft);
                bw.WriteUInt16(StabilizerLegsUpperRightBack);
                bw.WriteUInt16(StabilizerLegsUpperLeftBack);
                bw.WriteUInt16(StabilizerLegsMiddleRight);
                bw.WriteUInt16(StabilizerLegsMiddleLeft);
                bw.WriteUInt16(StabilizerLegsMiddleRightBack);
                bw.WriteUInt16(StabilizerLegsMiddleLeftBack);
                bw.WriteUInt16(StabilizerLegsLowerRight);
                bw.WriteUInt16(StabilizerLegsLowerLeft);
                bw.WriteUInt16(StabilizerLegsLowerRightBack);
                bw.WriteUInt16(StabilizerLegsLowerLeftBack);
            }

            #endregion
        }

        public class DesignTuning
        {
            #region Properties

            public byte EnOutput { get; set; }
            public byte EnCapacity { get; set; }
            public byte KpOutput { get; set; }
            public byte Load { get; set; }
            public byte EnWeaponSkill { get; set; }
            public byte Maneuverability { get; set; }
            public byte FiringStability { get; set; }
            public byte AimPrecision { get; set; }
            public byte LockSpeed { get; set; }
            public byte MissileLockSpeed { get; set; }
            public byte RadarRefreshRate { get; set; }
            public byte EcmResistance { get; set; }
            public byte RectificationHead { get; set; }
            public byte RectificationCore { get; set; }
            public byte RectificationArm { get; set; }
            public byte RectificationLeg { get; set; }
            public byte HorizontalThrustMain { get; set; }
            public byte VerticalThrust { get; set; }
            public byte HorizontalThrustSide { get; set; }
            public byte HorizontalThrustBack { get; set; }
            public byte QuickBoostMain { get; set; }
            public byte QuickBoostSide { get; set; }
            public byte QuickBoostBack { get; set; }
            public byte OveredBoostThrust { get; set; }
            public byte TurningAbility { get; set; }
            public byte StabilityHead { get; set; }
            public byte StabilityCore { get; set; }
            public byte StabilityLegs { get; set; }
            public byte Unk1C { get; set; }
            public byte Unk1D { get; set; }
            public byte Unk1E { get; set; }
            public byte Unk1F { get; set; }

            #endregion

            #region Constructors

            public DesignTuning()
            {

            }

            internal DesignTuning(BinaryStreamReader br)
            {
                EnOutput = br.ReadByte();
                EnCapacity = br.ReadByte();
                KpOutput = br.ReadByte();
                Load = br.ReadByte();
                EnWeaponSkill = br.ReadByte();
                Maneuverability = br.ReadByte();
                FiringStability = br.ReadByte();
                AimPrecision = br.ReadByte();
                LockSpeed = br.ReadByte();
                MissileLockSpeed = br.ReadByte();
                RadarRefreshRate = br.ReadByte();
                EcmResistance = br.ReadByte();
                RectificationHead = br.ReadByte();
                RectificationCore = br.ReadByte();
                RectificationArm = br.ReadByte();
                RectificationLeg = br.ReadByte();
                HorizontalThrustMain = br.ReadByte();
                VerticalThrust = br.ReadByte();
                HorizontalThrustSide = br.ReadByte();
                HorizontalThrustBack = br.ReadByte();
                QuickBoostMain = br.ReadByte();
                QuickBoostSide = br.ReadByte();
                QuickBoostBack = br.ReadByte();
                OveredBoostThrust = br.ReadByte();
                TurningAbility = br.ReadByte();
                StabilityHead = br.ReadByte();
                StabilityCore = br.ReadByte();
                StabilityLegs = br.ReadByte();
                Unk1C = br.ReadByte();
                Unk1D = br.ReadByte();
                Unk1E = br.ReadByte();
                Unk1F = br.ReadByte();
            }

            #endregion

            #region Write

            internal void Write(BinaryStreamWriter bw)
            {
                bw.WriteByte(EnOutput);
                bw.WriteByte(EnCapacity);
                bw.WriteByte(KpOutput);
                bw.WriteByte(Load);
                bw.WriteByte(EnWeaponSkill);
                bw.WriteByte(Maneuverability);
                bw.WriteByte(FiringStability);
                bw.WriteByte(AimPrecision);
                bw.WriteByte(LockSpeed);
                bw.WriteByte(MissileLockSpeed);
                bw.WriteByte(RadarRefreshRate);
                bw.WriteByte(EcmResistance);
                bw.WriteByte(RectificationHead);
                bw.WriteByte(RectificationCore);
                bw.WriteByte(RectificationArm);
                bw.WriteByte(RectificationLeg);
                bw.WriteByte(HorizontalThrustMain);
                bw.WriteByte(VerticalThrust);
                bw.WriteByte(HorizontalThrustSide);
                bw.WriteByte(HorizontalThrustBack);
                bw.WriteByte(QuickBoostMain);
                bw.WriteByte(QuickBoostSide);
                bw.WriteByte(QuickBoostBack);
                bw.WriteByte(OveredBoostThrust);
                bw.WriteByte(TurningAbility);
                bw.WriteByte(StabilityHead);
                bw.WriteByte(StabilityCore);
                bw.WriteByte(StabilityLegs);
                bw.WriteByte(Unk1C);
                bw.WriteByte(Unk1D);
                bw.WriteByte(Unk1E);
                bw.WriteByte(Unk1F);
            }

            #endregion
        }

        public class DesignDecals
        {
            public Decal Head { get; set; }
            public Decal Core { get; set; }
            public Decal ArmRight { get; set; }
            public Decal ArmLeft { get; set; }
            public Decal Legs { get; set; }

            public DesignDecals()
            {
                Head = new Decal();
                Core = new Decal();
                ArmRight = new Decal();
                ArmLeft = new Decal();
                Legs = new Decal();
            }

            internal DesignDecals(BinaryStreamReader br)
            {
                Head = new Decal(br);
                Core = new Decal(br);
                ArmRight = new Decal(br);
                ArmLeft = new Decal(br);
                Legs = new Decal(br);
            }

            internal void Write(BinaryStreamWriter bw)
            {
                Head.Write(bw);
                Core.Write(bw);
                ArmRight.Write(bw);
                ArmLeft.Write(bw);
                Legs.Write(bw);
            }
        }

        #endregion
    }
}
