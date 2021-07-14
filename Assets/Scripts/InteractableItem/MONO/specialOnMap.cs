using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class specialOnMap : MonoBehaviour, IPointerClickHandler
{
    public InteractableItem_SO _itemData;
    public InteractType InteractType => _itemData.interactType;
    public ItemType ItemType => _itemData.itemType;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (InteractType == InteractType.KEY)
        {
            Vector3 pos = GameManager.Instance.Player.transform.position;
            float sqrLenght = (this.transform.position - pos).sqrMagnitude;
            if (sqrLenght > 400) //人物与物体间的距离
            {
                Debug.Log("太远了够不到");
                //会话框
            }
            else
            {
                switch (ItemType)
                {
                    case ItemType.KIDS:
                        GameManager.StoryKeyPoint._Kids = !GameManager.StoryKeyPoint._Kids;
                        break;
                    case ItemType.LIGHTER:
                        GameManager.StoryKeyPoint._Lighter = !GameManager.StoryKeyPoint._Lighter;
                        break;
                    case ItemType.LAMP:
                        GameManager.StoryKeyPoint._Lamp = !GameManager.StoryKeyPoint._Lamp;
                        break;
                    case ItemType.DRAINAGE:
                        GameManager.StoryKeyPoint._Drainage = !GameManager.StoryKeyPoint._Drainage;
                        break;
                    case ItemType.LADDER:
                        GameManager.StoryKeyPoint._Ladder = !GameManager.StoryKeyPoint._Ladder;
                        break;
                }
                Debug.Log("阿巴阿巴");
                //对话
            }
        }

    }
}