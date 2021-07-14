using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class DialogueController : MonoBehaviour,IPointerClickHandler
{
    public DialogueData_SO currentData;

    //private bool canTalk = false;

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TriggerEnter");
        if (other.CompareTag("Player") && currentData)
        {
            canTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueUI.Instance.dialoguePanel.SetActive(false);
            canTalk = false;
        }
    }*/

    /*private void Update()
    {
        //Debug.Log(canTalk);
        if (canTalk && (Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.R)))
        {
            OpenDialogue();
        }
    }*/

    /// <summary>
    /// 打开UI, input text
    /// </summary>
    private void OpenDialogue()
    {
        DialogueUI.Instance.UpdateDialogueData(currentData);
        DialogueUI.Instance.UpdateMainDialogue(currentData.dialoguePieces[0]);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        OpenDialogue();
    }
}