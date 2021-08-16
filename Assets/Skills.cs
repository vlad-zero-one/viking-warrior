using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    private static List<string> _allSkills = new List<string>
    {
        "EnlargedSightAngle",
        "EnlargedAttackAngle",
        "MaximumAttackAngle",
        "EnlargedMaximumDamagablesToAttack",
        "CanUseTwoUltimatesAtOnce",
        "CanUseSpears",
        "AutoTargetSpears",
        "Lucky",
    };

    private static Dictionary<string, string> _closedSkills = new Dictionary<string, string>
    {
        { "CanUseSpears",  "AutoTargetSpears" },
        { "EnlargedAttackAngle", "MaximumAttackAngle" },
    };

    private List<string> _unlearnedSkills;
    public List<string> LearnedSkills = new List<string>();

    PlayerUnit _playerUnit;

    private void LoadSkill(string skillName)
    {
        switch (skillName)
        {
            case "EnlargedSightAngle":
                EnlargedSightAngle();
                break;
            case "EnlargedAttackAngle":
                EnlargedAttackAngle();
                break;
            case "MaximumAttackAngle":
                MaximumAttackAngle();
                break;
            case "EnlargedMaximumDamagablesToAttack":
                EnlargedMaximumDamagablesToAttack();
                break;
            case "CanUseTwoUltimatesAtOnce":
                CanUseTwoUltimatesAtOnce();
                break;
            case "CanUseSpears":
                CanUseSpears();
                break;
            case "AutoTargetSpears":
                AutoTargetSpears();
                break;
            case "Lucky":
                Lucky();
                break;
        }
    }

    private void Start()
    {
        _playerUnit = GameObject.Find("Player").GetComponent<PlayerUnit>();

        _unlearnedSkills = new List<string>(_allSkills);
        foreach (string skill in _closedSkills.Values)
        {
            _unlearnedSkills.Remove(skill);
        }
        /*
        string tmp;

        Debug.Log("Unlearned: " + _unlearnedSkills.Count + ". Learned: " + LearnedSkills.Count);
        tmp = GetRandomUnlearnedSkill();
        Debug.Log(tmp);
        LearnSkill(tmp);

        Debug.Log("Unlearned: " + _unlearnedSkills.Count + ". Learned: " + LearnedSkills.Count);
        tmp = GetRandomUnlearnedSkill();
        Debug.Log(tmp);
        LearnSkill(tmp);

        Debug.Log("Unlearned: " + _unlearnedSkills.Count + ". Learned: " + LearnedSkills.Count);
        tmp = GetRandomUnlearnedSkill();
        Debug.Log(tmp);
        LearnSkill(tmp);

        Debug.Log("Unlearned: " + _unlearnedSkills.Count + ". Learned: " + LearnedSkills.Count);
        tmp = GetRandomUnlearnedSkill();
        Debug.Log(tmp);
        LearnSkill(tmp);

        Debug.Log("Unlearned: " + _unlearnedSkills.Count + ". Learned: " + LearnedSkills.Count);
        tmp = GetRandomUnlearnedSkill();
        Debug.Log(tmp);
        LearnSkill(tmp);

        Debug.Log("Unlearned: " + _unlearnedSkills.Count + ". Learned: " + LearnedSkills.Count);
        tmp = GetRandomUnlearnedSkill();
        Debug.Log(tmp);
        LearnSkill(tmp);

        Debug.Log("Unlearned: " + _unlearnedSkills.Count + ". Learned: " + LearnedSkills.Count);
        tmp = GetRandomUnlearnedSkill();
        Debug.Log(tmp);
        LearnSkill(tmp);

        Debug.Log("Unlearned: " + _unlearnedSkills.Count + ". Learned: " + LearnedSkills.Count);
        tmp = GetRandomUnlearnedSkill();
        Debug.Log(tmp);
        LearnSkill(tmp);

        Debug.Log("Unlearned: " + _unlearnedSkills.Count + ". Learned: " + LearnedSkills.Count);
        tmp = GetRandomUnlearnedSkill();
        Debug.Log(tmp);
        LearnSkill(tmp);

        Debug.Log("Unlearned: " + _unlearnedSkills.Count + ". Learned: " + LearnedSkills.Count);
        tmp = GetRandomUnlearnedSkill();
        Debug.Log(tmp);
        LearnSkill(tmp);

        Debug.Log("Unlearned: " + _unlearnedSkills.Count + ". Learned: " + LearnedSkills.Count);
        tmp = GetRandomUnlearnedSkill();
        Debug.Log(tmp);
        */
    }

    public void LearnSkill(string skillName)
    {
        if (_allSkills.Contains(skillName) && !LearnedSkills.Contains(skillName) && _unlearnedSkills.Contains(skillName))
        {
            LearnedSkills.Add(skillName);
            LoadSkill(skillName);
            _unlearnedSkills.Remove(skillName);
            if (_closedSkills.ContainsKey(skillName))
            {
                _unlearnedSkills.Add(_closedSkills[skillName]);
            }
        }
    }

    private string GetRandomUnlearnedSkill()
    {
        if (_unlearnedSkills.Count > 0)
            return _unlearnedSkills[Random.Range(0, _unlearnedSkills.Count)];
        else
            return "";
    }

    public string[] ChooseSkill()
    {
        string[] returnStrings = new string[0];
 
        if (_unlearnedSkills.Count >= 3)
        {
            returnStrings = new string[3];
            returnStrings[0] = GetRandomUnlearnedSkill();
            returnStrings[1] = GetRandomUnlearnedSkill();
            while (returnStrings[0] == returnStrings[1])
                returnStrings[1] = GetRandomUnlearnedSkill();
            returnStrings[2] = GetRandomUnlearnedSkill();
            while (returnStrings[2] == returnStrings[1] || returnStrings[2] == returnStrings[0])
                returnStrings[2] = GetRandomUnlearnedSkill();
        }
        else if (_unlearnedSkills.Count == 2)
        {
            returnStrings = new string[2];
            returnStrings[0] = GetRandomUnlearnedSkill();
            returnStrings[1] = GetRandomUnlearnedSkill();
            while (returnStrings[0] == returnStrings[1])
                returnStrings[1] = GetRandomUnlearnedSkill();
        }
        else if (_unlearnedSkills.Count == 1)
        {
            returnStrings = new string[1];
            returnStrings[0] = GetRandomUnlearnedSkill();
        }

        return returnStrings;
    }

    private void EnlargedSightAngle()
    {
        _playerUnit.SightAngle = 360;
    }
    private void EnlargedAttackAngle()
    {
        _playerUnit.AttackAngle = 180;
    }
    private void MaximumAttackAngle()
    {
        _playerUnit.AttackAngle = 360;
    }
    private void EnlargedMaximumDamagablesToAttack()
    {
        _playerUnit.MaximumDamagablesToAttack++;
    }
    private void CanUseTwoUltimatesAtOnce()
    {

    }
    private void CanUseSpears()
    {

    }
    private void AutoTargetSpears()
    {

    }
    private void Lucky()
    {

    }
}
