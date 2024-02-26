using System;

public class RotatorModel : IRotatorModel
{
    private const int TOTAL_MAX_ROTATION_CHANGES = 10;
    public event Action<int> OnEditValueChanged;
    public event Action<int> OnRotationValueChanged;
    public event Action OnRotationDirectionChanged;
    public event Action OnRotationToggled;
    private int totalRotationToggles;
    public void ToggleRotation()
    {
        // if (totalRotationToggles + totalDirectionChanges < TOTAL_MAX_ROTATION_CHANGES)
        {
            //totalRotationToggles++;
            OnEditValueChanged?.Invoke(totalRotationToggles);
            OnRotationToggled?.Invoke();
        }

    }

    public void ToggleRotationDirection()
    {
        //if (totalRotationToggles + totalDirectionChanges < TOTAL_MAX_ROTATION_CHANGES)
        {
            //totalDirectionChanges++;
            //OnRotationValueChanged?.Invoke(totalDirectionChanges);
            //OnRotationDirectionChanged?.Invoke();
        }
    }

}

public interface IRotatorModel : IRotatorBaseModel
{
    event Action OnRotationToggled;
    void ToggleRotation();
    //void ToggleRotationDirection();
}

public interface IRotatorBaseModel
{
    event Action<int> OnEditValueChanged;

}