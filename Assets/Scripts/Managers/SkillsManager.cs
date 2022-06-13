using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class SkillsManager : MonoBehaviour
{
    private List<Skill> _learnedSkills = new List<Skill>();

    private static Dictionary<Skill, Skill> _blockedSkills = new Dictionary<Skill, Skill>
    {
        { Skill.CanUseSpears,  Skill.AutoTargetSpears },
        { Skill.EnlargedAttackAngle, Skill.MaximumAttackAngle },
    };

    private List<Skill> _availableSkills = new List<Skill>(
        Enum.GetValues(typeof(Skill)).Cast<Skill>()
        );

    private PlayerUnit _playerUnit;

    private void Start()
    {
        _playerUnit = GameObject.Find("Player").GetComponent<PlayerUnit>();

        foreach (var skill in _blockedSkills.Values)
        {
            _availableSkills.Remove(skill);
        }
    }

    public bool LearnSkill(Skill skill)
    {
        if (!_learnedSkills.Contains(skill) && _availableSkills.Contains(skill))
        {
            _learnedSkills.Add(skill);
            ExecuteMethodByEnumValue(skill);
            _availableSkills.Remove(skill);
            if (_blockedSkills.ContainsKey(skill))
            {
                _availableSkills.Add(_blockedSkills[skill]);
                return true;
            }
        }
        return false;
    }
    private void ExecuteMethodByEnumValue(Skill skill)
    {
        var skillName = Enum.GetName(typeof(Skill), skill);
        typeof(SkillsManager).GetMethod(skillName);
    }

    private object GetRandomUnlearnedSkillOrDefault()
    {
        if (_availableSkills.Count > 0)
            return _availableSkills[Random.Range(0, _availableSkills.Count)];
        else
            return null;
    }

    public List<Skill> GetSkills()
    {
        var skillsList = new List<Skill>();

        while (_availableSkills.Count > 0)
        {
            var skill = GetRandomUnlearnedSkillOrDefault();

            if (skill == null || _availableSkills.Count <= skillsList.Count) break;

            if (skillsList.Contains((Skill)skill)) continue;

            skillsList.Add((Skill)skill);
        }

        return skillsList;
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