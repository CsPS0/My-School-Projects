namespace logicLib
{
    public class GameBoard
    {
        string[,] matrix = new string[3, 3];

        public int playerId = 0;

        public bool Move(int x, int y)
        {
            if (matrix[x, y] == "")
            {
                matrix[x, y] = playerId==1 ? "O" : "X";
                if (playerId == 0)
                {
                    playerId = 1;
                }
                else playerId = 0;
                return true;
            }
            else return false;
        }

        public 

        public GameBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matrix[i, j] = "";
                }
            }
        }
    }
}
