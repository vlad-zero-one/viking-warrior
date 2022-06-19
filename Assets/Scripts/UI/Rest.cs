using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Rest : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SkillsManager skillsManager;
    [SerializeField] private GameObject skillButtonPrefab;
    [SerializeField] private Transform buttonsRoot;

    private GameObject _player;
    private PlayerUnit _playerUnit;
    private Transform _chooseSkillTransform;
    private bool _learnLater = false;
    private bool _openedSkillChooser = false;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerUnit = _player.GetComponent<PlayerUnit>();
        _chooseSkillTransform = transform.Find("ChooseSkill");
    }

    public void OnPointerClick(PointerEventData data)
    {
        // if ChooseSkill window already opened, second click on Rest button will call LearnLater()
        if (_openedSkillChooser)
        {
            LearnLater();
        }
        else
        {
            // heal player to the maximum
            _playerUnit.SetMaximumHealth();
            // if its not call after LearnLater()
            if (!_learnLater)
            {
                // getting three or less random available skills
                var skills = skillsManager.GetSkills();
                // if there are available skills and player has skill points let player choose skill
                //(ChooseSkill GameObject already activated from the Inspector by pressing Rest button)
                if (skills.Count > 0 && _playerUnit.AvailableSkillPoints > 0)
                {
                    _openedSkillChooser = true;
                    foreach (var skillData in skills)
                    {
                        InstantiateSkillButton(skillData);
                    }
                }
                // else deactivate ChooseSkill window
                else
                {
                    _chooseSkillTransform.gameObject.SetActive(false);
                    _openedSkillChooser = false;
                }
            }
            else
            {
                _chooseSkillTransform.gameObject.SetActive(true);
                _openedSkillChooser = true;
            }
        }
    }

    private void InstantiateSkillButton(SkillsMapItem skillData)
    {
        var button = Instantiate(skillButtonPrefab, buttonsRoot);

        button.GetComponent<Button>()
            .onClick
            .AddListener(delegate {
                skillsManager.LearnSkill(skillData.Skill);
                _playerUnit.AvailableSkillPoints--;
                CloseSkillChooser();
            });

        button.GetComponent<Image>().overrideSprite = skillData.Sprite;

        button.GetComponentInChildren<Text>().text = skillData.Description;
    }

    private void CloseSkillChooser()
    {
        foreach (Transform button in buttonsRoot)
        {
            Destroy(button.gameObject);
        }

        _learnLater = false;
        _chooseSkillTransform.gameObject.SetActive(false);
        _openedSkillChooser = false;
    }

    // save the skills in window and just close it
    public void LearnLater()
    {
        _learnLater = true;
        _chooseSkillTransform.gameObject.SetActive(false);
        _openedSkillChooser = false;
    }
}
