using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public GameObject contButton;
    public string[] dialogue;
    private int index;
    public float typingSpeed;
    public bool playerInRange;
   

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)&& playerInRange)
        {
            if(dialoguePanel.activeInHierarchy)
            {
                zeroText();
                
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Type());
                
            }
            
        }

        if (dialogueText.text == dialogue[index])
        {
            contButton.SetActive(true);
        }
        
        
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false); 
    }

    IEnumerator Type()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false);
        if(index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Type());
        }
        else
        {
            dialogueText.text = "";
            index = 0;
            dialoguePanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            zeroText();
        }
    }
}
