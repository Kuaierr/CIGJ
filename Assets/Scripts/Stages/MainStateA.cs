using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MainStateA : IState
{
    public void OnEnter()
    {
        GameManager.Instance.ResetMap();
        GameManager.Instance.Player.speed = 0;
        //TODO 播放timeline
        GameManager.Instance.currentDirector.stopped += EnablePlayerController;
        GameManager.Instance.currentDirector.Play();
    }
    
    private void EnablePlayerController(PlayableDirector obj)
    {
        Debug.Log("MainA Enter");
        GameManager.Instance.Player.speed = 20;
        //Reset Scene
        GameManager.Instance.ResetMap();
        GameManager.Instance.currentDirector.stopped -= EnablePlayerController;
    }

    public void OnUpdate()
    {
        if (GameManager.Instance.FinalEnd && (!GameManager.Instance.isDayTime))
        {
            GameManager.Instance.FadeOutIn();
            GameManager.Instance.TransitionState(StateType.EndingStage);
        }
        else if(!GameManager.Instance.isDayTime)
        {
            GameManager.Instance.FadeOutIn();
            GameManager.Instance.TransitionState(StateType.MainStageB);
        }
        //GameManager.Instance.TransitionState(StateType.MainStageB);
    }

    public void OnQuit()
    {
        Debug.Log("MainA OnExit");
        //生成尸体
        GameManager.Instance.SetupDeadbody();
    }
}
