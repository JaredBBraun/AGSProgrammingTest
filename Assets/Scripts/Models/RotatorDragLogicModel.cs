using System;
using UnityEngine;

public class RotatorDragLogicModel : IRotatorDirectionModel
{
    private Vector2 startingMousePosition;
    private const float POINTER_DRAG_MIN_AMOUNT = 1;
    private const float DRAG_TIMER_COOLDOWN_AMOUNT = 2f;
    private bool isRotatorEditEnabled = true;
    public event Action<bool> OnDirectionEditEnable;
    public event Action<int> OnEditValueChanged;
    private int totalDirectionChanges;


    public void PossibleFlipDirection(Vector2 endPosition)
    {
        if (endPosition.y > startingMousePosition.y && (endPosition.y - startingMousePosition.y) > POINTER_DRAG_MIN_AMOUNT)
        {
            totalDirectionChanges++;
            OnEditValueChanged?.Invoke(totalDirectionChanges);
            isRotatorEditEnabled = false;
            OnDirectionEditEnable?.Invoke(isRotatorEditEnabled);
            StartEditEnableCountdown();
        }
    }

    private void StartEditEnableCountdown()
    {
        CoroutineTimerHandler.StartTimer(DRAG_TIMER_COOLDOWN_AMOUNT, () =>
       {
           isRotatorEditEnabled = true;
           OnDirectionEditEnable?.Invoke(true);
       });
    }

    public void StoreStartingDragPosition(Vector2 startPosition)
    {
        startingMousePosition = startPosition;
    }
    public void Reset()
    {
        totalDirectionChanges = 0;
        OnEditValueChanged?.Invoke(totalDirectionChanges);
    }

}

public interface IRotatorDirectionModel : IRotatorBaseModel
{
    event Action<bool> OnDirectionEditEnable;
    void PossibleFlipDirection(Vector2 vector);
    void StoreStartingDragPosition(Vector2 vector);
}