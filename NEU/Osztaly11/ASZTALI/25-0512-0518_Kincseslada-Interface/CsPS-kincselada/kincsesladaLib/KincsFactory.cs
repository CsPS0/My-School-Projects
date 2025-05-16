namespace kincsesladaLib
{
    public class KincsFactory
    {
        readonly Random random = new();

        public IKincs Create()
        {
            int kincsTipus = random.Next(2);

            if (kincsTipus == 0)
            {
                int ermeTipus = random.Next(3);
                return new Erme(ermeTipus);
            }
            else
            {
                int dragakoTipus = random.Next(3);
                int dragakoMeret = random.Next(1, 4);
                return new Dragako(dragakoTipus, dragakoMeret);
            }
        }
    }
}