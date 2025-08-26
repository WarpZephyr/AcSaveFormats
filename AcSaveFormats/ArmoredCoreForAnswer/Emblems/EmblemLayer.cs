using Edoke.IO;
using System;

namespace AcSaveFormats.ArmoredCoreForAnswer.Emblems
{
    /// <summary>
    /// A layer for custom emblems.
    /// </summary>
    public struct EmblemLayer
    {
        #region Properties

        /// <summary>
        /// The angle of the layer.
        /// </summary>
        public byte Angle { get; set; }

        /// <summary>
        /// The ID of the image being used on this layer.
        /// </summary>
        public byte ImageID { get; set; }

        /// <summary>
        /// The ID of the color being used on this layer.
        /// </summary>
        public byte ColorID { get; set; }

        /// <summary>
        /// The width of the layer.<br/>
        /// If negative the layer is flipped horizontally.
        /// </summary>
        public sbyte Width { get; set; }

        /// <summary>
        /// The height of the layer.<br/>
        /// If negative the layer is flipped vertically.
        /// </summary>
        public sbyte Height { get; set; }

        /// <summary>
        /// The X coordinate of the layer.
        /// </summary>
        public byte X { get; set; }

        /// <summary>
        /// The Y coordinate of the layer.
        /// </summary>
        public byte Y { get; set; }

        /// <summary>
        /// The flags of the layer.
        /// </summary>
        public EmblemLayerFlags Flags { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new <see cref="EmblemLayer"/>.
        /// </summary>
        public EmblemLayer()
        {

        }

        /// <summary>
        /// Read an <see cref="EmblemLayer"/> from a stream.
        /// </summary>
        /// <param name="br">The stream reader.</param>
        internal EmblemLayer(BinaryStreamReader br)
        {
            Angle = br.ReadByte();
            ImageID = br.ReadByte();
            ColorID = br.ReadByte();
            Width = br.ReadSByte();
            Height = br.ReadSByte();
            X = br.ReadByte();
            Y = br.ReadByte();
            Flags = (EmblemLayerFlags)br.ReadByte();
        }

        #endregion

        #region Write

        /// <summary>
        /// Write an <see cref="EmblemLayer"/> to a stream.
        /// </summary>
        /// <param name="bw">The stream writer.</param>
        internal readonly void Write(BinaryStreamWriter bw)
        {
            bw.WriteByte(Angle);
            bw.WriteByte(ImageID);
            bw.WriteByte(ColorID);
            bw.WriteSByte(Width);
            bw.WriteSByte(Height);
            bw.WriteByte(X);
            bw.WriteByte(Y);
            bw.WriteByte((byte)Flags);
        }

        #endregion

        #region Objects

        /// <summary>
        /// The flags of a layer.
        /// </summary>
        [Flags]
        public enum EmblemLayerFlags : byte
        {
            /// <summary>
            /// No flags are enabled.
            /// </summary>
            None = 0,

            /// <summary>
            /// Angle is negative.
            /// </summary>
            NegativeAngle = 16,

            /// <summary>
            /// X is negative.
            /// </summary>
            NegativeX = 64,

            /// <summary>
            /// Y is negative.
            /// </summary>
            NegativeY = 128
        }

        #endregion
    }
}
