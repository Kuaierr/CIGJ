using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MainStateB : IState
{
    public void OnEnter()
    {
        GameManager.Instance.Player.speed = 0;
        //TODO 播放timeline
        GameManager.Instance.currentDirector.stopped += EnablePlayerController;
        GameManager.Instance.currentDirector.Play();
    }
    
    private void EnablePlayerController(PlayableDirector obj)
    {
        Debug.Log("MainB Enter");
        //GameManager.Instance.FadeInOut();
        GameManager.Instance.Player.speed = 20;
        GameManager.Instance.currentDirector.stopped -= EnablePlayerController;
    }

    public void OnUpdate()
    {
        //TODO 根据交互修改Game Manager中的剧情参数
        //TODO 检测时钟的交互
        if (GameManager.Instance.isDayTime)
        {
            GameManager.Instance.FadeOutIn();
            GameManager.Instance.TransitionState(StateType.MainStageA);
        }
    }

    public void OnQuit()
    {
        Debug.Log("MainB OnExit");
        GameManager.Instance.BackToMorning();
    }
}
