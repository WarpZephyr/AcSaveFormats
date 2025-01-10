using AcSaveFormats.ACFA.Colors;
using AcSaveFormats.ACFA.Emblems;
using BinaryMemory;
using System.Drawing;

namespace AcSaveFormats.ACFA
{
    /// <summary>
    /// Paint save data.
    /// </summary>
    public class Paint
    {
        #region Constants

        /// <summary>
        /// The number of colorsets.
        /// </summary>
        public const int ColorSetCount = 4 * 4;

        /// <summary>
        /// The number of colors in the user palette.
        /// </summary>
        public const int PaletteCount = 3 * 12;

        /// <summary>
        /// The number of emblems.
        /// </summary>
        public const int EmblemCount = 64;

        #endregion

        #region Properties

        /// <summary>
        /// Unknown; Usually 400.
        /// </summary>
        public ushort Unk00 { get; set; }

        /// <summary>
        /// Unknown; Usually 0.
        /// </summary>
        public ushort Unk02 { get; set; }

        /// <summary>
        /// The user saved colorsets.
        /// </summary>
        public ColorSet[] ColorSets { get; private set; }

        /// <summary>
        /// The user saved color palette.
        /// </summary>
        public Color[] UserPalette { get; private set; }

        /// <summary>
        /// The user saved emblems.
        /// </summary>
        public Emblem[] Emblems { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new <see cref="Paint"/>
        /// </summary>
        public Paint()
        {
            Unk00 = 400;
            Unk02 = 0;
            ColorSets = new ColorSet[ColorSetCount];
            UserPalette = new Color[PaletteCount];
            Emblems = new Emblem[EmblemCount];
        }

        /// <summary>
        /// Read a <see cref="Paint"/> from a stream.
        /// </summary>
        /// <param name="br">The stream reader.</param>
        internal Paint(BinaryStreamReader br)
        {
            Unk00 = br.ReadUInt16();
            Unk02 = br.ReadUInt16();
            ColorSets = new ColorSet[ColorSetCount];
            UserPalette = new Color[PaletteCount];
            Emblems = new Emblem[EmblemCount];

            for (int i = 0; i < ColorSetCount; i++)
            {
                ColorSets[i] = new ColorSet(br);
            }

            for (int i = 0; i < PaletteCount; i++)
            {
                UserPalette[i] = br.ReadColorRGBA();
            }

            for (int i = 0; i < EmblemCount; i++)
            {
                Emblems[i] = new Emblem(br);
            }
        }

        #endregion

        #region Read

        /// <summary>
        /// Read a <see cref="Paint"/> from a file.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <returns>A new <see cref="Paint"/>.</returns>
        public static Paint Read(string path)
        {
            using var br = new BinaryStreamReader(path, true);
            return new Paint(br);
        }

        /// <summary>
        /// Read a <see cref="Paint"/> from a byte array.
        /// </summary>
        /// <param name="bytes">The byte array.</param>
        /// <returns>A new <see cref="Paint"/>.</returns>
        public static Paint Read(byte[] bytes)
        {
            using var br = new BinaryStreamReader(bytes, true);
            return new Paint(br);
        }

        #endregion

        #region Write

        /// <summary>
        /// Write a <see cref="Paint"/> to a stream.
        /// </summary>
        /// <param name="bw">The stream writer.</param>
        internal void Write(BinaryStreamWriter bw)
        {
            bw.WriteUInt16(Unk00);
            bw.WriteUInt16(Unk02);

            for (int i = 0; i < ColorSetCount; i++)
            {
                ColorSets[i].Write(bw);
            }

            for (int i = 0; i < PaletteCount; i++)
            {
                bw.WriteColorRGBA(UserPalette[i]);
            }

            for (int i = 0; i < EmblemCount; i++)
            {
                Emblems[i].Write(bw);
            }
        }

        /// <summary>
        /// Write this <see cref="Paint"/> to a file.
        /// </summary>
        /// <param name="path">The file path.</param>
        public void Write(string path)
        {
            using var bw = new BinaryStreamWriter(path, true);
            Write(bw);
        }

        /// <summary>
        /// Write this <see cref="Paint"/> to a byte array.
        /// </summary>
        /// <returns>A byte array.</returns>
        public byte[] Write()
        {
            using var bw = new BinaryStreamWriter(true);
            Write(bw);
            return bw.ToArray();
        }

        #endregion
    }
}
