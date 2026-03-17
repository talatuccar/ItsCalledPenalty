public struct BallKickedEvent
{
    public ShotData ShotData;

    public BallKickedEvent(ShotData shotData)
    {
        ShotData = shotData;
    }
}