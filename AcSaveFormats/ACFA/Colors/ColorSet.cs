using BinaryMemory;
using System.Drawing;
using System.IO;

namespace AcSaveFormats.ACFA.Colors
{
    /// <summary>
    /// A color set containing six colors.
    /// </summary>
    public struct ColorSet
    {
        /// <summary>
        /// The main color.
        /// </summary>
        public Color Main { get; set; }

        /// <summary>
        /// The sub color.
        /// </summary>
        public Color Sub { get; set; }

        /// <summary>
        /// The support color.
        /// </summary>
        public Color Support { get; set; }

        /// <summary>
        /// The optional color.
        /// </summary>
        public Color Optional { get; set; }

        /// <summary>
        /// The joint color.
        /// </summary>
        public Color Joint { get; set; }

        /// <summary>
        /// The device color.
        /// </summary>
        public Color Device { get; set; }

        /// <summary>
        /// Create a new ColorSet using six RGBA order colors.
        /// </summary>
        /// <param name="main">The main color.</param>
        /// <param name="sub">The sub color.</param>
        /// <param name="support">The support color.</param>
        /// <param name="optional">The optional color.</param>
        /// <param name="joint">The joint color.</param>
        /// <param name="device">The device color.</param>
        public ColorSet(Color main, Color sub, Color support, Color optional, Color joint, Color device)
        {
            Main = main;
            Sub = sub;
            Support = support;
            Optional = optional;
            Joint = joint;
            Device = device;
        }

        /// <summary>
        /// Create a new copy of an existing colorset.
        /// </summary>
        /// <param name="colorset">A colorset.</param>
        public ColorSet(ColorSet colorset)
        {
            Main = colorset.Main;
            Sub = colorset.Sub;
            Support = colorset.Support;
            Optional = colorset.Optional;
            Joint = colorset.Joint;
            Device = colorset.Device;
        }

        /// <summary>
        /// Make a new color set with all values set the specified color.
        /// </summary>
        /// <param name="color">A color.</param>
        public ColorSet(Color color)
        {
            Main = color;
            Sub = color;
            Support = color;
            Optional = color;
            Joint = color;
            Device = color;
        }

        /// <summary>
        /// Read from a stream.
        /// </summary>
        /// <param name="br">The stream reader.</param>
        internal ColorSet(BinaryStreamReader br)
        {
            Main = br.ReadColorRGBA();
            Sub = br.ReadColorRGBA();
            Support = br.ReadColorRGBA();
            Optional = br.ReadColorRGBA();
            Joint = br.ReadColorRGBA();
            Device = br.ReadColorRGBA();
        }

        /// <summary>
        /// Write to a stream.
        /// </summary>
        /// <param name="bw">The stream writer.</param>
        internal readonly void Write(BinaryStreamWriter bw)
        {
            bw.WriteColorRGBA(Main);
            bw.WriteColorRGBA(Sub);
            bw.WriteColorRGBA(Support);
            bw.WriteColorRGBA(Optional);
            bw.WriteColorRGBA(Joint);
            bw.WriteColorRGBA(Device);
        }
    }
}
