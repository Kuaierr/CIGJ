using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

/// <summary>
/// 这个类将挂在对应的道具上
/// *不要忘记配置Data
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class ItemOnMap : MonoBehaviour, IPointerClickHandler
{
    public InteractableItem_SO _itemData;
    private InteractType InteractType => _itemData.interactType;
    private ItemType ItemType => _itemData.itemType;

    private void Awake()
    {
        _itemData.currentIndex = 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CompareTag("Move"))
        {
            OpenDialogue();
            //问题：无法放入场景
            StartCoroutine(TakeItem());
        }


        if (GameObject.FindWithTag("Move"))
        {
            Vector3 poss = GameObject.FindWithTag("Move").transform.position;
            float sqrLengh = (this.transform.position - poss).sqrMagnitude;
            if (this.InteractType == InteractType.STARTER && sqrLengh > 1000)
            {
                OpenDialogue();
            }
            else if (this.InteractType == InteractType.STARTER && sqrLengh < 1000 && !HaveLight())
            {
                OpenDialogue();
            }
            else if (this.InteractType == InteractType.STARTER && sqrLengh < 1000 && HaveLight())
            {
                OpenDialogue();
            }
        }


        if (InteractType == InteractType.RECEIVER)
        {
            OpenDialogue();
        }

        else if (InteractType == InteractType.ENVIRONMENT && GameManager.Instance.isDayTime == true)
        {
            OpenDialogue();
        }
        else if (InteractType == InteractType.ENVIRONMENT && GameManager.Instance.isDayTime == false)
        {
            OpenDialogue();
        }


        else if (InteractType == InteractType.KEY)
        {
            OpenDialogue();
            switch (ItemType)
            {
                case ItemType.KIDS:
                    GameManager.StoryKeyPoint._Kids = true;
                    /*Debug.Log(GameManager.StoryKeyPoint._Kids);
                    Debug.Log(" * " + GameManager.Instance.FinalEnd);*/
                    break;
                case ItemType.LIGHTER:
                    GameManager.StoryKeyPoint._Lighter = true;
                    /*Debug.Log(GameManager.StoryKeyPoint._Lighter);
                    Debug.Log(" * " + GameManager.Instance.FinalEnd);*/
                    break;
                case ItemType.LAMP:
                    GameManager.StoryKeyPoint._Lamp = true;
                    /*Debug.Log(GameManager.StoryKeyPoint._Lamp);
                    Debug.Log(" * " + GameManager.Instance.FinalEnd);*/
                    break;
                case ItemType.DRAINAGE:
                    GameManager.StoryKeyPoint._Drainage = true;
                    /*Debug.Log(GameManager.StoryKeyPoint._Drainage);
                    Debug.Log(" * " + GameManager.Instance.FinalEnd);*/
                    break;
                case ItemType.LADDER:
                    GameManager.StoryKeyPoint._Ladder = true;
                    /*Debug.Log(GameManager.StoryKeyPoint._Ladder);
                    Debug.Log(" * " + GameManager.Instance.FinalEnd);*/
                    break;
            }
        }

        //TODO
        StartCoroutine(SwitchDayAndNight());
    }

    private void OpenDialogue()
    {
        if (GameManager.Instance.isTalking)
        {
            return;
        }

        GameManager.Instance.isTalking = true;

        if (IsTooFar())
        {
            DialogueUI.Instance.UpdateDialogueData(GameManager.Instance.toofarData);
            DialogueUI.Instance.UpdateMainDialogue(GameManager.Instance.toofarData.dialoguePieces[0]);
            return;
        }

        if (!ConditionCompleted())
        {
            DialogueUI.Instance.UpdateDialogueData(_itemData.dialogues[_itemData.currentIndex]);
            DialogueUI.Instance.UpdateMainDialogue(_itemData.dialogues[_itemData.currentIndex].dialoguePieces[0]);

            if (_itemData.currentIndex >= _itemData.dialogues.Count - 1)
            {
                _itemData.currentIndex = _itemData.backIndex;
            }
            else
            {
                _itemData.currentIndex++;
            }
        }
        else
        {
            DialogueUI.Instance.UpdateDialogueData(_itemData.afterChange);
            DialogueUI.Instance.UpdateMainDialogue(_itemData.afterChange.dialoguePieces[0]);
        }
    }

    #region BoolJudgement

    private bool IsTooFar()
    {
        Vector3 pos = GameManager.Instance.Player.transform.position;
        float sqrLenght = (transform.position - pos).sqrMagnitude;
        Debug.Log(sqrLenght);
        if (sqrLenght > 500) //人物与物体间的距离
        {
            return true;
        }

        return false;
    }

    private bool HaveLight()
    {
        var items = InventoryManager.Instance.inventoryData.items;
        if (items.Count == 0)
        {
            return false;
        }

        return items.Any(item => item.itemData.name == "diaodeng");
    }

    /// <summary>
    /// 如果存在目标对话，并达到目标对话的序号，就执行改变后的对话
    /// </summary>
    /// <returns></returns>
    private bool ConditionCompleted()
    {
        if (_itemData.target)
        {
            if (_itemData.target.currentIndex >= _itemData.targetIndex)
            {
                return true;
            }
        }

        return false;
    }

    #endregion

    #region IEnumerator

    IEnumerator TakeItem()
    {
        InventoryManager.Instance.inventoryData.AddItem(_itemData, 1);
        InventoryManager.Instance.inventoryUI.RefreshUI();
        while (GameManager.Instance.isTalking)
        {
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(this.gameObject);
    }

    IEnumerator SwitchDayAndNight()
    {
        while (GameManager.Instance.isTalking)
        {
            yield return 0;
        }
        if (GameManager.Instance.isDayTime == false) //夜->日
        {
            if (transform.name == "钟")
            {
                yield return new WaitForSeconds(0.5f);
                GameManager.Instance.isDayTime = true;
            }
        }
        else //日->夜
        {
            if (transform.name == "门")
            {
                yield return new WaitForSeconds(0.5f);
                GameManager.Instance.isDayTime = false;
            }
        }

        yield break;
    }

    #endregion
}