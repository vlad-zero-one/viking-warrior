using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private const string TestSceneName = "TestScene";

    public void LoadTestScene()
    {
        SceneManager.LoadScene(TestSceneName);
    }
}