using System;
using UnityEngine;
using System.Collections;

public class GameModel : MonoBehaviour {
    public enum GameState{Menu, Intro, Game, WaveEnd, GameEnd}
    public static GameState state = GameState.Intro;
    public int currWave = 0;


    int introCount = 0;
    int toSpecialCount = 15;
    int specialDurationCount = 15;
    private Coroutine specialCountCoro;




    public static bool isPlaying { get { return state == GameState.Game || state == GameState.WaveEnd; } }

    void Start()
    {
        StartCoroutine(CountDownIntro());
        EventManager.OnMapGenerationFinish += HandleMapGenerationFinish;
    }

    private IEnumerator CountDownIntro()
    {
        Debug.Log("Start Intro count");

        int lastCount = introCount;

        while (lastCount >= 0)
        {
            EventManager.IntroCount(lastCount);
            lastCount--;
            Debug.Log("Intro - Count " + lastCount);
            yield return new WaitForSeconds(1);
        }

        currWave = 1;

        Debug.Log("Map Generation Start");
        EventManager.MapGenerationStart();
    }

    private void HandleMapGenerationFinish()
    {
        Debug.Log("Map Generation Finish");
        specialCountCoro = StartCoroutine(StartCountSpecialCreate());
        state = GameState.Game;
    }

    private IEnumerator StartCountSpecialCreate()
    {
        Debug.Log("Start Count to special");
        int lastCount = toSpecialCount;

        while (lastCount >= 0)
        {
            EventManager.IntroCount(lastCount);
            lastCount--;
            Debug.Log("Special in "+lastCount);
            yield return new WaitForSeconds(1);
        }

        Debug.Log("CreateSpecial");
        EventManager.SpecialCreate();
        specialCountCoro = StartCoroutine(StartCountSpecialKill());
    }

    private IEnumerator StartCountSpecialKill()
    {
        Debug.Log("Start Special Life");
        int lastCount = specialDurationCount;

        while (lastCount >= 0)
        {
            EventManager.IntroCount(lastCount);
            lastCount--;
            Debug.Log("Start Life "+lastCount);
            yield return new WaitForSeconds(1);
        }

        Debug.Log("Special Kill");
        EventManager.SpecialKill();
        specialCountCoro = StartCoroutine(StartCountSpecialCreate());
    }
}
