using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorLogicLoader : MonoBehaviour
{
    [SerializeField]
    private RotatorView rotatorView;
    [SerializeField]
    private RotatorButtonView rotatorButtonView;
    [SerializeField]
    private RotatorDirectionView rotatorDirectionView;
    private RotatorController rotatorController;

    // Start is called before the first frame update
    void Start()
    {
        RotatorModel rotatorModel = new RotatorModel();
        RotatorDirectionModel rotatorDirectionModel = new RotatorDirectionModel();

        rotatorController = new RotatorController(rotatorView, rotatorModel, rotatorButtonView,
        rotatorDirectionView, rotatorDirectionModel);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rotatorController.Test();
        }
    }
}
