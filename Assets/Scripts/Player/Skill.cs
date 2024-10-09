using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour, IDataPersistence
{
    [SerializeField] PlayerHealthManager healthManager;
    [SerializeField] PlayerMovement movement;
    [SerializeField] Attributes attributes;
    [SerializeField] WeaponAttack attack;
    [SerializeField] PlayerEXP playerExp;
    public SkillTree skillTree;
    // Start is called before the first frame update
    void Start()
    {
        playerExp.availableSkillPoints.skillPoints = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UnlockSkill1()
    {
        //healthManager.maxHealth += healthManager.maxHealth * 0.1f;
        attributes.SetAttributes("ConTextNumber");
        healthManager.SetHealth();
    }
    public void UnlockSkill2()
    {
        attack.damageToGive += 1;
    }
    public void UnlockSkill4()
    {
        healthManager.defense += 1;
    }    
    public void UnlockSkill8()
    {
        movement.startingMoveSpeed += 2;
    }
    public void UnlockSkill10()
    {
        healthManager.defense += 3;
    }

    public void LoadData(GameData data)
    {
        this.playerExp.availableSkillPoints.skillPoints = data.availableSkillPoints;
        this.skillTree.leftSkills = 0;
        this.skillTree.rightSkills = 0;

        if (data.skill1Activated)
        {
            this.skillTree.skill1Activated = data.skill1Activated;
            skillTree.Skill1();
        }
        if (data.skill2Activated)
        {
            this.skillTree.skill2Activated = data.skill2Activated;
            skillTree.Skill2();
        }
        if (data.skill3Activated)
        {
            this.skillTree.skill3Activated = data.skill3Activated;
            skillTree.Skill3();
        }
        if (data.skill4Activated)
        {
            this.skillTree.skill4Activated = data.skill4Activated;
            skillTree.Skill4();
        }
        if (data.skill5Activated)
        {
            this.skillTree.skill5Activated = data.skill5Activated;
            skillTree.Skill5();
        }
        if (data.skill6Activated)
        {
            this.skillTree.skill6Activated = data.skill6Activated;
            skillTree.Skill6();
        }
        if (data.skill7Activated)
        {
            this.skillTree.skill7Activated = data.skill7Activated;
            skillTree.Skill7();
        }
        if (data.skill8Activated)
        {
            this.skillTree.skill8Activated = data.skill8Activated;
            skillTree.Skill8();
        }
        if (data.skill9Activated)
        {
            this.skillTree.skill9Activated = data.skill9Activated;
            skillTree.Skill9();
        }
        if (data.skill10Activated)
        {
            this.skillTree.skill10Activated = data.skill10Activated;
            skillTree.Skill10();
        }
    }

    public void SaveData(GameData data)
    {
        data.skill1Activated = this.skillTree.skill1Activated;
        data.skill2Activated = this.skillTree.skill2Activated;
        data.skill3Activated = this.skillTree.skill3Activated;
        data.skill4Activated = this.skillTree.skill4Activated;
        data.skill5Activated = this.skillTree.skill5Activated;
        data.skill6Activated = this.skillTree.skill6Activated;
        data.skill7Activated = this.skillTree.skill7Activated;
        data.skill8Activated = this.skillTree.skill8Activated;
        data.skill9Activated = this.skillTree.skill9Activated;
        data.skill10Activated = this.skillTree.skill10Activated;
        data.leftSkills = this.skillTree.leftSkills;
        data.rightSkills = this.skillTree.rightSkills;
        data.availableSkillPoints = this.playerExp.playerLevel;
    }

}


