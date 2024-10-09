using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using Unity.VisualScripting;

public class NPC : MonoBehaviour
{
   // public Image NPCImage;
    public Sprite NPCSprite;

   // public TextMeshProUGUI NPCNameSet;
    public string NPCName;
    public Color32 TextColor;
    public string[] lines;
    public string[] linesWhenQuestActive;
    public int typeOfDialogue;

    public GameObject DialogueBox;
    public GameObject questCompletedUI;
    public GameObject interactWindow;
    public bool questCompleted = false;
    bool isColliding = false;
    public bool isActive = false;
    public List<GameObject> objectsToDisable;


    // Start is called before the first frame update
    void Start()
    {
        /*NPCImage.sprite = NPCSprite;
        NPCNameSet.text = NPCName;
        NPCNameSet.color = TextColor;*/
    }

    // Update is called once per frame
    void Update()
    {
        // Begin dialogue with button pressed if within range
        if (isColliding)
        {
            if (Input.GetButtonDown("Use"))
            {
                interactWindow.SetActive(false);
                if (questCompleted && isActive)
                {
                    questCompletedUI.SetActive(true);
                }
                else if (isActive)
                {
                    DialogueBox.SetActive(true);
                    FindObjectOfType<Dialogue>().StartDialogue(linesWhenQuestActive, NPCName, NPCSprite, 4);
                }
                else
                {
                    DialogueBox.SetActive(true);
                    FindObjectOfType<Dialogue>().StartDialogue(lines, NPCName, NPCSprite, typeOfDialogue);
                }
                Time.timeScale = 0f;
            }
            if (Input.GetButtonDown("Escape")) // Close dialogue
            {
                //FindObjectOfType<Dialogue>().ClearText();
                DialogueBox.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && this.enabled)
        {
            isColliding = true;
            interactWindow.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.enabled && collision.tag == "Player")
        {
            isColliding = false;
            if (interactWindow != null)
                interactWindow.SetActive(false);
        }
    }

    public void AcceptQuest()
    {
        FindObjectOfType<Quest1>().AcceptQuest();
        FindObjectOfType<Dialogue>().ClearText();
    }

    public void AcceptQuest2()
    {
        FindObjectOfType<Quest2>().AcceptQuest();
        FindObjectOfType<Dialogue>().ClearText();
    }

    public void AcceptRewards()
    {
        FindObjectOfType<Quest1>().GiveRewards();
        gameObject.GetComponent<NPC>().enabled = false;
        //questCompleted = true;
        foreach (GameObject item in objectsToDisable)
        {
            item.SetActive(false);
        }
    }

    public void AcceptRewards2()
    {
        FindObjectOfType<Quest2>().GiveRewards();
        //questCompleted = true;
        foreach (GameObject item in objectsToDisable)
        {
            item.SetActive(false);
        }
        gameObject.GetComponent<NPC>().enabled = false;
    }

    public void DisableQuestNPC()
    {
        foreach (GameObject item in objectsToDisable)
        {
            item.SetActive(false);
        }
        gameObject.GetComponent<NPC>().enabled = false;
    }

}
