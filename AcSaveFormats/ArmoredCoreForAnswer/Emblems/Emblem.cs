using Edoke.IO;
using System;

namespace AcSaveFormats.ArmoredCoreForAnswer.Emblems
{
    /// <summary>
    /// An emblem with customizable layers of image IDs or a single preset image ID.
    /// </summary>
    public class Emblem
    {
        #region Constants

        /// <summary>
        /// The number of layers in the emblem.
        /// </summary>
        public const int LayerCount = 16;

        #endregion

        #region Properties

        /// <summary>
        /// The emblem type.
        /// </summary>
        public EmblemType Type { get; set; }

        /// <summary>
        /// Unknown.
        /// </summary>
        public byte Unk01 { get; set; }

        /// <summary>
        /// Unknown.
        /// </summary>
        public byte Unk02 { get; set; }

        /// <summary>
        /// Unknown.
        /// </summary>
        public byte Unk03 { get; set; }

        /// <summary>
        /// The ID of a preset emblem.<br/>
        /// Only used with <see cref="EmblemType.Preset"/>.
        /// </summary>
        public ushort EmblemID { get; set; }

        /// <summary>
        /// The layers of a custom emblem.<br/>
        /// Only used with <see cref="EmblemType.Custom"/>.<br/>
        /// There are 16 layers.
        /// </summary>
        public EmblemLayer[] Layers { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="Emblem"/>.
        /// </summary>
        public Emblem()
        {
            Type = EmblemType.Custom;
            EmblemID = 0;
            Layers = new EmblemLayer[LayerCount];
        }

        /// <summary>
        /// Reads an <see cref="Emblem"/> from a stream.
        /// </summary>
        /// <param name="br">The stream reader.</param>
        /// <exception cref="NotSupportedException">The specified <see cref="EmblemType"/> is invalid or not supported.</exception>
        internal Emblem(BinaryStreamReader br)
        {
            Layers = new EmblemLayer[LayerCount];

            Type = br.ReadEnumByte<EmblemType>();
            Unk01 = br.ReadByte();
            Unk02 = br.ReadByte();
            Unk03 = br.ReadByte();
            switch (Type)
            {
                case EmblemType.None:
                    br.AssertPattern(128, 0);
                    break;
                case EmblemType.Preset:
                    EmblemID = br.ReadUInt16();
                    br.AssertPattern(126, 0);
                    break;
                case EmblemType.Custom:
                    for (int i = 0; i < LayerCount; i++)
                    {
                        Layers[i] = new EmblemLayer(br);
                    }
                    break;
                default:
                    throw new NotSupportedException($"Unknown {nameof(EmblemType)}: {Type}");
            }
        }

        #endregion

        #region Write

        /// <summary>
        /// Write an <see cref="Emblem"/> to a stream.
        /// </summary>
        /// <param name="bw">The stream writer.</param>
        internal void Write(BinaryStreamWriter bw)
        {
            bw.WriteByte((byte)Type);
            bw.WriteByte(Unk01);
            bw.WriteByte(Unk02);
            bw.WriteByte(Unk03);
            switch (Type)
            {
                case EmblemType.None:
                    bw.WritePattern(128, 0);
                    break;
                case EmblemType.Preset:
                    bw.WriteUInt16(EmblemID);
                    bw.WritePattern(126, 0);
                    break;
                case EmblemType.Custom:
                    for (int i = 0; i < LayerCount; i++)
                    {
                        Layers[i].Write(bw);
                    }
                    break;
            }
        }

        #endregion
    }
}
