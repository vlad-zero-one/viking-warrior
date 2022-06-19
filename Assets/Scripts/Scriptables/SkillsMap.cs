using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SkillsMap")]
public class SkillsMap : ScriptableObject
{
    public List<SkillsMapItem> Values;
}

[Serializable]
public class SkillsMapItem
{
    public Skill Skill;
    public Sprite Sprite;
    public string Description;
}