using AcSaveFormats.Textures;
using Edoke.IO;
using System;
using static AcSaveFormats.Textures.DDS;

namespace AcSaveFormats.ArmoredCoreForAnswer.Designs
{
    public class Thumbnail
    {
        #region Constants

        public const int ThumbnailDataSize = 16384;

        #endregion

        #region Properties

        public ushort Width { get; private set; }
        public ushort Height { get; private set; }
        public byte[] PixelData { get; private set; }
        public bool IsXbox360 { get; set; }

        #endregion

        #region Constructors

        public Thumbnail()
        {
            PixelData = new byte[ThumbnailDataSize];
            IsXbox360 = false;
        }

        internal Thumbnail(BinaryStreamReader br, bool xbox)
        {
            IsXbox360 = xbox;
            Width = br.AssertUInt16(256);
            Height = br.AssertUInt16(128);
            br.AssertInt32(0);
            br.AssertInt32(0);
            int dataSize = br.AssertInt32(ThumbnailDataSize);
            PixelData = br.ReadBytes(dataSize);

            if (IsXbox360)
            {
                PixelData = DrSwizzler.Deswizzler.Xbox360Deswizzle(PixelData, Width, Height, DrSwizzler.DDS.DXEnums.DXGIFormat.BC1UNORM);
            }
        }

        #endregion

        #region Read

        public static Thumbnail Read(string path, bool xbox = false)
        {
            using var br = new BinaryStreamReader(path, true);
            return new Thumbnail(br, xbox);
        }

        public static Thumbnail Read(byte[] bytes, bool xbox = false)
        {
            using var br = new BinaryStreamReader(bytes, true);
            return new Thumbnail(br, xbox);
        }

        #endregion

        #region Write

        internal void Write(BinaryStreamWriter bw, bool xbox)
        {
            bw.WriteUInt16(Width);
            bw.WriteUInt16(Height);
            bw.WriteInt32(0);
            bw.WriteInt32(0);
            bw.WriteInt32(PixelData.Length);

            if (xbox)
            {
                bw.WriteBytes(DrSwizzler.Swizzler.Xbox360Swizzle(PixelData, Width, Height, DrSwizzler.DDS.DXEnums.DXGIFormat.BC1UNORM));
            }
            else
            {
                bw.WriteBytes(PixelData);
            }
        }

        public void Write(string path)
        {
            using var bw = new BinaryStreamWriter(path, true);
            Write(bw, IsXbox360);
        }

        public void Write(string path, bool xbox)
        {
            using var bw = new BinaryStreamWriter(path, true);
            Write(bw, xbox);
        }

        public byte[] Write()
        {
            var bw = new BinaryStreamWriter(true);
            Write(bw, IsXbox360);
            return bw.FinishBytes();
        }

        public byte[] Write(bool xbox)
        {
            var bw = new BinaryStreamWriter(true);
            Write(bw, xbox);
            return bw.FinishBytes();
        }

        #endregion

        #region Methods

        public bool SetPixelData(byte[] bytes)
        {
            if (bytes.Length != ThumbnailDataSize)
            {
                return false;
            }

            PixelData = bytes;
            return true;
        }

        #endregion

        #region DDS Methods

        public DDS BuildDdsHeader()
        {
            var dds = new DDS();
            dds.Width = Width;
            dds.Height = Height;
            dds.PitchOrLinearSize = Math.Max(1, (Width + 3) / 4) * DXT1_BLOCKSIZE;
            dds.MipMapCount = 1;
            dds.Flags = DDSD.CAPS | DDSD.HEIGHT | DDSD.WIDTH | DDSD.PIXELFORMAT | DDSD.MIPMAPCOUNT | DDSD.LINEARSIZE;
            dds.DDSPixelFormat = new PIXELFORMAT
            {
                Flags = DDPF.FOURCC,
                FourCC = "DXT1"
            };

            dds.Caps = DDSCAPS.TEXTURE;
            return dds;
        }

        public byte[] GetDdsBytes()
        {
            return BuildDdsHeader().Write(PixelData);
        }

        #endregion
    }
}
