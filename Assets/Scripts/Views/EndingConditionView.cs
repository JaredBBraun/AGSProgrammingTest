using System;
using UnityEngine;
using UnityEngine.UI;

public class EndingConditionView : MonoBehaviour, IEndingConditionView
{
    public event Action OnTryAgain;
    public event Action OnQuit;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private Button tryAgainButton, quitButton;

    private void Start()
    {
        tryAgainButton.onClick.AddListener(() => OnTryAgain.Invoke());
        quitButton.onClick.AddListener(() => OnQuit.Invoke());
    }
    public void ShowEndingVisual(bool show)
    {
        canvasGroup.alpha = (show) ? 1 : 0;
        canvasGroup.interactable = show;
        canvasGroup.blocksRaycasts = show;
    }

}

public interface IEndingConditionView
{
    event Action OnTryAgain;
    event Action OnQuit;
    void ShowEndingVisual(bool show);
}