using Edoke.IO;

namespace AcSaveFormats.ArmoredCoreForAnswer.Decals
{
    public class Decal
    {
        public const int LayerCount = 8;

        public DecalLayer[] Layers { get; private set; }

        public Decal()
        {
            Layers = new DecalLayer[LayerCount];
            for (int i = 0; i < LayerCount; i++)
            {
                Layers[i] = new DecalLayer();
            }
        }

        internal Decal(BinaryStreamReader br)
        {
            Layers = new DecalLayer[LayerCount];
            for (int i = 0; i < LayerCount; i++)
            {
                Layers[i] = new DecalLayer(br);
            }
        }

        internal void Write(BinaryStreamWriter bw)
        {
            for (int i = 0; i < LayerCount; i++)
            {
                Layers[i].Write(bw);
            }
        }
    }
}
