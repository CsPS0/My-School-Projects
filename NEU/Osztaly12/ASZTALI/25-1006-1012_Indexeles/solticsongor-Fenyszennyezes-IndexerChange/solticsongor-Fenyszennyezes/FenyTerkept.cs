namespace solticsongor_Fenyszennyezes
{
    public class FenyTerkep
    {
        private int[,] fenypwr;
        public int N { get; private set; }
        public int M { get; private set; }

        public FenyTerkep(string file)
        {
            string[] sorok = File.ReadAllLines(file);
            string[] sor = sorok[0].Split(' ');
            N = int.Parse(sor[0]);
            M = int.Parse(sor[1]);

            fenypwr = new int[N, M];

            for (int i = 0; i < N; i++)
            {
                string[] fenySor = sorok[i + 1].Split('\t');
                for (int j = 0; j < M; j++)
                {
                    fenypwr[i, j] = int.Parse(fenySor[j]);
                }
            }
        }

        public int this[int sor, int oszlop]
        {
            get
            {
                if (sor >= 0 && sor < N && oszlop >= 0 && oszlop < M)
                {
                    return fenypwr[sor, oszlop];
                }
                else
                {
                    throw new IndexOutOfRangeException("Ha nem megy, hát nem megy.");
                }
            }
        }
    }
}