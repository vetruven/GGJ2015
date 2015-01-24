using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    public int minEnemiesPerWave = 4;
    public int maxEnemiesPerWave = 8;
    public float waveintervals = 10;

    private float nextWaveTime = 10000000;

	void Awake()
	{
	    EventManager.OnGameStart += StartWave;
	}


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
            CreateEnemy();

        if(GameModel.isPlaying && nextWaveTime < Time.time)
            StartWave();
    }

    private void StartWave()
    {
        int enemeniestoCreate = Random.Range(minEnemiesPerWave, maxEnemiesPerWave+1);
        for (int i = 0; i < enemeniestoCreate; i++)
            CreateEnemy();

        nextWaveTime = Time.time + waveintervals;
        EventManager.WaveStart();
    }


    private void CreateEnemy()
    {
        Tile t = Arena.GetFreeTile();

        if(t != null)
            ((GameObject)Instantiate(enemyPrefab, t.transform.position, Quaternion.identity)).GetComponent<Enemy>();
    }


    void OnDestroy()
    {
        EventManager.OnWaveStart -= CreateEnemy;
    }


}
