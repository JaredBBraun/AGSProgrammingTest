using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotatorDirectionView : MonoBehaviour, IRotatorDirectionView
{
    [SerializeField]
    private Text totalRotations;
    [SerializeField]
    private EventTrigger rotatorToggleButton;
    public event Action<Vector2> OnPointerDown;
    public event Action<Vector2> OnPointerUp;
    [SerializeField]
    private Transform levelPivotBase;
    private bool isDraggingEnabled = true;

    private void Awake()
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener(OnClickDown);
        rotatorToggleButton.triggers.Add(entry);


        EventTrigger.Entry entryUp = new EventTrigger.Entry();
        entryUp.eventID = EventTriggerType.EndDrag; //use end drag to get mouse/pointer data even if we leave the graphic rect
        entryUp.callback.AddListener(OnClickUp);
        rotatorToggleButton.triggers.Add(entryUp);
    }

    private void OnClickUp(BaseEventData arg0)
    {
        if (!isDraggingEnabled)
        {
            return;
        }

        Vector2 endPosition = Input.mousePosition; //should be using BaseEventData arg but we are cheating here, TODO: fix this to be more consistent with pointer data
        OnPointerUp?.Invoke(endPosition);
    }

    private void OnClickDown(BaseEventData arg0)
    {
        if (!isDraggingEnabled)
        {
            return;
        }
        PointerEventData arg01 = ((PointerEventData)arg0);
        OnPointerDown?.Invoke(arg01.pointerCurrentRaycast.screenPosition);
    }

    public void AddListener(UnityAction unityAction)
    {
        throw new System.NotImplementedException();
    }

    public void SetTotalEditsText(int edits)
    {
        throw new System.NotImplementedException();
    }

    public void EnableDrag(bool isDraggingEnabled)
    {
        this.isDraggingEnabled = isDraggingEnabled;
        if (isDraggingEnabled)
        {
            //Debug.LogWarning("on");
            //StartCoroutine(EnableThingLate());
            levelPivotBase.rotation = Quaternion.Euler(0f, 0f, 0f);
            //gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("off");
            levelPivotBase.rotation = Quaternion.Euler(0f, 0f, 90f);

        }
    }

    private IEnumerator EnableThingLate()
    {
        levelPivotBase.rotation = Quaternion.Euler(0f, 0f, 0f);

        yield return null;


        //throw new NotImplementedException();
    }
}

public interface IRotatorDirectionView : IRotatorEditView
{
    event Action<Vector2> OnPointerDown;
    event Action<Vector2> OnPointerUp;

    void EnableDrag(bool isDraggingEnabled);
}