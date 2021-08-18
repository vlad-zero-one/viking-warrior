using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private PlayerUnit _playerUnit;
    private float _experienceToTheNextLevel;
    private float _currentExperience;


    void Start()
    {
        // if not bind from inspector use GameObject.Find
        if (_playerUnit == null)
            _playerUnit = GameObject.Find("Player").GetComponent<PlayerUnit>();
        if (_slider == null)
            _slider = GameObject.Find("ExpBar/Slider").GetComponent<Slider>();

        _experienceToTheNextLevel = _playerUnit.ExperienceToTheNextLevel;
        _currentExperience = _playerUnit.Experience;
        _slider.value = _currentExperience / _experienceToTheNextLevel;
    }

    public void OnChangeExperience(float experience)
    {
        _slider.value += (experience / _experienceToTheNextLevel);
    }

    public void OnLevelUp()
    {
        _experienceToTheNextLevel = _playerUnit.ExperienceToTheNextLevel;
    }
}
