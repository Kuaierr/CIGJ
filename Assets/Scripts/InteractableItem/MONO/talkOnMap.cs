using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class talkOnMap : MonoBehaviour, IPointerClickHandler
{
    public InteractableItem_SO _itemData;
    public InteractType InteractType => _itemData.interactType;
    public ItemType ItemType => _itemData.itemType;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (InteractType == InteractType.RECEIVER)
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
                Debug.Log("阿巴阿巴");
    
                //对话
            }
        }

    }
}
