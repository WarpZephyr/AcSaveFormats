using Edoke.IO;
using System.Drawing;

namespace AcSaveFormats.ACFA.Colors
{
    /// <summary>
    /// A colorset.
    /// </summary>
    public class AcColorSet
    {
        #region Properties

        /// <summary>
        /// The head color set.
        /// </summary>
        public ColorSet HeadColor { get; set; }

        /// <summary>
        /// The core color set.
        /// </summary>
        public ColorSet CoreColor { get; set; }

        /// <summary>
        /// The right arm color set.
        /// </summary>
        public ColorSet ArmRightColor { get; set; }

        /// <summary>
        /// The left arm color set.
        /// </summary>
        public ColorSet ArmLeftColor { get; set; }

        /// <summary>
        /// The color set for legs.
        /// </summary>
        public ColorSet LegsColor { get; set; }

        /// <summary>
        /// The right arm unit color set.
        /// </summary>
        public ColorSet ArmUnitRightColor { get; set; }

        /// <summary>
        /// The left arm unit color set.
        /// </summary>
        public ColorSet ArmUnitLeftColor { get; set; }

        /// <summary>
        /// The right back unit color set.
        /// </summary>
        public ColorSet BackUnitRightColor { get; set; }

        /// <summary>
        /// The left back unit color set.
        /// </summary>
        public ColorSet BackUnitLeftColor { get; set; }

        /// <summary>
        /// The shoulder unit color set.
        /// </summary>
        public ColorSet ShoulderUnitColor { get; set; }

        /// <summary>
        /// The right hanger unit color set.
        /// </summary>
        public ColorSet HangerUnitRightColor { get; set; }

        /// <summary>
        /// The left hanger unit color set.
        /// </summary>
        public ColorSet HangerUnitLeftColor { get; set; }

        /// <summary>
        /// The top head stabilizer color set.
        /// </summary>
        public ColorSet StabilizerHeadTopColor { get; set; }

        /// <summary>
        /// The right side head stabilizer color set.
        /// </summary>
        public ColorSet StabilizerHeadRightColor { get; set; }

        /// <summary>
        /// The left side head stabilizer color set.
        /// </summary>
        public ColorSet StabilizerHeadLeftColor { get; set; }

        /// <summary>
        /// The upper right side core stabilizer color set.
        /// </summary>
        public ColorSet StabilizerCoreUpperRightColor { get; set; }

        /// <summary>
        /// The upper left side core stabilizer color set.
        /// </summary>
        public ColorSet StabilizerCoreUpperLeftColor { get; set; }

        /// <summary>
        /// The lower right side core stabilizer color set.
        /// </summary>
        public ColorSet StabilizerCoreLowerRightColor { get; set; }

        /// <summary>
        /// The lower left side core stabilizer color set.
        /// </summary>
        public ColorSet StabilizerCoreLowerLeftColor { get; set; }

        /// <summary>
        /// The right arm stabilizer color set.
        /// </summary>
        public ColorSet StabilizerArmRightColor { get; set; }

        /// <summary>
        /// The left arm stabilizer color set.
        /// </summary>
        public ColorSet StabilizerArmLeftColor { get; set; }

        /// <summary>
        /// The back leg stabilizer color set.
        /// </summary>
        public ColorSet StabilizerLegsBackColor { get; set; }

        /// <summary>
        /// The back leg stabilizer color set.
        /// </summary>
        public ColorSet StabilizerLegsUpperRightColor { get; set; }

        /// <summary>
        /// The upper left leg stabilizer color set.
        /// </summary>
        public ColorSet StabilizerLegsUpperLeftColor { get; set; }

        /// <summary>
        /// The back upper right leg stabilizer color set, unused.
        /// </summary>
        public ColorSet StabilizerLegsUpperRightBackColor { get; set; }

        /// <summary>
        /// The back upper left leg stabilizer color set, unused.
        /// </summary>
        public ColorSet StabilizerLegsUpperLeftBackColor { get; set; }

        /// <summary>
        /// The middle right side leg stabilizer color set.
        /// </summary>
        public ColorSet StabilizerLegsMiddleRightColor { get; set; }

        /// <summary>
        /// The middle left side leg stabilizer color set.
        /// </summary>
        public ColorSet StabilizerLegsMiddleLeftColor { get; set; }

        /// <summary>
        /// The back middle right leg stabilizer color set, unused.
        /// </summary>
        public ColorSet StabilizerLegsMiddleRightBackColor { get; set; }

        /// <summary>
        /// The back middle left leg stabilizer color set, unused.
        /// </summary>
        public ColorSet StabilizerLegsMiddleLeftBackColor { get; set; }

        /// <summary>
        /// The lower right side leg stabilizer color set.
        /// </summary>
        public ColorSet StabilizerLegsLowerRightColor { get; set; }

        /// <summary>
        /// The lower left side leg stabilizer color set.
        /// </summary>
        public ColorSet StabilizerLegsLowerLeftColor { get; set; }

        /// <summary>
        /// The back right left leg stabilizer color set, unused.
        /// </summary>
        public ColorSet StabilizerLegsLowerRightBackColor { get; set; }

        /// <summary>
        /// The back lower left leg stabilizer color set, unused.
        /// </summary>
        public ColorSet StabilizerLegsLowerLeftBackColor { get; set; }

        /// <summary>
        /// The color pattern to be applied to all color sets.
        /// </summary>
        public byte AllPattern { get; set; }

        /// <summary>
        /// The color pattern to be applied to all frame color sets.
        /// </summary>
        public byte AllFramesPattern { get; set; }

        /// <summary>
        /// The color pattern to be applied to all unit color sets.
        /// </summary>
        public byte AllUnitsPattern { get; set; }

        /// <summary>
        /// The color pattern to be applied to all stabilizer color sets.
        /// </summary>
        public byte AllStabilizersPattern { get; set; }

        /// <summary>
        /// The color pattern set for the head color set.
        /// </summary>
        public byte HeadPattern { get; set; }

        /// <summary>
        /// The color pattern set for the core color set.
        /// </summary>
        public byte CorePattern { get; set; }

        /// <summary>
        /// The color pattern set for the right arm color set.
        /// </summary>
        public byte ArmRightPattern { get; set; }

        /// <summary>
        /// The color pattern set for the left arm color set.
        /// </summary>
        public byte ArmLeftPattern { get; set; }

        /// <summary>
        /// The color pattern set for the legs color set.
        /// </summary>
        public byte LegsPattern { get; set; }

        /// <summary>
        /// The color pattern set for the right arm unit color set.
        /// </summary>
        public byte ArmUnitRightPattern { get; set; }

        /// <summary>
        /// The color pattern set for the left arm unit color set.
        /// </summary>
        public byte ArmUnitLeftPattern { get; set; }

        /// <summary>
        /// The color pattern set for the right back unit color set.
        /// </summary>
        public byte BackUnitRightPattern { get; set; }

        /// <summary>
        /// The color pattern set for the left back unit color set.
        /// </summary>
        public byte BackUnitLeftPattern { get; set; }

        /// <summary>
        /// The color pattern set for the shoulder unit color set.
        /// </summary>
        public byte ShoulderUnitPattern { get; set; }

        /// <summary>
        /// The color pattern set for the right hangar unit color set.
        /// </summary>
        public byte HangerUnitRightPattern { get; set; }

        /// <summary>
        /// The color pattern set for the left hangar unit color set.
        /// </summary>
        public byte HangerUnitLeftPattern { get; set; }

        /// <summary>
        /// The color pattern to be applied to all head stabilizer color sets.
        /// </summary>
        public byte AllHeadStabilizersPattern { get; set; }

        /// <summary>
        /// The color pattern to be applied to all core stabilizer color sets.
        /// </summary>
        public byte AllCoreStabilizersPattern { get; set; }

        /// <summary>
        /// The color pattern to be applied to all arm stabilizer color sets.
        /// </summary>
        public byte AllArmStabilizersPattern { get; set; }

        /// <summary>
        /// The color pattern to be applied to all leg stabilizer color sets.
        /// </summary>
        public byte AllLegStabilizersPattern { get; set; }

        /// <summary>
        /// The color pattern set for the top head stabilizer color set.
        /// </summary>
        public byte HeadTopStabilizerPattern { get; set; }

        /// <summary>
        /// The color pattern set for the right head stabilizer color set.
        /// </summary>
        public byte HeadRightStabilizerPattern { get; set; }

        /// <summary>
        /// The color pattern set for the left head stabilizer color set.
        /// </summary>
        public byte HeadLeftStabilizerPattern { get; set; }

        /// <summary>
        /// The color pattern set for the upper right core stabilizer color set.
        /// </summary>
        public byte CoreUpperRightStabilizerPattern { get; set; }

        /// <summary>
        /// The color pattern set for the upper left core stabilizer color set.
        /// </summary>
        public byte CoreUpperLeftStabilizerPattern { get; set; }

        /// <summary>
        /// The color pattern set for the lower right core stabilizer color set.
        /// </summary>
        public byte CoreLowerRightStabilizerPattern { get; set; }

        /// <summary>
        /// The color pattern set for the lower left core stabilizer color set.
        /// </summary>
        public byte CoreLowerLeftStabilizerPattern { get; set; }

        /// <summary>
        /// The color pattern set for the right arm stabilizer color set.
        /// </summary>
        public byte ArmRightStabilizerPattern { get; set; }

        /// <summary>
        /// The color pattern set for the left arm stabilizer color set.
        /// </summary>
        public byte ArmLeftStabilizerPattern { get; set; }

        /// <summary>
        /// The color pattern set for the stabilizer on the back of legs' color set.
        /// </summary>
        public byte LegsBackStabilizerPattern { get; set; }

        /// <summary>
        /// The color pattern set for the upper right stabilizer color set.
        /// </summary>
        public byte LegsUpperRightStabilizerPattern { get; set; }

        /// <summary>
        /// The color pattern set for the upper left stabilizer color set.
        /// </summary>
        public byte LegsUpperLeftStabilizerPattern { get; set; }

        /// <summary>
        /// The color pattern set for the middle right stabilizer color set.
        /// </summary>
        public byte LegsMiddleRightStabilizerPattern { get; set; }

        /// <summary>
        /// The color pattern set for the middle left stabilizer color set.
        /// </summary>
        public byte LegsMiddleLeftStabilizerPattern { get; set; }

        /// <summary>
        /// The color pattern set for the lower right stabilizer color set.
        /// </summary>
        public byte LegsLowerRightStabilizerPattern { get; set; }

        /// <summary>
        /// The color pattern set for the lower left stabilizer color set.
        /// </summary>
        public byte LegsLowerLeftStabilizerPattern { get; set; }

        /// <summary>
        /// The eye color.
        /// </summary>
        public Color EyeColor { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new <see cref="AcColorSet"/>.
        /// </summary>
        public AcColorSet()
        {

        }

        /// <summary>
        /// Read an <see cref="AcColorSet"/> from a stream.
        /// </summary>
        /// <param name="br">The stream reader.</param>
        internal AcColorSet(BinaryStreamReader br)
        {
            HeadColor = new ColorSet(br);
            CoreColor = new ColorSet(br);
            ArmRightColor = new ColorSet(br);
            ArmLeftColor = new ColorSet(br);
            LegsColor = new ColorSet(br);
            ArmUnitRightColor = new ColorSet(br);
            ArmUnitLeftColor = new ColorSet(br);
            BackUnitRightColor = new ColorSet(br);
            BackUnitLeftColor = new ColorSet(br);
            ShoulderUnitColor = new ColorSet(br);
            HangerUnitRightColor = new ColorSet(br);
            HangerUnitLeftColor = new ColorSet(br);
            StabilizerHeadTopColor = new ColorSet(br);
            StabilizerHeadRightColor = new ColorSet(br);
            StabilizerHeadLeftColor = new ColorSet(br);
            StabilizerCoreUpperRightColor = new ColorSet(br);
            StabilizerCoreUpperLeftColor = new ColorSet(br);
            StabilizerCoreLowerRightColor = new ColorSet(br);
            StabilizerCoreLowerLeftColor = new ColorSet(br);
            StabilizerArmRightColor = new ColorSet(br);
            StabilizerArmLeftColor = new ColorSet(br);
            StabilizerLegsBackColor = new ColorSet(br);
            StabilizerLegsUpperRightColor = new ColorSet(br);
            StabilizerLegsUpperLeftColor = new ColorSet(br);
            StabilizerLegsUpperRightBackColor = new ColorSet(br);
            StabilizerLegsUpperLeftBackColor = new ColorSet(br);
            StabilizerLegsMiddleRightColor = new ColorSet(br);
            StabilizerLegsMiddleLeftColor = new ColorSet(br);
            StabilizerLegsMiddleRightBackColor = new ColorSet(br);
            StabilizerLegsMiddleLeftBackColor = new ColorSet(br);
            StabilizerLegsLowerRightColor = new ColorSet(br);
            StabilizerLegsLowerLeftColor = new ColorSet(br);
            StabilizerLegsLowerRightBackColor = new ColorSet(br);
            StabilizerLegsLowerLeftBackColor = new ColorSet(br);
            AllPattern = br.ReadByte();
            AllFramesPattern = br.ReadByte();
            AllUnitsPattern = br.ReadByte();
            AllStabilizersPattern = br.ReadByte();
            HeadPattern = br.ReadByte();
            CorePattern = br.ReadByte();
            ArmRightPattern = br.ReadByte();
            ArmLeftPattern = br.ReadByte();
            LegsPattern = br.ReadByte();
            ArmUnitRightPattern = br.ReadByte();
            ArmUnitLeftPattern = br.ReadByte();
            BackUnitRightPattern = br.ReadByte();
            BackUnitLeftPattern = br.ReadByte();
            ShoulderUnitPattern = br.ReadByte();
            HangerUnitRightPattern = br.ReadByte();
            HangerUnitLeftPattern = br.ReadByte();
            AllHeadStabilizersPattern = br.ReadByte();
            AllCoreStabilizersPattern = br.ReadByte();
            AllArmStabilizersPattern = br.ReadByte();
            AllLegStabilizersPattern = br.ReadByte();
            HeadTopStabilizerPattern = br.ReadByte();
            HeadRightStabilizerPattern = br.ReadByte();
            HeadLeftStabilizerPattern = br.ReadByte();
            CoreUpperRightStabilizerPattern = br.ReadByte();
            CoreUpperLeftStabilizerPattern = br.ReadByte();
            CoreLowerRightStabilizerPattern = br.ReadByte();
            CoreLowerLeftStabilizerPattern = br.ReadByte();
            ArmRightStabilizerPattern = br.ReadByte();
            ArmLeftStabilizerPattern = br.ReadByte();
            LegsBackStabilizerPattern = br.ReadByte();
            LegsUpperRightStabilizerPattern = br.ReadByte();
            LegsUpperLeftStabilizerPattern = br.ReadByte();
            LegsMiddleRightStabilizerPattern = br.ReadByte();
            LegsMiddleLeftStabilizerPattern = br.ReadByte();
            LegsLowerRightStabilizerPattern = br.ReadByte();
            LegsLowerLeftStabilizerPattern = br.ReadByte();
            EyeColor = br.ReadRgba();
        }

        #endregion

        #region Read

        /// <summary>
        /// Read an <see cref="AcColorSet"/> from a file.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <returns>A new <see cref="Paint"/>.</returns>
        public static AcColorSet Read(string path)
        {
            using var br = new BinaryStreamReader(path, true);
            return new AcColorSet(br);
        }

        /// <summary>
        /// Read an <see cref="AcColorSet"/> from a byte array.
        /// </summary>
        /// <param name="bytes">The byte array.</param>
        /// <returns>A new <see cref="Paint"/>.</returns>
        public static AcColorSet Read(byte[] bytes)
        {
            using var br = new BinaryStreamReader(bytes, true);
            return new AcColorSet(br);
        }

        #endregion

        #region Write

        /// <summary>
        /// Write an <see cref="AcColorSet"/> to a stream.
        /// </summary>
        /// <param name="bw">The stream writer.</param>
        internal void Write(BinaryStreamWriter bw)
        {
            HeadColor.Write(bw);
            CoreColor.Write(bw);
            ArmRightColor.Write(bw);
            ArmLeftColor.Write(bw);
            LegsColor.Write(bw);
            ArmUnitRightColor.Write(bw);
            ArmUnitLeftColor.Write(bw);
            BackUnitRightColor.Write(bw);
            BackUnitLeftColor.Write(bw);
            ShoulderUnitColor.Write(bw);
            HangerUnitRightColor.Write(bw);
            HangerUnitLeftColor.Write(bw);
            StabilizerHeadTopColor.Write(bw);
            StabilizerHeadRightColor.Write(bw);
            StabilizerHeadLeftColor.Write(bw);
            StabilizerCoreUpperRightColor.Write(bw);
            StabilizerCoreUpperLeftColor.Write(bw);
            StabilizerCoreLowerRightColor.Write(bw);
            StabilizerCoreLowerLeftColor.Write(bw);
            StabilizerArmRightColor.Write(bw);
            StabilizerArmLeftColor.Write(bw);
            StabilizerLegsBackColor.Write(bw);
            StabilizerLegsUpperRightColor.Write(bw);
            StabilizerLegsUpperLeftColor.Write(bw);
            StabilizerLegsUpperRightBackColor.Write(bw);
            StabilizerLegsUpperLeftBackColor.Write(bw);
            StabilizerLegsMiddleRightColor.Write(bw);
            StabilizerLegsMiddleLeftColor.Write(bw);
            StabilizerLegsMiddleRightBackColor.Write(bw);
            StabilizerLegsMiddleLeftBackColor.Write(bw);
            StabilizerLegsLowerRightColor.Write(bw);
            StabilizerLegsLowerLeftColor.Write(bw);
            StabilizerLegsLowerRightBackColor.Write(bw);
            StabilizerLegsLowerLeftBackColor.Write(bw);
            bw.WriteByte(AllPattern);
            bw.WriteByte(AllFramesPattern);
            bw.WriteByte(AllUnitsPattern);
            bw.WriteByte(AllStabilizersPattern);
            bw.WriteByte(HeadPattern);
            bw.WriteByte(CorePattern);
            bw.WriteByte(ArmRightPattern);
            bw.WriteByte(ArmLeftPattern);
            bw.WriteByte(LegsPattern);
            bw.WriteByte(ArmUnitRightPattern);
            bw.WriteByte(ArmUnitLeftPattern);
            bw.WriteByte(BackUnitRightPattern);
            bw.WriteByte(BackUnitLeftPattern);
            bw.WriteByte(ShoulderUnitPattern);
            bw.WriteByte(HangerUnitRightPattern);
            bw.WriteByte(HangerUnitLeftPattern);
            bw.WriteByte(AllHeadStabilizersPattern);
            bw.WriteByte(AllCoreStabilizersPattern);
            bw.WriteByte(AllArmStabilizersPattern);
            bw.WriteByte(AllLegStabilizersPattern);
            bw.WriteByte(HeadTopStabilizerPattern);
            bw.WriteByte(HeadRightStabilizerPattern);
            bw.WriteByte(HeadLeftStabilizerPattern);
            bw.WriteByte(CoreUpperRightStabilizerPattern);
            bw.WriteByte(CoreUpperLeftStabilizerPattern);
            bw.WriteByte(CoreLowerRightStabilizerPattern);
            bw.WriteByte(CoreLowerLeftStabilizerPattern);
            bw.WriteByte(ArmRightStabilizerPattern);
            bw.WriteByte(ArmLeftStabilizerPattern);
            bw.WriteByte(LegsBackStabilizerPattern);
            bw.WriteByte(LegsUpperRightStabilizerPattern);
            bw.WriteByte(LegsUpperLeftStabilizerPattern);
            bw.WriteByte(LegsMiddleRightStabilizerPattern);
            bw.WriteByte(LegsMiddleLeftStabilizerPattern);
            bw.WriteByte(LegsLowerRightStabilizerPattern);
            bw.WriteByte(LegsLowerLeftStabilizerPattern);
            bw.WriteRgba(EyeColor);
        }

        /// <summary>
        /// Write this <see cref="AcColorSet"/> to a file.
        /// </summary>
        /// <param name="path">The file path.</param>
        public void Write(string path)
        {
            using var bw = new BinaryStreamWriter(path, true);
            Write(bw);
        }

        /// <summary>
        /// Write this <see cref="AcColorSet"/> to a byte array.
        /// </summary>
        /// <returns>A byte array.</returns>
        public byte[] Write()
        {
            var bw = new BinaryStreamWriter(true);
            Write(bw);
            return bw.FinishBytes();
        }

        #endregion
    }
}
