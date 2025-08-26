using Edoke.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace AcSaveFormats.ACFA
{
    public class DesignDocument
    {
        #region Constants

        public const int ReservedDesignCount = 200;
        private const int FileSizeShiftJIS = 4837128;
        private const int FileSizeUTF16 = 4856328;

        #endregion

        #region Properties

        public ushort Unk00 { get; set; }
        public List<Design> Designs { get; set; }
        public bool UTF16 { get; set; }
        public bool Xbox { get; set; }

        #endregion

        #region Constructors

        public DesignDocument()
        {
            UTF16 = true;
            Unk00 = 450;
            Designs = [];
        }

        internal DesignDocument(BinaryStreamReader br, bool utf16, bool xbox)
        {
            UTF16 = utf16;
            Xbox = xbox;
            Unk00 = br.ReadUInt16();
            br.AssertUInt16(0);
            ushort designCount = br.ReadUInt16();
            if (designCount > ReservedDesignCount)
                throw new InvalidDataException($"Invalid design count: {designCount}; Max: {ReservedDesignCount}");

            br.AssertUInt16(0);
            br.AssertPattern(320, 0);

            Designs = new List<Design>(designCount);
            for (int i = 0; i < designCount; i++)
            {
                Designs.Add(new Design(br, utf16, xbox));
            }
        }

        #endregion

        #region Read

        public static DesignDocument Read(string path, bool xbox)
        {
            using var br = new BinaryStreamReader(path, true);
            return new DesignDocument(br, br.Length >= FileSizeUTF16, xbox);
        }

        public static DesignDocument Read(string path, bool utf16, bool xbox)
        {
            using var br = new BinaryStreamReader(path, true);
            return new DesignDocument(br, utf16, xbox);
        }

        public static DesignDocument Read(byte[] bytes, bool xbox)
        {
            using var br = new BinaryStreamReader(bytes, true);
            return new DesignDocument(br, br.Length >= FileSizeUTF16, xbox);
        }

        public static DesignDocument Read(byte[] bytes, bool utf16, bool xbox)
        {
            using var br = new BinaryStreamReader(bytes, true);
            return new DesignDocument(br, utf16, xbox);
        }

        #endregion

        #region Write

        internal void Write(BinaryStreamWriter bw, bool utf16, bool xbox)
        {
            bw.WriteUInt16(Unk00);
            bw.WriteUInt16(0);

            if (Designs.Count > ReservedDesignCount)
                throw new InvalidOperationException($"Invalid design count: {Designs.Count}; Max: {ReservedDesignCount}");
            bw.WriteUInt16((ushort)Designs.Count);
            bw.WriteUInt16(0);
            bw.WritePattern(320, 0);

            for (int i = 0; i < Designs.Count; i++)
            {
                Designs[i].Write(bw, utf16, xbox);
            }

            // Write unused design space
            int remaining = ReservedDesignCount - Designs.Count;
            if (remaining > 0)
            {
                int designSize = utf16 ? Design.FileSizeUTF16 : Design.FileSizeShiftJIS;
                bw.WritePattern(designSize * remaining, 0);
            }
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
    }
}
