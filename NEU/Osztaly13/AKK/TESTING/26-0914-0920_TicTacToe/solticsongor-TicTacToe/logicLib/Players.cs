namespace logicLib
{
    public class Players
    {
        public int PlayerTurn { get; init; }
        public string CharacterName { get; init; }
        public bool PlayerStep { get; init; }

        public Players(string datas)
        {
            string[] data = datas.Split(";");
            PlayerTurn = int.Parse(data[0]);
            CharacterName = data[1];
        }
    }
}
