using System;

public class RotatorToggleModel : IRotatorToggleModel
{
    public event Action<int> OnEditValueChanged;
    public event Action OnRotationToggled;
    private int totalRotationToggles;
    public void ToggleRotation()
    {
        totalRotationToggles++;
        OnEditValueChanged?.Invoke(totalRotationToggles);
        OnRotationToggled?.Invoke();
    }



    public void Reset()
    {
        totalRotationToggles = 0;
        OnEditValueChanged?.Invoke(totalRotationToggles);
    }

}

public interface IRotatorToggleModel : IRotatorBaseModel
{
    event Action OnRotationToggled;
    void ToggleRotation();
}

public interface IRotatorBaseModel
{
    event Action<int> OnEditValueChanged;
    void Reset();

}