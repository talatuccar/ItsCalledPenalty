public struct BallKickedEvent
{
    public ShotData ShotData { get; }
    public float TimingScore { get; } // 0 ile 1 arasý gelen deðer

    public BallKickedEvent(ShotData shotData, float timingScore)
    {
        ShotData = shotData;
        TimingScore = timingScore;
    }
}