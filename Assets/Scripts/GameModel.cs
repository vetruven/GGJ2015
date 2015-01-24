using System;
using UnityEngine;
using System.Collections;

public class GameModel : MonoBehaviour {
    public enum GameState{Menu, Intro, Game, WaveEnd, GameEnd}
    public static GameState state = GameState.Intro;
    public static int currWave = 0;


    int introCount = 1;
    private int timeBetweenWaves = 1;
    private Coroutine specialCountCoro;

    void Awake()
    {
        gameObject.AddComponent<SpecialCounter>();
        state = GameState.Intro;
    }
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
            EventManager.ArenaChange();

        if (Input.GetKeyDown(KeyCode.Y))
            Application.LoadLevel(0);

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }


    public static bool isPlaying { get
    {
        return state == GameState.Game || state == GameState.WaveEnd || state == GameState.Intro;
    } }

    void Start()
    {
        StartCoroutine(CountDownIntro());
        EventManager.OnWaveFinish += FinishWave;
        EventManager.OnPlayerExplode += CheckForGameEnd;
    }

    void OnDestroy()
    {
        EventManager.OnWaveFinish -= FinishWave;
        EventManager.OnPlayerExplode -= CheckForGameEnd;
    }

    private void CheckForGameEnd()
    {
        if(Player.players.Count == 0)
            state = GameState.GameEnd;

        Debug.Log("Check for the game Player# == " + Player.players.Count);
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
        EventManager.GameStart();
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

        Debug.Log("Start Wave = "+currWave);
        EventManager.WaveStart();
        EventManager.ArenaChange();
        GetComponent<SpecialCounter>().StartSpecialCount();
    }

    void OnGUI()
    {
        if(GUI.Button(new Rect(20,20,100,50),"RESET" ))
            Application.LoadLevel(0);
    }
}
