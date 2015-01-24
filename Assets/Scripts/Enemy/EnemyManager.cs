using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;

	void Awake()
	{
	    EventManager.OnWaveStart += WaveStart;
	}

    private void WaveStart()
    {
        Tile t = Arena.GetFreeTile();
        Enemy e = ((GameObject)Instantiate(enemyPrefab, t.transform.position, Quaternion.identity)).GetComponent<Enemy>();
    }


    void OnDestroy()
    {
        EventManager.OnWaveStart -= WaveStart;
    }
}
