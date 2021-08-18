using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public GameObject restButton;
    //GameObject _chooseSkillGameObject;

    void OnTriggerEnter2D(Collider2D collider)
    {
        //_chooseSkillGameObject = GameObject.Find("ChooseSkill");
        if (!collider.isTrigger && collider.CompareTag("Player"))
        {
            restButton.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        //_chooseSkillGameObject.GetComponent<ChooseSkill>().LearnLater();
        if (!collider.isTrigger && collider.CompareTag("Player"))
        {
            // if ChooseSkill window is active call LearnLater()
            Transform chooseSkill = restButton.transform.Find("ChooseSkill");
            if (chooseSkill)
            {
                if (chooseSkill.gameObject.activeSelf)
                {
                    restButton.GetComponent<Rest>().LearnLater();
                }
            }
            restButton.SetActive(false);
        }
    }
}
