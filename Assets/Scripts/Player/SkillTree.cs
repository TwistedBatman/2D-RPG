using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public int skillPoints;
    public List<Button> buttons;
    public List<GameObject> images;
    [SerializeField] Skill skill;

    bool skill1 = true;
    bool skill2 = false;
    bool skill3 = false;
    bool skill4 = false;
    bool skill5 = false;
    bool skill6 = false;
    bool skill7 = false;
    bool skill8 = false;
    bool skill9 = false;
    bool skill10 = false;

    public bool skill1Activated = false;
    public bool skill2Activated = false;
    public bool skill3Activated = false;
    public bool skill4Activated = false;
    public bool skill5Activated = false;
    public bool skill6Activated = false;
    public bool skill7Activated = false;
    public bool skill8Activated = false;
    public bool skill9Activated = false;
    public bool skill10Activated = false;

    //public List<bool> skills;
    public List<bool> skillsActivated;

    public int leftSkills = 0;
    public int rightSkills = 0;

    public void UnlockSkills()
    {
        for (int i = 0; i < skillsActivated.Count; i++)
        {
            if (skillsActivated[i] == true)
            {
                buttons[i].interactable = false;
                images[i].SetActive(true);
            }
        }
    }

    public void Skill1()
    {
        if (skill1Activated)
        {
            skillPoints--;
            buttons[0].interactable = true;
            images[0].SetActive(true);
            skill2 = true;
        }
        else if (skillPoints >= 1 && skill1) // Check if there are sufficient skill points and if its the first time unlocking the skill
        {
            skillPoints--; // Reduce skill point
            buttons[0].interactable = true; // Set the next skill button as interactable
            images[0].SetActive(true); // Set the check image as active
            skill1 = false; // Set as false so it can't be activated again
            skill2 = true; // set the next skill as active
            skill1Activated = true;
            skill.UnlockSkill1();
        }

    }   
    public void Skill2()
    {
        if (skill2Activated)
        {
            skillPoints--;
            buttons[1].interactable = true;
            buttons[2].interactable = true;
            images[1].SetActive(true);
            skill4 = true;
            skill3 = true;
        }
        else if(skillPoints >= 1 && skill2)
        {
            skillPoints--;
            buttons[1].interactable = true;
            buttons[2].interactable = true;
            images[1].SetActive(true);
            skill2 = false;
            skill3 = true;
            skill4 = true;
            skill2Activated = true;
            skillsActivated.Add(true);
            skill.UnlockSkill2();
        }
    }   
    public void Skill3()
    {
        if (skill3Activated)
        {
            skillPoints--;
            buttons[3].interactable = true;
            buttons[4].interactable = true;
            images[2].SetActive(true);
            skill5 = true;
            skill6 = true;
        }
        else if (skillPoints >= 1 && skill3)
        {
            skillPoints--;
            buttons[3].interactable = true;
            buttons[4].interactable = true;
            images[2].SetActive(true);
            skill3 = false;
            skill5 = true;
            skill6 = true;
            skill3Activated = true;
        }
    }  
    public void Skill4()
    {
        if (skill4Activated)
        {
            skillPoints--;
            images[3].SetActive(true);
            skill7 = true;
            skill8 = true;
            buttons[5].interactable = true;
            buttons[6].interactable = true;
            skill.UnlockSkill4();
        }
        else if (skillPoints >= 1 && skill4)
        {
            skillPoints--;
            buttons[5].interactable = true;
            buttons[6].interactable = true;
            images[3].SetActive(true);
            skill4 = false;
            skill7 = true;
            skill8 = true;
            skill4Activated = true;
            skill.UnlockSkill4();
        }
    }  
    public void Skill5()
    {
        if (skill5Activated)
        {
            skillPoints--;
            images[4].SetActive(true);
            leftSkills++;
            if (leftSkills == 2) // Check if both previous skills have been unlocked
            {
                buttons[7].interactable = true;
                skill9 = true;
            }
        }
        else if (skillPoints >= 1 && skill5)
        {
            skillPoints--;
            skill5 = false;
            leftSkills++;
            images[4].SetActive(true);
            skill5Activated = true;
            if (leftSkills == 2) // Check if both previous skills have been unlocked
            {
                buttons[7].interactable = true;
                skill9 = true;
            }
        }
    }    
    public void Skill6()
    {
        if (skill6Activated)
        {
            skillPoints--;
            images[5].SetActive(true);
            leftSkills++;
            if (leftSkills == 2)
            {
                buttons[7].interactable = true;
                skill9 = true;
            }
        }
        else if (skillPoints >= 1 && skill6)
        {
            skillPoints--;
            skill6 = false;
            leftSkills++;
            images[5].SetActive(true);
            skill6Activated = true;
            if (leftSkills == 2)
            {
                buttons[7].interactable = true;
                skill9 = true;
            }
        }
    }  
    public void Skill7()
    {
        if (skill7Activated)
        {
            skillPoints--;
            images[6].SetActive(true);
            rightSkills++;
            if (rightSkills == 2)
            {
                buttons[8].interactable = true;
                skill10 = true;
            }
        }
        else if (skillPoints >= 1 && skill7)
        {
            skillPoints--;
            skill7 = false;
            rightSkills++;
            images[6].SetActive(true);
            skill7Activated = true;
            if (rightSkills == 2)
            {
                buttons[8].interactable = true;
                skill10 = true;
            }
        }
    }  
    public void Skill8()
    {
        if (skill8Activated)
        {
            skillPoints--;
            images[7].SetActive(true);
            rightSkills++;
            skill.UnlockSkill8();
            if (rightSkills == 2)
            {
                buttons[8].interactable = true;
                skill10 = true;
            }
        }else if (skillPoints >= 1 && skill8)
        {
            skillPoints--;
            skill8 = false;
            rightSkills++;
            images[7].SetActive(true);
            skill8Activated = true;
            skill.UnlockSkill8();
            if (rightSkills == 2)
            {
                buttons[8].interactable = true;
                skill10 = true;
            }
        }
    }  
    public void Skill9()
    {
        if (skill9Activated)
        {
            skillPoints--;
            images[8].SetActive(true);
        }
        else if (skillPoints >= 1 && skill9)
        {
            skillPoints--;
            images[8].SetActive(true);
            skill9 = false;
            skill9Activated = true;
        }

    } 
    public void Skill10()
    {
        if (skill10Activated)
        {
            skillPoints--;
            images[9].SetActive(true);
            skill.UnlockSkill10();
        }
        else if (skillPoints >= 1 && skill10)
        {
            skillPoints--;
            images[9].SetActive(true);
            skill10 = false;
            skill10Activated = true;
            skill.UnlockSkill10();
        }
    }

/*    public void LoadData(GameData data)
    {
        Debug.Log(skill1Activated);
        this.skill1Activated = data.skill1Activated;
    }

    public void SaveData(GameData data)
    {
        Debug.Log(skill1Activated);
        data.skill1Activated = this.skill1Activated;
    }*/
}
