using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RotatorButtonView : MonoBehaviour, IRotatorEditView
{
    [SerializeField]
    private Text totalRotations;
    [SerializeField]
    private Button rotatorToggleButton;

    public void AddListener(UnityAction unityAction)
    {
        rotatorToggleButton.onClick.AddListener(unityAction);
    }

    public void SetTotalEditsText(int rotationAmount)
    {
        totalRotations.text = rotationAmount.ToString();
    }
}

public interface IRotatorEditView
{
    void SetTotalEditsText(int edits);
    void AddListener(UnityAction unityAction);
}