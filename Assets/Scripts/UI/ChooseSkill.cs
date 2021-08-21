using UnityEngine;
using UnityEngine.UI;

public class ChooseSkill : MonoBehaviour
{
    private GameObject _player;
    private PlayerUnit _playerUnit;
    private Skills _skillsComponent;
    private Transform _innerSkillsGameObject;
    private bool _learnLater = false;

    private void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerUnit = _player.GetComponent<PlayerUnit>();
        // heal player to the maximum
        _playerUnit.Healthpoints = _playerUnit.MaximumHealthpoints;
 
        if (!_learnLater)
        {
            _skillsComponent = _player.GetComponent<Skills>();
            _innerSkillsGameObject = transform.Find("Skills");
            string[] skills = _skillsComponent.ChooseSkill();
            // if no more available skills or no more skill points deactivate choosing
            if (skills.Length == 0 || _playerUnit.AvailableSkillPoints == 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                foreach (string skillName in skills)
                {
                    CreateButton(skillName);
                }
            }
        }
    }
    
    private void CreateButton(string name)
    {
        var prefabButton = Resources.Load("ChooseSkillButtons/" + name);
        GameObject instantiatedButton = (GameObject)Instantiate(prefabButton, _innerSkillsGameObject);
        instantiatedButton.GetComponent<Button>().onClick.AddListener( delegate { _skillsComponent.LearnSkill(name); } );
        instantiatedButton.GetComponent<Button>().onClick.AddListener( delegate { _playerUnit.AvailableSkillPoints--; } );
        instantiatedButton.GetComponent<Button>().onClick.AddListener(CloseSkillChooser);
    }
    
    private void CloseSkillChooser()
    {
        for (int i = 0; i < _innerSkillsGameObject.childCount; i++)
        {
            Destroy(_innerSkillsGameObject.GetChild(i).gameObject);
        }
        _learnLater = false;
        gameObject.SetActive(false);
    }

    public void LearnLater()
    {
        _learnLater = true;
        gameObject.SetActive(false);
    }
}
