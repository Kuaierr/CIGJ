using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : Singleton<InventoryManager>
{
    public class DragData
    {
        public SlotHolder originalHolder;
        public RectTransform originalParent;
    }
    
    //添加模板以存储数据
    [Header("Inventory Data")] 
    public InventoryData_SO inventoryTemplate;
    public InventoryData_SO inventoryData;
    
    /*public InventoryData_SO actionTemplate;
    public InventoryData_SO actionData;
    
    public InventoryData_SO equipmentTemplate;
    public InventoryData_SO equipmentData;*/

    [Header("Containers")] 
    public ContainerUI inventoryUI;
    /*public ContainerUI actionUI;
    public ContainerUI equipmentUI;*/

    [Header("DragCanvas")]
    public Canvas dragCanvas;
    public DragData currentDrag;

    //UI是否开启
    private bool isOpen = false;

    [Header("UIPlane")] 
    public GameObject bagPanel;
    //public GameObject statsPanel;

    /*[Header("Stats Text")]
    public Text healthText;
    public Text attackText;
    */

    [Header("Tooltip")] public ItemTooltip tooltip;

    protected override void Awake()
    {
        base.Awake();
        if (inventoryTemplate)
        {
            inventoryData = Instantiate(inventoryTemplate);
        }
        /*if (actionTemplate)
        {
            actionData = Instantiate(actionTemplate);
        }
        if (equipmentTemplate)
        {
            equipmentData = Instantiate(equipmentTemplate);
        }*/
    }

    private void Start()
    {
        //LoadData();
        
        inventoryUI.RefreshUI();
        /*actionUI.RefreshUI();
        equipmentUI.RefreshUI();*/
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            isOpen = !isOpen;
            bagPanel.SetActive(isOpen);
            //statsPanel.SetActive(isOpen);
        }

        /*UpdateStatsText(GameManager.Instance.playerStats.MaxHealth,
            GameManager.Instance.playerStats.attackData.minDamage,
            GameManager.Instance.playerStats.attackData.maxDamage);*/
    }

    /*#region load inventoryData

    public void SaveData()
    {
        SaveManager.Instance.Save(inventoryData,inventoryData.name);
        SaveManager.Instance.Save(actionTemplate,actionTemplate.name);
        SaveManager.Instance.Save(equipmentTemplate,equipmentTemplate.name);
    }

    public void LoadData()
    {
        SaveManager.Instance.Load(inventoryData,inventoryData.name);
        SaveManager.Instance.Load(actionTemplate,actionTemplate.name);
        SaveManager.Instance.Load(equipmentTemplate,equipmentTemplate.name);
    }

    #endregion*/

    /// <summary>
    /// 更新人物面板中的数据
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    /*public void UpdateStatsText(int health, int min, int max)
    {
        healthText.text = health.ToString();
        attackText.text = min + " - " + max;
    }*/

    #region 检查拖拽物品是否在每一个slot内

    public bool CheckInInventoryUI(Vector3 position)
    {
        for (int i = 0; i < inventoryUI.slotHolders.Length; i++)
        {
            RectTransform t = inventoryUI.slotHolders[i].transform as RectTransform;
            if (RectTransformUtility.RectangleContainsScreenPoint(t,position))
            {
                return true;
            }
        }

        return false;
    }
    
    /*public bool CheckInActionUI(Vector3 position)
    {
        for (int i = 0; i < actionUI.slotHolders.Length; i++)
        {
            RectTransform t = actionUI.slotHolders[i].transform as RectTransform;
            if (RectTransformUtility.RectangleContainsScreenPoint(t,position))
            {
                return true;
            }
        }

        return false;
    }*/
    
    /*public bool CheckInEquipmentUI(Vector3 position)
    {
        for (int i = 0; i < equipmentUI.slotHolders.Length; i++)
        {
            RectTransform t = equipmentUI.slotHolders[i].transform as RectTransform;
            if (RectTransformUtility.RectangleContainsScreenPoint(t,position))
            {
                return true;
            }
        }

        return false;
    }*/

    #endregion
    
    /*#region check questitem

    public void CheckQuestItemInBag(string questItemName)
    {
        //check Inventory
        foreach (var item in inventoryData.items)
        {
            if (item.itemData!=null)
            {
                if (item.itemData.itemName == questItemName)
                {
                    QuestManager.Instance.UpdateQuestProgress(item.itemData.itemName,item.amount);
                }
            }
        }
        
        //check action
        foreach (var item in actionData.items)
        {
            if (item.itemData!=null)
            {
                if (item.itemData.itemName == questItemName)
                {
                    QuestManager.Instance.UpdateQuestProgress(item.itemData.itemName,item.amount);
                }
            }
        }
    }

    #endregion*/

    /*public InventoryItem QuestItemInBag(ItemData_SO questItemDataSo)
    {
        return inventoryData.items.Find(i => i.itemData == questItemDataSo);
    }
    
    public InventoryItem QuestItemInAction(ItemData_SO questItemDataSo)
    {
        return actionData.items.Find(i => i.itemData == questItemDataSo);
    }*/
}
