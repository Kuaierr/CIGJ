using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemUI))]
public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ItemUI currentItemUI;
    private SlotHolder currentHolder;
    private SlotHolder targetHolder;

    private void Awake()
    {
        currentItemUI = GetComponent<ItemUI>();
        currentHolder = GetComponentInParent<SlotHolder>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //temp origin data
        InventoryManager.Instance.currentDrag = new InventoryManager.DragData();
        //这里可以写成一个变量，但这是不用写holder.transform.parent
        InventoryManager.Instance.currentDrag.originalHolder = GetComponent<SlotHolder>();
        InventoryManager.Instance.currentDrag.originalParent = (RectTransform) transform.parent;
        transform.SetParent(InventoryManager.Instance.dragCanvas.transform,true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //follow mouse pos
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //exchange data
        
        //TODO: 与地图中的物品交互
        
        //判断是否在UI上
        if (EventSystem.current.IsPointerOverGameObject())
        {
            /*if (InventoryManager.Instance.CheckInActionUI(eventData.position) ||
                InventoryManager.Instance.CheckInInventoryUI(eventData.position) ||
                InventoryManager.Instance.CheckInEquipmentUI(eventData.position)) //如果鼠标在格子内*/
            
            //TODO: Interact with SceneItem
            if(InventoryManager.Instance.CheckInInventoryUI(eventData.position))
            {
                if (eventData.pointerEnter.gameObject.GetComponent<SlotHolder>())//这里代表目标是空格子
                {
                    targetHolder = eventData.pointerEnter.gameObject.GetComponent<SlotHolder>();
                }
                else if(eventData.pointerEnter.gameObject.GetComponentInParent<SlotHolder>()) //如果目标内有道具（图片是显示状态），pointerEnter.gameObject是Image，要从parent中获取slotholder
                {
                    targetHolder = eventData.pointerEnter.gameObject.GetComponentInParent<SlotHolder>();
                }
                else//如果格子被其他UI遮挡，就会发生空引用异常，因此做一下处理
                {
                    GoToOriginalPos();

                    return;
                }
                
                //Debug.Log(eventData.pointerEnter.gameObject);
                
                //根据目标格子的类型执行不同的交换逻辑
                switch (targetHolder.slotType)
                {
                    case SlotType.BAG:
                        SwapItem();
                        break;
                    /*case SlotType.WEAPON:
                        if (currentItemUI.Bag.items[currentItemUI.Index].itemData.itemType == ItemType.Weapon)
                        {
                            SwapItem();
                        }
                        break;
                    case SlotType.ARMOR:
                        if (currentItemUI.Bag.items[currentItemUI.Index].itemData.itemType == ItemType.Armor)
                        {
                            //SwapItem();
                        }
                        break;
                    case SlotType.ACTION:
                        if (currentItemUI.Bag.items[currentItemUI.Index].itemData.itemType == ItemType.Useable)
                        {
                            SwapItem();
                        }
                        break;*/
                    default:
                        break;
                }
                currentHolder.UpdateItem();
                targetHolder.UpdateItem();
            }
        }
        else
        {
            //不在UI上的情况
            //TODO 在这个case下摆放到场景中
            if (currentHolder.itemUI.currentItemData.takeable)
            {
                var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(currentHolder.itemUI.currentItemData.prefab,new Vector3(pos.x,pos.y,1),Quaternion.identity);
                            
                GoToOriginalPos();
                InventoryManager.Instance.inventoryData.items[currentHolder.itemUI.Index].itemData = null;
                InventoryManager.Instance.inventoryUI.RefreshUI();
                return;
            }
        }

        GoToOriginalPos();
    }

    private void SwapItem()
    {
        var targetItem = targetHolder.itemUI.Bag.items[targetHolder.itemUI.Index];
        var tempItem = currentHolder.itemUI.Bag.items[currentHolder.itemUI.Index];

        //可叠加物体在这个情况下会销毁自己
        if (targetHolder == currentHolder)
        {
            return;
        }

        if (targetItem.itemData == tempItem.itemData && targetItem.itemData.stackable)
        {
            targetItem.amount += tempItem.amount;
            tempItem.itemData = null;
            tempItem.amount = 0;
        }
        else
        {
            currentHolder.itemUI.Bag.items[currentHolder.itemUI.Index] = targetItem;
            targetHolder.itemUI.Bag.items[targetHolder.itemUI.Index] = tempItem;
        }
    }

    private void GoToOriginalPos()
    {
        transform.SetParent(InventoryManager.Instance.currentDrag.originalParent);
        
        RectTransform t = transform as RectTransform;
        t.offsetMax = -Vector2.one * 15;
        t.offsetMin = Vector2.one * 15;
    }
}
