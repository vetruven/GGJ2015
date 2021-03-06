﻿using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action<string> OnOSFMsg;
    public static Action<int> OnSpecialCount;
    public static Action OnSpecialCreate;
    public static Action OnSpecialKill;
    public static Action OnGameStart;
    public static Action OnWaveStart;
    public static Action OnWaveFinish;
    public static Action OnArenaChange;
    public static Action<Vector3,float> OnEnemyExplode;
    public static Action OnPlayerExplode;


    public static void OSFMsg(String pMsg) { RaiseAction(OnOSFMsg, pMsg); }
    public static void SpecialCount(int pCount) { RaiseAction(OnSpecialCount, pCount); }
    public static void SpecialCreate() { RaiseAction(OnSpecialCreate); }
    public static void SpecialKill() { RaiseAction(OnSpecialKill); }
    public static void GameStart() { RaiseAction(OnGameStart); }
    public static void WaveStart() { RaiseAction(OnWaveStart);}
    public static void WaveFinish() { RaiseAction(OnWaveFinish); }
    public static void ArenaChange() { RaiseAction(OnArenaChange); }
    public static void EnemyExplode(Vector3 pPos, float pDamage) { RaiseAction(OnEnemyExplode, pPos, pDamage); }
    public static void PlayerExplode() { RaiseAction(OnPlayerExplode); }

    public static void RaiseAction( Action a )
    {
        if ( a != null )
        {
            a.Invoke();
        }
    }

    public static void RaiseAction<T>( Action<T> a, T GenParam )
    {
        if ( a != null )
        {
            a( GenParam );
        }
    }

    public static void RaiseAction<T, TK>( Action<T, TK> a, T GenParam, TK GenParam2 )
    {
        if ( a != null )
        {
            a( GenParam, GenParam2 );
        }
    }



}
