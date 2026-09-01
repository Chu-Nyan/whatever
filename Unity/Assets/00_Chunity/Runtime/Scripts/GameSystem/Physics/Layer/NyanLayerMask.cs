namespace Chunity.GameSystem.Physics
{
    public struct NyanLayerMask
    {
        public int Mask;

        public NyanLayerMask(int mask)
        {
            Mask = mask;
        }

        public NyanLayerMask(NyanLayer a)
        {
            Mask = (int)a;
        }

        public NyanLayerMask(NyanLayer a, NyanLayer b)
        {
            Mask = (int)a | (int)b;
        }

        public NyanLayerMask(NyanLayer a, NyanLayer b, NyanLayer c)
        {
            Mask = (int)a | (int)b | (int)c;
        }

        public readonly bool ContainsLayer(NyanLayer layer)
        {
            int value = (int)layer;
            return value == (Mask & value);
        }

        public readonly bool ContainsLayer(int layer)
        {
            return layer == (Mask & layer);
        }
    }
}
