using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    //public GameObject dialoguePanel;
    public GameObject shopUI;
    public GameObject blacksmithUI;
    public GameObject QuestUI;
    public GameObject QuestUIGather;
    public GameObject questCompletedUI;
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI buttonText;
    public Image NPCImage;
    public int typeOfDialogue;
    //public Quest quest;

    public string[] lines;
    public float textSpeed;
    //public NPC npc;
    private int index;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // When pressing left click load all the text and finish all dialog when text is finished
        if(Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
                NextLine();
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue(string[] NPClines, string name, Sprite image, int type)
    {
        textComponent.text = string.Empty;
        index = 0;
        lines = NPClines;
        nameText.text = name;
        NPCImage.sprite = image;
        NPCImage.SetNativeSize();
        typeOfDialogue = type;
        switch (type)
        {
            case 0:
                buttonText.text = "Open Shop";
                break;
            case 1:
                buttonText.text = "Open Shop";
                break;
            case 2:
                buttonText.text = "Continue";
                break;                
            case 3:
                buttonText.text = "Continue";
                break;            
            case 4:
                buttonText.text = "Close";
                break;
            default:
                break;
        }
        StartCoroutine(TypeLine());
        
    }

    IEnumerator TypeLine()
    {
        // Type each character 1 by 1
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            //ClearText();
        }
    }

    public void ClearText()
    {
        textComponent.text = string.Empty;
        index = 0;
        //gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        //Time.timeScale = 1f;
    }

    public void OpenWindow()
    {
        switch (typeOfDialogue)
        {
            case 0:
                shopUI.SetActive(true);
                break;
            case 1:
                blacksmithUI.SetActive(true);
                break;
            case 2:
                QuestUI.SetActive(true);
                break;
            case 3:
                QuestUIGather.SetActive(true);
                break;            
            case 4:
                gameObject.SetActive(false);
                Time.timeScale = 1f;
                break;
            default:
                break;
        }
        gameObject.SetActive(false);
    }
}
