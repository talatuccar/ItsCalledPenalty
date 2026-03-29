namespace Project.Core.EventBus
{
    public struct BallKickedEvent
    {
        public ShotData ShotData { get; }
        public float TimingScore { get; } // 0 ile 1 arasý gelen deðer

        public ShotSettings Settings { get; } // Ayarlarý buraya ekledik

        public BallKickedEvent(ShotData shotData, float timingScore, ShotSettings settings)
        {
            ShotData = shotData;
            TimingScore = timingScore;
            Settings = settings;
        }
    }

    public struct GoalScoredEvent
    {
    }

    public struct GoalMissedEvent
    {
    }

    public struct GameStartedEvent
    {
    }

    public struct GameEndedEvent
    {
    }
}