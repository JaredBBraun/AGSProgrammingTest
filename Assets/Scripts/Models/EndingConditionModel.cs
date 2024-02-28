using System;

public class EndingConditionModel : IEndingConditionModel
{
    private const int TOTAL_MAX_ROTATION_CHANGES = 10;
    private int currentRotationToggleChanges;
    private int currentRotationDirectionChanges;
    public event Action OnEndingConditionMet;
    public event Action OnQuit;
    public event Action OnTryAgain;

    public void OnRotationDirectionEditMade(int amount)
    {
        currentRotationDirectionChanges = amount;
        CheckEndingCondition();
    }

    private void CheckEndingCondition()
    {
        if ((currentRotationDirectionChanges + currentRotationToggleChanges) >= TOTAL_MAX_ROTATION_CHANGES)
        {
            OnEndingConditionMet?.Invoke();
        }
    }

    public void OnRotationToggleEditMade(int amount)
    {
        currentRotationToggleChanges = amount;
        CheckEndingCondition();
    }

    public void TryAgain()
    {
        currentRotationToggleChanges = 0;
        currentRotationDirectionChanges = 0;
        OnTryAgain?.Invoke();
    }

    public void Quit()
    {
        OnQuit?.Invoke();
    }
}

public interface IEndingConditionModel
{
    void OnRotationDirectionEditMade(int amount);
    void OnRotationToggleEditMade(int amount);
    event Action OnEndingConditionMet;
    event Action OnQuit;
    event Action OnTryAgain;
    void TryAgain();
    void Quit();

}