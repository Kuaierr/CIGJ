using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SlotType
{
    BAG,
    WEAPON,
    ARMOR,
    ACTION
}

//level 2
public class SlotHolder : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public SlotType slotType;
    
    public ItemUI itemUI;

    public void UpdateItem()
    {
        switch (slotType)
        {
            case SlotType.BAG:
                //get bag data
                itemUI.Bag = InventoryManager.Instance.inventoryData;
                break;
            /*case SlotType.WEAPON:
                itemUI.Bag = InventoryManager.Instance.equipmentData;
                //Equip and Exchange
                if (itemUI.Bag.items[itemUI.Index].itemData)
                {
                    GameManager.Instance.playerStats.ChangeWeapon(itemUI.Bag.items[itemUI.Index].itemData);
                }
                else//空引用、耐久耗尽等情况
                {
                    GameManager.Instance.playerStats.UnequipWeapon();
                }
                break;
            case SlotType.ARMOR:
                itemUI.Bag = InventoryManager.Instance.equipmentData;
                break;
            case SlotType.ACTION:
                itemUI.Bag = InventoryManager.Instance.actionData;
                break;*/
        }
        
        //从这个格子的道具信息获得背包，再通过道具信息中的索引取到 数据库中对应的道具
        var item = itemUI.Bag.items[itemUI.Index];
        //然后更新格子信息
        itemUI.SetupItemUI(item.itemData,item.amount);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount % 2 == 0) 
        {
            UseItem();
        }
    }

    public void UseItem()
    {
        /*if (itemUI.GetItemData() == null)
        {
            return;
        }
        if (itemUI.Bag.items[itemUI.Index].itemData.itemType == ItemType.Useable &&
            itemUI.Bag.items[itemUI.Index].amount > 0) 
        {
            GameManager.Instance.playerStats.ApplyHealth(itemUI.GetItemData().useableData.healthPoint);

            itemUI.Bag.items[itemUI.Index].amount--;
            
            //Check Quest Data
            QuestManager.Instance.UpdateQuestProgress(itemUI.GetItemData().name,-1);
        }*/
        Debug.Log("Used Item");
        UpdateItem();
    }

    #region tooltip
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemUI.GetItemData())
        {
            InventoryManager.Instance.tooltip.SetupTooltip(itemUI.GetItemData());
            InventoryManager.Instance.tooltip.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryManager.Instance.tooltip.gameObject.SetActive(false);
    }
    
    //关闭悬浮窗口
    private void OnDisable()
    {
        if (InventoryManager.Instance.tooltip.enabled== false)
        {
            return;
        }
        InventoryManager.Instance.tooltip.gameObject.SetActive(false);
    }
    #endregion
}
