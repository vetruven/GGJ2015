using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    public GameObject TilePrefab;
    public int width = 20;
    public int height = 20;
    public float tileSize = 20;

    public float crackWidth = 1;

    public List<Tile> tiles; 


    void Awake()
    {
        for (int x = 0; x < width; x++)
            for (int z = 0; z < height; z++)
                CreateTile(x, z);
    }

    private void CreateTile(int pX, int pZ)
    {
        Tile t = ((GameObject)Instantiate(TilePrefab)).GetComponent<Tile>();
        t.transform.position = transform.position + new Vector3(pX * (crackWidth+tileSize),-0.5f,pZ * (crackWidth+tileSize) );
        t.coord = new Vector2(pX, pZ);
        t.transform.parent = transform;
        tiles.Add(t);
    }
}
