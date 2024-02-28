using System;

public class RotatorLogicController
{
    public event Action OnQuit;
    public RotatorLogicController(IRotatorView rotatorView, IRotatorToggleModel rotatorModel, IRotatorEditView rotatorButtonView,
    IRotatorDirectionView[] rotatorDirectionView, IRotatorDirectionModel rotatorDirectionModel, IEndingConditionModel endingConditionModel,
    IEndingConditionView endingConditionView)
    {
        //duplicate work but needed to handle lever and lever base dragging
        foreach (IRotatorDirectionView directionChanger in rotatorDirectionView)
        {
            directionChanger.OnPointerDown += rotatorDirectionModel.StoreStartingDragPosition;
            directionChanger.OnPointerUp += rotatorDirectionModel.PossibleFlipDirection;
            rotatorDirectionModel.OnDirectionEditEnable += directionChanger.EnableDrag;
            rotatorDirectionModel.OnEditValueChanged += directionChanger.SetTotalEditsText;
        }
        rotatorDirectionModel.OnEditValueChanged += endingConditionModel.OnRotationDirectionEditMade;

        rotatorDirectionModel.OnDirectionEditEnable += rotatorView.ChangeRotationDirection;
        rotatorButtonView.AddListener(rotatorModel.ToggleRotation);
        rotatorModel.OnEditValueChanged += rotatorButtonView.SetTotalEditsText;
        rotatorModel.OnEditValueChanged += endingConditionModel.OnRotationToggleEditMade;
        rotatorModel.OnRotationToggled += rotatorView.ToggleRotation;

        endingConditionModel.OnEndingConditionMet += () => endingConditionView.ShowEndingVisual(true);
        endingConditionView.OnTryAgain += endingConditionModel.TryAgain;
        endingConditionModel.OnTryAgain += () =>
        {
            endingConditionView.ShowEndingVisual(false);
            rotatorModel.Reset();
            rotatorDirectionModel.Reset();
            rotatorView.Reset();
        };

        endingConditionView.OnQuit += endingConditionModel.Quit;
        endingConditionModel.OnQuit += () => OnQuit?.Invoke();

    }
}
