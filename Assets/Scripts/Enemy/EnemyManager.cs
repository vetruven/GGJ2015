using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;

	void Awake()
	{
	    EventManager.OnWaveStart += CreateEnemy;
	}

    private void CreateEnemy()
    {
        Tile t = Arena.GetFreeTile();
        Enemy e = ((GameObject)Instantiate(enemyPrefab, t.transform.position, Quaternion.identity)).GetComponent<Enemy>();
    }


    void OnDestroy()
    {
        EventManager.OnWaveStart -= CreateEnemy;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
            CreateEnemy();
    }
}
