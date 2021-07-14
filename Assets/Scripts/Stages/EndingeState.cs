using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class EndingeState : IState
{
    public void OnEnter()
    {
        Debug.Log("Clear");
        GameManager.Instance.Player.speed = 0;
        //TODO 播放timeline
        GameManager.Instance.currentDirector.stopped += Ending;
        GameManager.Instance.currentDirector.Play();
    }
    
    private void Ending(PlayableDirector obj)
    {
        GameManager.Instance.FadeIn();
        //TODO : Back To Main Menu
        GameManager.Instance.plane.SetActive(true);
        GameManager.Instance.Ending();
        //SceneManager.LoadScene(0);
    }

    public void OnUpdate()
    {
        
    }

    public void OnQuit()
    {
        throw new System.NotImplementedException();
    }

    /*IEnumerator EndStory()
    {
        
        SceneManager.LoadScene(0);
    }*/
}
