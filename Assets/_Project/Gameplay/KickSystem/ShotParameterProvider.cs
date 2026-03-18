using UnityEngine;

public class ShotParameterProvider : MonoBehaviour    // it manages sliders
{
    [Header("Oscillators")]
    [SerializeField] private SliderOscillator directionOscillator;
    [SerializeField] private SliderOscillator heightOscillator;

    public enum SelectionState { Direction, Height, Timing, Finished } // Timing eklendi
    private SelectionState _currentSelection = SelectionState.Direction;

   
    public void Initialize()
    {
        _currentSelection = SelectionState.Direction;
        directionOscillator.StartMoving();
        heightOscillator.Stop();
    }


    public bool AdvanceSelection()
    {
        switch (_currentSelection)
        {
            case SelectionState.Direction:
                directionOscillator.Stop();
                heightOscillator.StartMoving();
                _currentSelection = SelectionState.Height;
                return false; // Sadece Direction bitti, halka açýlmamalý.

            case SelectionState.Height:
                heightOscillator.Stop();
                _currentSelection = SelectionState.Timing; // Ýþte þimdi Timing'e geçtik!
                return false; // Hala bir týk daha bekliyoruz (Halka için).

            case SelectionState.Timing:
                _currentSelection = SelectionState.Finished;
                return true; // Her þey bitti, þut çekilebilir.

            default:
                return true;
        }
    }

    // Hangi aþamada olduðumuzu dýþarýya söyleyen küçük bir yardýmcý:
    public SelectionState GetCurrentState() => _currentSelection;
    public ShotData GetShotData()
    {
        
        return new ShotData(
            directionOscillator.GetValue(),
            heightOscillator.GetValue()
        );
    }
}