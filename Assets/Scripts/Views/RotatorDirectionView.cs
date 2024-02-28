using System;
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
        EventTrigger.Entry entryDown = new EventTrigger.Entry();
        entryDown.eventID = EventTriggerType.PointerDown;
        entryDown.callback.AddListener(OnClickDown);
        rotatorToggleButton.triggers.Add(entryDown);


        EventTrigger.Entry entryUp = new EventTrigger.Entry();
        entryUp.eventID = EventTriggerType.EndDrag; //use end drag to get mouse/pointer data even if we leave the graphic rect
        entryUp.callback.AddListener(OnClickUp);
        rotatorToggleButton.triggers.Add(entryUp);
    }

    private void OnClickUp(BaseEventData baseEventData)
    {
        if (!isDraggingEnabled)
        {
            return;
        }

        Vector2 endPosition = Input.mousePosition; //should be using BaseEventData arg but we are cheating here, TODO: fix this to be more consistent with pointer data
        OnPointerUp?.Invoke(endPosition);
    }

    private void OnClickDown(BaseEventData baseEventData)
    {
        if (!isDraggingEnabled)
        {
            return;
        }
        PointerEventData arg01 = ((PointerEventData)baseEventData);
        OnPointerDown?.Invoke(arg01.pointerCurrentRaycast.screenPosition);
    }

    public void AddListener(UnityAction unityAction)
    {
        throw new System.NotImplementedException(); //needed in interface then?
    }

    public void SetTotalEditsText(int edits)
    {
        totalRotations.text = edits.ToString();
    }

    public void EnableDrag(bool isDraggingEnabled)
    {
        this.isDraggingEnabled = isDraggingEnabled;
        if (isDraggingEnabled)
        {
            levelPivotBase.rotation = Quaternion.Euler(0f, 0f, 5f);
        }
        else
        {
            levelPivotBase.rotation = Quaternion.Euler(0f, 0f, 95f);

        }
    }
}

public interface IRotatorDirectionView : IRotatorEditView
{
    event Action<Vector2> OnPointerDown;
    event Action<Vector2> OnPointerUp;

    void EnableDrag(bool isDraggingEnabled);
}