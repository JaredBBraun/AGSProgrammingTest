using UnityEngine;

public class RotatorLogicLoader : MonoBehaviour
{
    [SerializeField]
    private RotatorView rotatorView;
    [SerializeField]
    private RotatorButtonView rotatorButtonView;
    [SerializeField]
    private RotatorDirectionView[] rotatorDirectionViews;
    [SerializeField]
    private EndingConditionView endingConditionView;

    private void Start()
    {
        RotatorToggleModel rotatorModel = new RotatorToggleModel();
        RotatorDragLogicModel rotatorDirectionModel = new RotatorDragLogicModel();
        EndingConditionModel endingConditionModel = new EndingConditionModel();

        RotatorLogicController rotatorController = new RotatorLogicController(rotatorView, rotatorModel, rotatorButtonView,
        rotatorDirectionViews, rotatorDirectionModel, endingConditionModel, endingConditionView);

        rotatorController.OnQuit += () =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#endif

#if UNITY_WEBGL
            //tried the js scripting route to do this but edited default html template instead to just add one function
            Application.ExternalEval("unloadUnityGame();");
#endif
        };
    }
}
