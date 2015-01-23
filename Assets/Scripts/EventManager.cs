using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action<int> OnIntroCount;
    public static Action<int> OnSpecialCount;
    public static Action OnSpecialCreate;
    public static Action OnSpecialKill;
    public static Action OnMapGenerationStart;
    public static Action OnMapGenerationFinish;

    public static void IntroCount(int pCount) { RaiseAction(OnIntroCount, pCount); }
    public static void SpecialCount(int pCount) { RaiseAction(OnSpecialCount, pCount); }
    public static void SpecialCreate() { RaiseAction(OnSpecialCreate); }
    public static void SpecialKill() { RaiseAction(OnSpecialKill); }
    public static void MapGenerationStart() { RaiseAction(OnMapGenerationStart);}
    public static void MapGenerationFinish() { RaiseAction(OnMapGenerationFinish); }



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
