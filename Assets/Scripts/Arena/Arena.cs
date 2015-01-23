using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    public GameObject TilePrefab;
    public int width = 20;
    public int height = 20;
    public float tileSize = 20;

    public int minWallCount = 20;
    public int maxWallCount = 30;

    public float crackWidth = 1;

    public List<Tile> tiles; 


    void Awake()
    {
        for (int z = 0; z < height; z++)
            for (int x = 0; x < width; x++)
                CreateTile(x, z);

        EventManager.OnMapGenerationStart += StartMapGeneration;
    }

    private void CreateTile(int pX, int pZ)
    {
        Tile t = ((GameObject)Instantiate(TilePrefab)).GetComponent<Tile>();
        t.transform.position = transform.position + new Vector3(pX * (crackWidth+tileSize),-0.5f,pZ * (crackWidth+tileSize) );
        t.origPos = t.transform.position;
        t.coord = new Vector2(pX, pZ);
        t.transform.parent = transform;
        tiles.Add(t);
    }


    private void StartMapGeneration()
    {
        for (int i = 0; i < Random.Range(minWallCount, maxWallCount); i++)
        {
            int idx = Random.Range(0, tiles.Count);

            int ppz = 0;
            while (tiles[idx].isTaken && ppz < 100)
            {
                idx = Random.Range(0, tiles.Count);
                ppz++;
            }
            
            tiles[idx].PullUp();
        }
    }

}
