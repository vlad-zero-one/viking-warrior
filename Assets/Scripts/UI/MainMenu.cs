using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}