using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorController
{
    private IRotatorView rotatorView;
    private IRotatorModel rotatorModel;
    private IRotatorDirectionView rotatorDirectionView;
    private IRotatorEditView rotatorButtonView;
    public RotatorController(IRotatorView rotatorView, IRotatorModel rotatorModel, IRotatorEditView rotatorButtonView,
    IRotatorDirectionView rotatorDirectionView, IRotatorDirectionModel rotatorDirectionModel)
    {
        this.rotatorView = rotatorView;
        this.rotatorModel = rotatorModel;

        this.rotatorDirectionView = rotatorDirectionView;
        this.rotatorButtonView = rotatorButtonView;

        rotatorDirectionView.OnPointerDown += rotatorDirectionModel.StoreStartingDragPosition;
        rotatorDirectionView.OnPointerUp += rotatorDirectionModel.PossibleFlipDirection;
        rotatorDirectionModel.OnDirectionEditEnable += rotatorDirectionView.EnableDrag;


        rotatorButtonView.AddListener(rotatorModel.ToggleRotation);
        rotatorModel.OnEditValueChanged += ChangeRotationToggleText;
        rotatorModel.OnRotationToggled += ToggleRotation;
    }

    private void ChangeRotationToggleText(int total)
    {
        rotatorButtonView.SetTotalEditsText(total);
    }

    public void Test()
    {
        rotatorDirectionView.EnableDrag(true);
    }

    public void ToggleRotation()
    {
        rotatorView.ToggleRotation();
    }
}
