using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private const string TestSceneName = "TestScene";

    [SerializeField] private GameObject _title;
    [SerializeField] private GameObject _buttons;
    [SerializeField] private Slider _loadingProgressBar;

    private AsyncOperation loadingOperation;

    public void LoadTestScene()
    {
        loadingOperation = SceneManager.LoadSceneAsync(TestSceneName);

        _title.SetActive(false);
        _buttons.SetActive(false);
        _loadingProgressBar.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (loadingOperation != null)
        {
            _loadingProgressBar.value = (loadingOperation.progress / 0.9f);
        }
    }
}