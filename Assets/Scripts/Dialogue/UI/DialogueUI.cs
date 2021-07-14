using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : Singleton<DialogueUI>
{
    [Header("Basic Element")]
    public Image icon;

    public Text mainText;

    public Button nextButton;

    public GameObject dialoguePanel;

    public RectTransform optionPanel;
    public OptionUI optionPrefab;

    [Header("Data")] 
    public DialogueData_SO currentData;

    private int currentIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        nextButton.onClick.AddListener(ContinueDialogue);
    }

    private void ContinueDialogue()
    {
        if (currentIndex < currentData.dialoguePieces.Count)
        {
            UpdateMainDialogue(currentData.dialoguePieces[currentIndex]);
        }
        else
        {
            dialoguePanel.SetActive(false);
            GameManager.Instance.isTalking = false;
        }
        
    }


    /// <summary>
    /// 即是传入内容的功能
    /// </summary>
    /// <param name="data"></param>
    public void UpdateDialogueData(DialogueData_SO data)
    {
        currentData = data;
        currentIndex = 0;
    }
    
    /// <summary>
    /// update image,text and button
    /// 即 翻页的功能
    /// </summary>
    public void UpdateMainDialogue(DialoguePiece piece)
    {
        dialoguePanel.SetActive(true);
        
        currentIndex++;

        if (piece.image != null)
        {
            icon.enabled = true;
            icon.sprite = piece.image;
        }
        else
        {
            icon.enabled = false;
        }

        mainText.text = "";

        //mainText.text = piece.text;
        mainText.DOText(piece.text, 1f);
        
        var color = nextButton.GetComponent<Image>().color;
        if (piece.options.Count ==0 && currentData.dialoguePieces.Count>0)
        {
            nextButton.interactable = true;
            color.a = 1f;
            //nextButton.gameObject.SetActive(true);
        }
        else
        {
            //nextButton.gameObject.SetActive(false);
            nextButton.interactable = false;
            color.a = 0f;
        }
        
        //creat options
        CreatOptions(piece);
    }

    private void CreatOptions(DialoguePiece piece)
    {
        if (optionPanel.childCount > 0)
        {
            for (int i = 0; i < optionPanel.childCount; i++)
            {
                Destroy(optionPanel.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < piece.options.Count; i++)
        {
            var option = Instantiate(optionPrefab, optionPanel);
            option.UpdateOption(piece,piece.options[i]);
        }
    }
}
