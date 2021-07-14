using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
public class dialogsystem :Singleton<dialogsystem>
{
    public Text textlabel;
    [CanBeNull]public TextAsset file;
    public int Index;
    List<string> textlist = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        gettextfromfile(file);
        Index = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && Index == textlist.Count)
        {
            gameObject.SetActive(false);
            Index = 0;
            return;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            textlabel.text = textlist[Index];
            Index++;
        }
    }

    void gettextfromfile(TextAsset file)
    {
        textlist.Clear();
        Index = 1;
        var linedata = file.text.Split('\n');
        foreach (var line in linedata)
        {
            textlist.Add(line);
        }
    }
}
