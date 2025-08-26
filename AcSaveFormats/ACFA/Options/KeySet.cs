using Edoke.IO;

namespace AcSaveFormats.ACFA.Options
{
    public class KeySet
    {
        #region Properties

        public Key LookUp { get; set; }
        public Key LookDown { get; set; }
        public Key ResetView { get; set; }
        public Key LookRight { get; set; }
        public Key LookLeft { get; set; }
        public Key Boost { get; set; }
        public Key OveredBoost { get; set; }
        public Key QuickBoost { get; set; }
        public Key UseRight { get; set; }
        public Key UseLeft { get; set; }
        public Key UseShoulder { get; set; }
        public Key SwitchRight { get; set; }
        public Key SwitchLeft { get; set; }
        public Key PurgeRight { get; set; }
        public Key PurgeLeft { get; set; }
        public Key PurgeShoulder { get; set; }
        public Key ReloadRight { get; set; }
        public Key ReloadLeft { get; set; }
        public Key LockOn { get; set; }
        public Key SwitchLock { get; set; }
        public Key Pause { get; set; }
        public Key Forward { get; set; }
        public Key Backward { get; set; }
        public Key Right { get; set; }
        public Key Left { get; set; }
        public Key AssaultArmor { get; set; }
        public byte ControlType { get; set; }
        public byte Unk1B { get; set; }
        public int Unk1C { get; set; }

        #endregion

        #region Constructors

        public KeySet()
        {
            // Simple Configuration
            // Terrible but is the default
            LookUp = Key.RightStickUpArrow;
            LookDown = Key.RightStickDownArrow;
            ResetView = Key.R3;
            LookRight = Key.RightStickRightArrow;
            LookLeft = Key.RightStickLeftArrow;
            Boost = Key.L2;
            OveredBoost = Key.L1;
            QuickBoost = Key.R2;
            UseRight = Key.X;
            UseLeft = Key.Square;
            UseShoulder = Key.R1;
            SwitchRight = Key.Circle;
            SwitchLeft = Key.Triangle;
            PurgeRight = Key.Compound;
            PurgeLeft = Key.Compound;
            PurgeShoulder = Key.Compound;
            ReloadRight = Key.Compound;
            ReloadLeft = Key.Compound;
            LockOn = Key.L3;
            SwitchLock = Key.Compound;
            Pause = Key.Start;
            Forward = Key.LeftStickUpArrow;
            Backward = Key.LeftStickDownArrow;
            Right = Key.Unassigned;
            Left = Key.Unassigned;
            AssaultArmor = Key.Compound;
            ControlType = 0;
            Unk1B = 192;
            Unk1C = 0;
        }

        internal KeySet(BinaryStreamReader br)
        {
            LookUp = br.ReadEnumByte<Key>();
            LookDown = br.ReadEnumByte<Key>();
            ResetView = br.ReadEnumByte<Key>();
            LookRight = br.ReadEnumByte<Key>();
            LookLeft = br.ReadEnumByte<Key>();
            Boost = br.ReadEnumByte<Key>();
            OveredBoost = br.ReadEnumByte<Key>();
            QuickBoost = br.ReadEnumByte<Key>();
            UseRight = br.ReadEnumByte<Key>();
            UseLeft = br.ReadEnumByte<Key>();
            UseShoulder = br.ReadEnumByte<Key>();
            SwitchRight = br.ReadEnumByte<Key>();
            SwitchLeft = br.ReadEnumByte<Key>();
            PurgeRight = br.ReadEnumByte<Key>();
            PurgeLeft = br.ReadEnumByte<Key>();
            PurgeShoulder = br.ReadEnumByte<Key>();
            ReloadRight = br.ReadEnumByte<Key>();
            ReloadLeft = br.ReadEnumByte<Key>();
            LockOn = br.ReadEnumByte<Key>();
            SwitchLock = br.ReadEnumByte<Key>();
            Pause = br.ReadEnumByte<Key>();
            Forward = br.ReadEnumByte<Key>();
            Backward = br.ReadEnumByte<Key>();
            Right = br.ReadEnumByte<Key>();
            Left = br.ReadEnumByte<Key>();
            AssaultArmor = br.ReadEnumByte<Key>();
            ControlType = br.ReadByte();
            Unk1B = br.ReadByte();
            Unk1C = br.ReadInt32();
        }

        #endregion

        #region Write

        internal void Write(BinaryStreamWriter bw)
        {
            bw.WriteByte((byte)LookUp);
            bw.WriteByte((byte)LookDown);
            bw.WriteByte((byte)ResetView);
            bw.WriteByte((byte)LookRight);
            bw.WriteByte((byte)LookLeft);
            bw.WriteByte((byte)Boost);
            bw.WriteByte((byte)OveredBoost);
            bw.WriteByte((byte)QuickBoost);
            bw.WriteByte((byte)UseRight);
            bw.WriteByte((byte)UseLeft);
            bw.WriteByte((byte)UseShoulder);
            bw.WriteByte((byte)SwitchRight);
            bw.WriteByte((byte)SwitchLeft);
            bw.WriteByte((byte)PurgeRight);
            bw.WriteByte((byte)PurgeLeft);
            bw.WriteByte((byte)PurgeShoulder);
            bw.WriteByte((byte)ReloadRight);
            bw.WriteByte((byte)ReloadLeft);
            bw.WriteByte((byte)LockOn);
            bw.WriteByte((byte)SwitchLock);
            bw.WriteByte((byte)Pause);
            bw.WriteByte((byte)Forward);
            bw.WriteByte((byte)Backward);
            bw.WriteByte((byte)Right);
            bw.WriteByte((byte)Left);
            bw.WriteByte((byte)AssaultArmor);
            bw.WriteByte(ControlType);
            bw.WriteByte(Unk1B);
            bw.WriteInt32(Unk1C);
        }

        #endregion

        #region Default Methods

        public static KeySet CreateDefaultRegularA()
        {
            KeySet keys = new()
            {
                LookUp = Key.RightStickUpArrow,
                LookDown = Key.RightStickDownArrow,
                ResetView = Key.R3,
                LookRight = Key.RightStickRightArrow,
                LookLeft = Key.RightStickLeftArrow,
                Boost = Key.L2,
                OveredBoost = Key.Triangle,
                QuickBoost = Key.R2,
                UseRight = Key.X,
                UseLeft = Key.Square,
                UseShoulder = Key.Circle,
                SwitchRight = Key.R1,
                SwitchLeft = Key.L1,
                PurgeRight = Key.Compound,
                PurgeLeft = Key.Compound,
                PurgeShoulder = Key.Compound,
                ReloadRight = Key.Compound,
                ReloadLeft = Key.Compound,
                LockOn = Key.L3,
                SwitchLock = Key.Compound,
                Pause = Key.Start,
                Forward = Key.LeftStickUpArrow,
                Backward = Key.LeftStickDownArrow,
                Right = Key.LeftStickRightArrow,
                Left = Key.LeftStickLeftArrow,
                AssaultArmor = Key.Compound,
                ControlType = 2,
                Unk1B = 128,
                Unk1C = 0
            };
            return keys;
        }

        public static KeySet CreateDefaultRegularB()
        {
            KeySet keys = new()
            {
                LookUp = Key.RightStickUpArrow,
                LookDown = Key.RightStickDownArrow,
                ResetView = Key.R3,
                LookRight = Key.RightStickRightArrow,
                LookLeft = Key.RightStickLeftArrow,
                Boost = Key.L2,
                OveredBoost = Key.Triangle,
                QuickBoost = Key.R2,
                UseRight = Key.R1,
                UseLeft = Key.L1,
                UseShoulder = Key.Circle,
                SwitchRight = Key.X,
                SwitchLeft = Key.Square,
                PurgeRight = Key.Compound,
                PurgeLeft = Key.Compound,
                PurgeShoulder = Key.Compound,
                ReloadRight = Key.Compound,
                ReloadLeft = Key.Compound,
                LockOn = Key.L3,
                SwitchLock = Key.Compound,
                Pause = Key.Start,
                Forward = Key.LeftStickUpArrow,
                Backward = Key.LeftStickDownArrow,
                Right = Key.LeftStickRightArrow,
                Left = Key.LeftStickLeftArrow,
                AssaultArmor = Key.Compound,
                ControlType = 3,
                Unk1B = 128,
                Unk1C = 0
            };
            return keys;
        }

        public static KeySet CreateDefaultSimple()
        {
            KeySet keys = new()
            {
                LookUp = Key.RightStickUpArrow,
                LookDown = Key.RightStickDownArrow,
                ResetView = Key.R3,
                LookRight = Key.RightStickRightArrow,
                LookLeft = Key.RightStickLeftArrow,
                Boost = Key.L2,
                OveredBoost = Key.L1,
                QuickBoost = Key.R2,
                UseRight = Key.X,
                UseLeft = Key.Square,
                UseShoulder = Key.R1,
                SwitchRight = Key.Circle,
                SwitchLeft = Key.Triangle,
                PurgeRight = Key.Compound,
                PurgeLeft = Key.Compound,
                PurgeShoulder = Key.Compound,
                ReloadRight = Key.Compound,
                ReloadLeft = Key.Compound,
                LockOn = Key.L3,
                SwitchLock = Key.Compound,
                Pause = Key.Start,
                Forward = Key.LeftStickUpArrow,
                Backward = Key.LeftStickDownArrow,
                Right = Key.Unassigned,
                Left = Key.Unassigned,
                AssaultArmor = Key.Compound,
                ControlType = 0,
                Unk1B = 192,
                Unk1C = 0
            };
            return keys;
        }

        #endregion

        #region Enums

        public enum Key : byte
        {
            LeftStickUpArrow = 0,
            LeftStickDownArrow = 1,
            LeftStickRightArrow = 2,
            LeftStickLeftArrow = 3,
            RightStickUpArrow = 4,
            RightStickDownArrow = 5,
            RightStickRightArrow = 6,
            RightStickLeftArrow = 7,
            Circle = 8,
            Triangle = 9,
            Square = 10,
            X = 11,
            L1 = 12,
            L2 = 13,
            R1 = 14,
            R2 = 15,
            L3 = 16,
            R3 = 17,
            Select = 18,
            Start = 19,
            UpArrow = 20,
            DownArrow = 21,
            RightArrow = 22,
            LeftArrow = 23,
            Compound = 254,
            Unassigned = 255
        }

        #endregion
    }
}
