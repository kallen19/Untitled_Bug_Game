using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using System;
using Unity.VisualScripting;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UIElements;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] List<string> lines = new List<string>();
    [SerializeField] float charactersPerSecond;
    private int currentLineIndex;
    private float timeBetweenCharacters;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentLineIndex = -1;
        timeBetweenCharacters = 1 / charactersPerSecond;
        textComponent.text = "";
        SetDialogueBoxVisibility(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(currentLineIndex == -1)
            {
                SetDialogueBoxVisibility(true);
                currentLineIndex++;
                DoDialogue();
            } 
            else if(textComponent.text == lines[currentLineIndex])
            {
                currentLineIndex ++;
                if(currentLineIndex == lines.Count)
                {
                    SetDialogueBoxVisibility(false);
                    return;
                }
                DoDialogue();
            } 
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[currentLineIndex];
            }
            
        }
    }

    void SetDialogueBoxVisibility(bool visibility)
    {
        
        dialogueBox.SetActive(visibility);
        
    }

    public void DoDialogue()
    {
        StartCoroutine(TypeLine());      

    }

    IEnumerator TypeLine()
    {
        textComponent.text = "";
        foreach(char c in lines[currentLineIndex].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(timeBetweenCharacters);
        }
    }
}
