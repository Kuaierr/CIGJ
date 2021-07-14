using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public Text optionText;
    private Button thisButton;
    private DialoguePiece currentPiece;

    private string nextPieceID;

    private bool takeQuest;

    private void Awake()
    {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener(OnOptionClicked);
    }

    public void UpdateOption(DialoguePiece piece, DialogueOption option)
    {
        currentPiece = piece;
        optionText.text = option.text;

        takeQuest = option.takeQuest;
        nextPieceID = option.targetID;
    }

    private void OnOptionClicked()
    {
        /*if (currentPiece.quest != null)
        {
            //change QuestData_SO into QuestTask
            var newTask = new QuestManager.QuestTask
            {
                questData = Instantiate(currentPiece.quest)
            };
            
            if (takeQuest)
            {
                //Add into list
                //is it excited
                if (QuestManager.Instance.HaveQuest(newTask.questData))
                {
                    //is it complete?
                    if (QuestManager.Instance.GetTask(newTask.questData).IsComplete &&
                        !QuestManager.Instance.GetTask(newTask.questData).IsFinished) 
                    {
                        newTask.questData.GiveRewards();
                        QuestManager.Instance.GetTask(newTask.questData).IsFinished = true;
                    }
                }
                else // take quest
                {
                    QuestManager.Instance.tasks.Add(newTask);
                    //如果直接修改newtask，被修改的将是临时变量而不是任务列表中的
                    QuestManager.Instance.GetTask(newTask.questData).IsStarted = true;

                    foreach (var requireItem in newTask.questData.RequireTargetName())
                    {
                        InventoryManager.Instance.CheckQuestItemInBag(requireItem);
                    }
                }
            }
        }*/
        
        if (nextPieceID=="")
        {
            DialogueUI.Instance.dialoguePanel.SetActive(false);
            return;
        }
        else
        {
            DialogueUI.Instance.UpdateMainDialogue(DialogueUI.Instance.currentData.dialogueIndex[nextPieceID]);
        }
    }

    
}
