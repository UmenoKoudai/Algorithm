using UnityEngine;

public class Goal : MonoBehaviour, IAction
{
    void IAction.Action()
    {
        GameManager.Instance.StageNumber++;
        SceneMoveSystem.Instance.SceneMove("Sample");
    }
}
