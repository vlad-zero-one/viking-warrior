using UnityEngine;
using UnityEngine.UI;

public class ChooseSkill : MonoBehaviour
{
    GameObject _player;
    PlayerUnit _playerUnit;
    Skills _skillsComponent;

    private void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerUnit = _player.GetComponent<PlayerUnit>();
        _skillsComponent = _player.GetComponent<Skills>();
        string[] skills = _skillsComponent.ChooseSkill();
        _playerUnit.Healthpoints = _playerUnit.MaximumHealthpoints;
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
    
    private void CreateButton(string name)
    {
        var prefabButton = Resources.Load("ChooseSkillButtons/" + name);
        GameObject instantiatedButton = (GameObject)Instantiate(prefabButton, gameObject.transform);
        instantiatedButton.GetComponent<Button>().onClick.AddListener( delegate { _skillsComponent.LearnSkill(name); } );
        instantiatedButton.GetComponent<Button>().onClick.AddListener( delegate { _playerUnit.AvailableSkillPoints--; } );
        instantiatedButton.GetComponent<Button>().onClick.AddListener(CloseSkillChooser);
    }
    
    private void CloseSkillChooser()
    {
        for (int i = 0; i <  gameObject.transform.childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
        gameObject.SetActive(false);
    }
}
