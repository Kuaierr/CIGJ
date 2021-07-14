using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BeginningState : IState
{
    public void OnEnter()
    {
        GameManager.Instance.FadeIn();
        GameManager.Instance.Player.speed = 0;
        //TODO 播放timeline
        GameManager.Instance.currentDirector.stopped += EnablePlayerController;
        GameManager.Instance.currentDirector.Play();
       
    }

    private void EnablePlayerController(PlayableDirector obj)
    {
        GameManager.Instance.Player.speed = 20;
        //TODO Reset Scene
        GameManager.Instance.currentDirector.stopped -= EnablePlayerController;
        Debug.Log("Stage1 OnEnter");
    }

    public void OnUpdate()
    {
        //TODO: 点击钟时切切换到下一个状态
        if (GameManager.Instance.isDayTime)
        {
            GameManager.Instance.FadeOutIn();
            GameManager.Instance.TransitionState(StateType.MainStageA);
        }
        
    }

    public void OnQuit()
    {
        Debug.Log("Stage1 OnExit");
        //TODO Reset Scene
        GameManager.Instance.ResetMap();
    }
    
}
