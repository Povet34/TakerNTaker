using Goldmetal.UndeadSurvivor;
using IngameSkill;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelector : MonoBehaviour
{
    RectTransform rect;

    [SerializeField] GameObject selectSkillPrefab;
    [SerializeField] Transform skillGroup;
    List<SelectSkill> selectSkills = new List<SelectSkill>();

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void Show(int chestLevel)
    {
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);

        LoadSelectableSkills();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);
    }

    private void LoadSelectableSkills()
    {
        DestoryRemainSkillSelectors();

        var skillDatas = SkillDataMngr.instance.GetAll();
        for (int i = 0; i < skillDatas.Count; i++)
        {
            var go = Instantiate(selectSkillPrefab, skillGroup);
            var select = go.GetComponent<SelectSkill>();
            select.Init(skillDatas[i], Hide);

            selectSkills.Add(select);
        }
    }

    private void DestoryRemainSkillSelectors()
    {
        if (null == selectSkills)
            return;

        foreach (var select in selectSkills) 
            Destroy(select.gameObject);
        
        selectSkills.Clear();
    }
    
    public void Select(int index)
    {
        selectSkills[index].OnClick(Hide);
    }
}
