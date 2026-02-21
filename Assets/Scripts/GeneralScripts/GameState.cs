[System.Serializable]
public class GameState
{
    // ----- Per Day Stats -----
    public int correctToday;
    public int reportedToday;
    public int helpedToday;
    public int flaggedToday;

    // ----- Total Stats -----
    public int totalCorrect;
    public int totalReported;
    public int totalHelped;
    public int totalFlagged;

    public void ResetDailyStats()
    {
        correctToday = 0;
        reportedToday = 0;
        helpedToday = 0;
        flaggedToday = 0;
    }
}
