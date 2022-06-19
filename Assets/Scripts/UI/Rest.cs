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
    private Skills _skillsComponent;
    private Transform _chooseSkillTransform, _innerSkillsTransform;
    private bool _learnLater = false;
    private bool _openedSkillChooser = false;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerUnit = _player.GetComponent<PlayerUnit>();
        _skillsComponent = _player.GetComponent<Skills>();
        _chooseSkillTransform = transform.Find("ChooseSkill");
        _innerSkillsTransform = _chooseSkillTransform.Find("Skills");
    }

    //public void OnPointerClick(PointerEventData data)
    //{
    //    // if ChooseSkill window already opened, second click on Rest button will call LearnLater()
    //    if (_openedSkillChooser)
    //    {
    //        LearnLater();
    //    }
    //    else
    //    {
    //        // heal player to the maximum
    //        _playerUnit.SetMaximumHealth();
    //        // if its not call after LearnLater()
    //        if (!_learnLater)
    //        {
    //            // getting three or less random available skills
    //            string[] skills = _skillsComponent.ChooseSkill();
    //            // if there are available skills and player has skill points let player choose skill
    //            //(ChooseSkill GameObject already activated from the Inspector by pressing Rest button)
    //            if (skills.Length > 0 && _playerUnit.AvailableSkillPoints > 0)
    //            {
    //                _openedSkillChooser = true;
    //                foreach (string skillName in skills)
    //                {
    //                    CreateButton(skillName);
    //                }
    //            }
    //            // else deactivate ChooseSkill window
    //            else
    //            {
    //                _chooseSkillTransform.gameObject.SetActive(false);
    //                _openedSkillChooser = false;
    //            }
    //        }
    //        else
    //        {
    //            _chooseSkillTransform.gameObject.SetActive(true);
    //            _openedSkillChooser = true;
    //        }
    //    }
    //}

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
                CloseSkillChooser2();
            });

        button.GetComponent<Image>().overrideSprite = skillData.Sprite;

        button.GetComponentInChildren<Text>().text = skillData.Description;
    }

    // closing Skill Choose
    private void CloseSkillChooser2()
    {
        foreach (Transform button in buttonsRoot)
        {
            Destroy(button.gameObject);
        }

        _learnLater = false;
        _chooseSkillTransform.gameObject.SetActive(false);
        _openedSkillChooser = false;
    }
    private void CreateButton(string name)
    {
        var prefabButton = Resources.Load("ChooseSkillButtons/" + name);
        GameObject instantiatedButton = (GameObject)Instantiate(prefabButton, _innerSkillsTransform);
        instantiatedButton.GetComponent<Button>().onClick.AddListener(delegate { _skillsComponent.LearnSkill(name); });
        instantiatedButton.GetComponent<Button>().onClick.AddListener(delegate { _playerUnit.AvailableSkillPoints--; });
        instantiatedButton.GetComponent<Button>().onClick.AddListener(CloseSkillChooser);
    }

    // closing Skill Choose
    private void CloseSkillChooser()
    {
        for (int i = 0; i < _innerSkillsTransform.childCount; i++)
        {
            Destroy(_innerSkillsTransform.GetChild(i).gameObject);
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
