namespace kincsesladaLib
{
    public class KincsFactory
    {
        private Random random;

        public KincsFactory()
        {
            random = new Random();
        }

        public IKincs Create()
        {
            int kincsType = random.Next(2);

            if (kincsType == 0)
            {
                int ermeType = random.Next(3);
                return new Erme(ermeType);
            }
            else
            {
                int dragakoType = random.Next(3);
                int dragakoSize = random.Next(1, 4);
                return new Dragako(dragakoType, dragakoSize);
            }
        }
    }
}