namespace GeneticGame.Settings
{
    public class ScoreSettings
    {
        public float ScoreForEating { get; set; }
        public float ScoreForCorrectWay { get; set; }
        public float ScoreForWrongWay { get; set; }
        
        public ScoreSettings()
        {
            ScoreForCorrectWay = 1;
            ScoreForEating = 10;
            ScoreForWrongWay = -1.5f;
        }
    }
}