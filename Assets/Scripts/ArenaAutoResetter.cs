using UnityEngine;
using System.Collections;

public class ArenaAutoResetter : MonoBehaviour {

    public float interval = 10;
    public float nextUpdate;


    void Awake()
    {
        nextUpdate = Time.time + interval;

    }

    void Update()
    {
        if (GameModel.isPlaying && nextUpdate < Time.time)
        {
            EventManager.ArenaChange();
            nextUpdate = Time.time + interval;
        }
    } 
}
