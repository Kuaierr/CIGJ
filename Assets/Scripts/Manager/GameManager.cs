using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using DG.Tweening;
using UnityEngine.UI;

public enum StateType{FirstStage, MainStageA, MainStageB, EndingStage}

/// <summary>
/// 储存了player
/// 负责剧情的控制
/// </summary>
public class GameManager : Singleton<GameManager>
{
    [Serializable]
    public class StoryKeyPoint
    {
        [Header("Pet")] 
        public static bool _Lamp = false; //灯泡是否合理收纳

        [Header("Family")] 
        public static bool _Ladder = false;//梯子在不在柜子旁边

        public static bool _Lighter = false;//收起打火机

        public static bool _Kids = false;//和孩子对话

        public static bool _Drainage = false;//疏通排水系统

        public static bool _Final => _Ladder && _Lighter && _Kids && _Drainage; //满足所有条件 无人身亡
    }

    public GameObject black;


    //public int isTalk = 0;
    public bool isDayTime = false;
    public PlayerController Player;
    public GameObject mapTemp;
    public GameObject mapPrefab;
    
    public DialogueData_SO toofarData;

    public bool isTalking = false;

    [Header("Fade")] public SceneFader fader;

    [Header("Timeline")]
    public PlayableDirector currentDirector;

    [Header("Ending")] 
    private Dictionary<int, PlayableDirector> _dictionary;
    
    public  PlayableDirector firstDayDirector;

    public  PlayableDirector firstToSecondDirector;

    public  PlayableDirector mainDirector;

    public  PlayableDirector endDirector;

    [Space]
    [Header("DeadPoint")] 
    [Header("不用赋值")]
    public Transform deadPoint;

    public Transform cat;
    public Transform motherA;
    public Transform motherB;
    public Transform fatherA;
    public Transform fatherB;
    public Transform wife;

    [Header("Deadbody")]
    public GameObject dCat;

    public GameObject dMotherA;
    
    public GameObject dMotherB;

    public GameObject dFatherA;
    
    public GameObject dFatherB;

    public GameObject dWife;



    public bool Cat => StoryKeyPoint._Lamp;
    public bool GrandFather => StoryKeyPoint._Ladder;
    public bool Wife => StoryKeyPoint._Lighter;
    public bool GrandMather => StoryKeyPoint._Kids && StoryKeyPoint._Drainage;

    public bool FinalEnd => StoryKeyPoint._Final && StoryKeyPoint._Lamp;
    
    
    private IState currentState;
    
    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();

    public GameObject plane;
    

    protected override void Awake()
    {
        base.Awake();
        Player = FindObjectOfType<PlayerController>();
        
        states.Add(StateType.FirstStage,new BeginningState());
        states.Add(StateType.MainStageA,new MainStateA());
        states.Add(StateType.MainStageB,new MainStateB());
        states.Add(StateType.EndingStage,new EndingeState());
        
        
        TransitionState(StateType.FirstStage);
    }

    private void Update()
    {
        currentState.OnUpdate();
    }

    public void TransitionState(StateType type)
    {
        currentState?.OnQuit();
        //FadeOutIn();
        if(plane)
            plane.SetActive(false);
        
        currentState = states[type];
        currentState.OnEnter();
    }

    public void ResetMap()
    {
        Destroy(GameManager.Instance.mapTemp, 0);
        mapTemp = Instantiate(GameManager.Instance.mapPrefab, new Vector3(0, 0, 0),
            Quaternion.identity);
        Debug.Log("Reset");
        
        //读取尸体点
        deadPoint = mapTemp.transform.GetChild(4);
        cat = deadPoint.GetChild(0);
        motherA = deadPoint.GetChild(1);
        motherB = deadPoint.GetChild(2);
        fatherA = deadPoint.GetChild(3);
        fatherB = deadPoint.GetChild(4);
        wife = deadPoint.GetChild(5);
    }

    public void SetupDeadbody()
    {
        StartCoroutine(SetDeadBody());
    }

    /// <summary>
    /// 在渐出之后生成尸体
    /// </summary>
    /// <returns></returns>
    IEnumerator SetDeadBody()
    {
        yield return new WaitForSeconds(0.5f);
        bool isFatherDead = false;
        if (!StoryKeyPoint._Lamp)
        {
            Instantiate(dCat, cat);
        }
        if (!StoryKeyPoint._Ladder)
        {
            Instantiate(dFatherA, fatherA);
            isFatherDead = true;
        }
        if (!(StoryKeyPoint._Drainage&&StoryKeyPoint._Kids))
        {
            if(!StoryKeyPoint._Kids)
            {
                Instantiate(dMotherA, motherA);
                if(!isFatherDead)
                    Instantiate(dFatherB, fatherB);
            }
            else
            {
                Instantiate(dMotherB, motherB);
                if(!isFatherDead)
                    Instantiate(dFatherB, fatherB);
            }
        }
        if (!StoryKeyPoint._Lighter)
        {
            Instantiate(dWife, wife);
        }
    }
    
    /// <summary>
    /// 回到早上时清空背包中的物品，与进度
    /// </summary>
    public void BackToMorning()
    {
        StoryKeyPoint._Ladder = false;
        StoryKeyPoint._Drainage = false;
        StoryKeyPoint._Kids = false;
        StoryKeyPoint._Lamp = false;
        StoryKeyPoint._Lighter = false;
        
        //delete Inventory
        var inventory = InventoryManager.Instance.inventoryData.items;
        foreach (var item in inventory)
        {
            item.itemData = null;
            item.amount = 0;
        }
    }

    #region Fader

    public void FadeOutIn()
        {
            SceneFader fade = Instantiate(fader);
            fade.StartCoroutine(fade.FadeOutIn());
        }
    
        public void FadeIn()
        {
            SceneFader fade = Instantiate(fader);
            fade.StartCoroutine(fade.FadeIn(1f));
        }
    
        public void FadeOut()
        {
            SceneFader fade = Instantiate(fader);
            fade.StartCoroutine(fade.FadeOut(1f));
        }

    #endregion
    
    

    public void Ending()
    {
        StartCoroutine("EndStory");
    }
    
    //TODO 这里需要改 但还是可以用的
    IEnumerator EndStory()
    {
        plane.SetActive(true);
        black.SetActive(true);
        Text text = plane.transform.GetChild(0).GetComponent<Text>();
        //剧情
        text.text = "";
        text.DOText("我在哪？眼前是陌生的天花板。。。", 1f);
        yield return new WaitForSeconds(2f);
        text.text = "";
        text.DOText("我救下家人了吧。。。", 1f);
        yield return new WaitForSeconds(2f);
        text.text = "";
        text.DOText("还是一切都是临死前的走马灯。。。", 1f);
        yield return new WaitForSeconds(2f);
        
        
        
        SceneManager.LoadScene(0);
    }
    
}
