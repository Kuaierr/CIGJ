using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainmenu : MonoBehaviour
{
    /*  public GameObject main;
   public GameObject settingmenu;



 void Start()
   {
       main.SetActive(true);
       settingmenu.SetActive(false);
   }*/
    public void playgame() {

        SceneManager.LoadScene(1);

    }
    /*public void setting()
    {
        main.SetActive(false);
        settingmenu.SetActive(true);
    }
    public void backtomain()
    {
        main.SetActive(true);
        settingmenu.SetActive(false);
    }*/

 public void quitgame()
    {
        Application.Quit();
    }

}
