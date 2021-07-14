using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;


public enum InteractType{STARTER,RECEIVER,ENVIRONMENT,KEY}
public enum ItemType{LAMP, LADDER, LIGHTER, KIDS, DRAINAGE, NORMAL}

[CreateAssetMenu(fileName = "newItem",menuName = "InteractableItem")]
public class InteractableItem_SO : ScriptableObject
{
      public new string name;
      
      public InteractType interactType;

      public ItemType itemType;

      public Sprite itemIcon;
      [TextArea]public string description;

      [Space]
      [Header("STARTER")] public bool stackable;
      //public bool needHeight;
      [Space]
      //[Header("RECEIVER")] public bool changeable;
      [Space]
      [Header("ENVIRONMENT")] //public bool movable;
      //public bool height;

      public bool takeable;
      public ItemOnMap prefab;

      [Header("DialogueData")]
      public List<DialogueData_SO> dialogues;

      

      public int currentIndex = 0;

      public int backIndex;

      [Header("Preposition")] 
      public InteractableItem_SO target;
      public int targetIndex;
      public DialogueData_SO afterChange;
      /*public delegate void OnClicked();
      public event OnClicked SecenChange;*/

}

