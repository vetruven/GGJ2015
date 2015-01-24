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

    public static List<Tile> tiles; 


    void Awake()
    {
        tiles = new List<Tile>();

        for (int z = 0; z < height; z++)
            for (int x = 0; x < width; x++)
                CreateTile(x, z);

        EventManager.OnArenaChange += ChangeArena;
        EventManager.OnGameStart += ChangeArena;
        EventManager.OnWaveFinish += Resetmap;
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

    private void ChangeArena()
    {
        Resetmap();
        StartMapGeneration();
    }

    private void StartMapGeneration()
    {
        for (int i = 0; i < Random.Range(minWallCount, maxWallCount); i++)
            GetFreeTile().PullUp();
    }


    private void Resetmap()
    {
        foreach (Tile tile in tiles)
            tile.PullDown();
    }

    public static Tile GetFreeTile()
    {
        int initIdx = Random.Range(0, tiles.Count);
        int currIdx = (initIdx + 1) % tiles.Count;
        while(currIdx != initIdx)
        {
            if (tiles[currIdx].isEmpty)
                return tiles[currIdx];

            currIdx = (currIdx + 1) % tiles.Count;
        }

        Debug.LogError("No Free Tile Found!");
        return null;
    }
}
