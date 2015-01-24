using System;
using UnityEngine;
using System.Collections;

public class GameModel : MonoBehaviour {
    public enum GameState{Menu, Intro, Game, WaveEnd, GameEnd}
    public static GameState state = GameState.Intro;
    public static int currWave = 0;


    int introCount = 0;
    private int timeBetweenWaves = 1;
    private Coroutine specialCountCoro;

    void Awake()
    {
        gameObject.AddComponent<SpecialCounter>();
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
            EventManager.WaveFinish();
    }


    public static bool isPlaying { get { return state == GameState.Game || state == GameState.WaveEnd; } }

    void Start()
    {
        StartCoroutine(CountDownIntro());
        EventManager.OnWaveFinish += FinishWave;
    }

    private IEnumerator CountDownIntro()
    {
        Debug.Log("Start Intro count");

        int lastCount = introCount;

        while (lastCount >= 0)
        {
            EventManager.OSFMsg(lastCount.ToString());
            Debug.Log("Intro - Count " + lastCount);
            lastCount--;
            yield return new WaitForSeconds(1);
        }

        currWave = 1;

        Debug.Log("Map Generation Start");
        EventManager.WaveStart();
        GetComponent<SpecialCounter>().StartSpecialCount();
        state = GameState.Game;
    }

    private void FinishWave()
    {
        GetComponent<SpecialCounter>().StopSpecialCount();
        StartCoroutine(StartCountToNextWave());
    }

    private IEnumerator StartCountToNextWave()
    {
        Debug.Log("Start Count To Next Wave");
        int lastCount = timeBetweenWaves;

        while (lastCount >= 0)
        {
            EventManager.OSFMsg(lastCount.ToString());
            Debug.Log("Start Wave in " + lastCount);
            lastCount--;
            yield return new WaitForSeconds(1);
        }

        currWave++;
        Debug.Log("Start Wave = "+currWave);
        EventManager.WaveStart();
        GetComponent<SpecialCounter>().StartSpecialCount();
    }
}
