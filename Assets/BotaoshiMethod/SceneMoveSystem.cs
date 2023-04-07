using UnityEngine.SceneManagement;

public class SceneMoveSystem : InstanceSystem<SceneMoveSystem>
{
    public void SceneMove(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
