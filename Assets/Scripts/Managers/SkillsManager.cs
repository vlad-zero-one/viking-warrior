﻿using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class SkillsManager : MonoBehaviour
{
    private const int SkillsReturnedInGetSkills = 3;

    [SerializeField] private SkillsMap _skillsMap;
    
    public SkillsMap SkillsConfig => _skillsMap;

    private List<Skill> _learnedSkills = new List<Skill>();

    private Dictionary<Skill, Skill> _blockedSkills = new Dictionary<Skill, Skill>
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
        //var skillName = Enum.GetName(typeof(Skill), skill);
        //typeof(SkillsManager).GetMethod(skillName);

        switch (skill)
        {
            case Skill.EnlargedSightAngle:
                EnlargedSightAngle();
                break;
            case Skill.EnlargedAttackAngle:
                EnlargedAttackAngle();
                break;
            case Skill.MaximumAttackAngle:
                MaximumAttackAngle();
                break;
            case Skill.EnlargedMaximumDamagablesToAttack:
                EnlargedMaximumDamagablesToAttack();
                break;
            case Skill.CanUseTwoUltimatesAtOnce:
                CanUseTwoUltimatesAtOnce();
                break;
            case Skill.CanUseSpears:
                CanUseSpears();
                break;
            case Skill.AutoTargetSpears:
                AutoTargetSpears();
                break;
            case Skill.EnhancedLuck:
                EnhancedLuck();
                break;
        }
    }

    private object GetRandomUnlearnedSkillOrDefault()
    {
        if (_availableSkills.Count > 0)
            return _availableSkills[Random.Range(0, _availableSkills.Count)];
        else
            return null;
    }

    public List<SkillsMapItem> GetSkills()
    {
        var skillsList = new List<Skill>();

        while (_availableSkills.Count > 0 && skillsList.Count < SkillsReturnedInGetSkills)
        {
            var skill = GetRandomUnlearnedSkillOrDefault();

            if (skill == null || _availableSkills.Count <= skillsList.Count) break;

            if (skillsList.Contains((Skill)skill)) continue;

            skillsList.Add((Skill)skill);
        }

        return _skillsMap.Values.Where(skillData => skillsList.Contains(skillData.Skill)).ToList();
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
        Debug.Log("TBD");
    }
    private void CanUseSpears()
    {
        Debug.Log("TBD");
    }
    private void AutoTargetSpears()
    {
        Debug.Log("TBD");
    }
    private void EnhancedLuck()
    {
        Debug.Log("TBD");
    }
}