using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//level 3
public class ItemUI : MonoBehaviour
{
    public Image icon = null;
    public Text amount = null;

    public InteractableItem_SO currentItemData;
    
    public InventoryData_SO Bag { get; set; }
    public int Index { get; set; } = -1;

    public void SetupItemUI(InteractableItem_SO item, int itemAmount)
    {
        if (itemAmount==0)
        {
            Bag.items[Index].itemData = null;
            icon.gameObject.SetActive(false);

            return;
        }

        if (itemAmount<0)
        {
            item = null;
        }
        
        if (item != null)
        {
            currentItemData = item;
            
            icon.sprite = item.itemIcon;
            amount.text = itemAmount.ToString();

            icon.gameObject.SetActive(true);
        }
        else
            icon.gameObject.SetActive(false);
    }

    /// <summary>
    /// 快速取到道具Data
    /// </summary>
    /// <returns></returns>
    public InteractableItem_SO GetItemData()
    {
        return Bag.items[Index].itemData;
    }
}
