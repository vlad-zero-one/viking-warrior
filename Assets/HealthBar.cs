using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private PlayerUnit _playerUnit;
    private float _maxHealth;
    private float _currentHealth;


    void Start()
    {
        // if not bind from inspector use GameObject.Find
        if (_playerUnit == null)
            _playerUnit = GameObject.Find("Player").GetComponent<PlayerUnit>();
        if (_slider == null)
            _slider = GameObject.Find("HealthBar/Slider").GetComponent<Slider>();

        _maxHealth = _playerUnit.MaximumHealthpoints;
        _currentHealth = _playerUnit.Healthpoints;
        _slider.value = _currentHealth / _maxHealth;
    }

    public void OnTakeDamage(float damage)
    {
        _slider.value -= (damage / _maxHealth);
    }
    
}
