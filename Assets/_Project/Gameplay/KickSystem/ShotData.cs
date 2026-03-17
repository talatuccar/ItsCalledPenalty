public struct ShotData
{
    public float Horizontal;   // 0-1
    public float Vertical;     // 0-1

    // Gelecek için:
    public float Power;
    public float Curve;
   

    public ShotData(float horizontal, float vertical)
    {
        Horizontal = horizontal;
        Vertical = vertical;

        Power = 1f;
        Curve = 0f;
     
    }
}