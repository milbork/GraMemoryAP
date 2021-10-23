namespace GraMemory.Klasy
{
    class GameSettings
    {
        public int MaxScore;
        public int GameTime;
        public int ShowTime;
        public int Size;
        public int Score;
        public int Rows;
        public int Columns;

        public GameSettings()
        {
            StartSettings();
        }

        public void StartSettings()
        {
            MaxScore = 60;
            GameTime = 90;
            ShowTime = 5;
            Size = 150;
            Score = 0;
            Rows = 6;
            Columns = 4;
        }
    }
}
