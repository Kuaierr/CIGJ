using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talkbutton : MonoBehaviour
{

    private bool close;
    public GameObject talkUI;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        close = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        close=false;
    }
    // Update is called once per frame
    void Update()
    {

        if (close && Input.GetKeyDown(KeyCode.T)) {


            talkUI.SetActive(true);

        }

    }

}
