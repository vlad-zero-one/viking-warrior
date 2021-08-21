using UnityEngine;

public class Bonfire : MonoBehaviour
{
    [SerializeField] private GameObject _restButton;
    //GameObject _chooseSkillGameObject;

    private void Start()
    {
        if (_restButton == null)
        {
            _restButton = GameObject.Find("MobileInputCanvas").transform.Find("Rest").gameObject;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //_chooseSkillGameObject = GameObject.Find("ChooseSkill");
        if (!collider.isTrigger && collider.CompareTag("Player"))
        {
            _restButton.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        //_chooseSkillGameObject.GetComponent<ChooseSkill>().LearnLater();
        if (!collider.isTrigger && collider.CompareTag("Player"))
        {
            // if ChooseSkill window is active call LearnLater()
            Transform chooseSkill = _restButton.transform.Find("ChooseSkill");
            if (chooseSkill)
            {
                if (chooseSkill.gameObject.activeSelf)
                {
                    _restButton.GetComponent<Rest>().LearnLater();
                }
            }
            _restButton.SetActive(false);
        }
    }
}
