using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    public Text itemName;
    public Text itemInfo;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        UpdatePosition();
    }

    public void SetupTooltip(InteractableItem_SO item)
    {
        itemName.text = item.name;
        itemInfo.text = item.description;
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3 mousePos = Input.mousePosition;

        //get width and height
        Vector3[] corners = new Vector3[4];
        _rectTransform.GetWorldCorners(corners);

        float width = corners[3].x - corners[0].x;
        float height = corners[1].y - corners[0].y;

        if (mousePos.y<height)
        {
            _rectTransform.position = mousePos + Vector3.up * (height * 0.6f);
        }else if (Screen.width - mousePos.x > width)
        {
            _rectTransform.position = mousePos + Vector3.right * (width * 0.6f);
        }
        else
        {
            _rectTransform.position = mousePos + Vector3.left * (width * 0.6f);
        }
    }
    
}
