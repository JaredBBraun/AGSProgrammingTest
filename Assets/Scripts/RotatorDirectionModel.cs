using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using UnityEngine;

public class RotatorDirectionModel : IRotatorDirectionModel
{
    private Vector2 startingMousePosition;
    private Timer editEnableStopWatch;
    private const float POINTER_DRAG_MIN_AMOUNT = 1;
    private const string EDIT_DIRECTION_TIMER_COOLDOWN = "EditEnablerTimerCooldown";
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
        CoroutineTimerHandler.StartTimer(EDIT_DIRECTION_TIMER_COOLDOWN, 3.0f, () =>
       {
           isRotatorEditEnabled = true;
           OnDirectionEditEnable?.Invoke(true);
       });
    }

    public void StoreStartingDragPosition(Vector2 startPosition)
    {
        startingMousePosition = startPosition;
    }
}

public interface IRotatorDirectionModel : IRotatorBaseModel
{
    event Action<bool> OnDirectionEditEnable;
    void PossibleFlipDirection(Vector2 vector);
    void StoreStartingDragPosition(Vector2 vector);
}