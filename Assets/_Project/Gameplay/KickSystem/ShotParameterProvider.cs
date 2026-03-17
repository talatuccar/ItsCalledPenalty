using UnityEngine;

public class ShotParameterProvider : MonoBehaviour    // it manages sliders
{
    [Header("Oscillators")]
    [SerializeField] private SliderOscillator directionOscillator;
    [SerializeField] private SliderOscillator heightOscillator;

    public enum SelectionState { Direction, Height, Finished }
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
                return false;

            case SelectionState.Height:
                heightOscillator.Stop();
                _currentSelection = SelectionState.Finished;
                return true;

            default:
                return true;
        }
    }


    public ShotData GetShotData()
    {
        
        return new ShotData(
            directionOscillator.GetValue(),
            heightOscillator.GetValue()
        );
    }
}