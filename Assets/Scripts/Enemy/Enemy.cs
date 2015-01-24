using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {


    private static List<Enemy> _enemies;
    public static List<Enemy> enemies { get { return InitEnemies();} }

    public Tile targetTile;
    public Player target;

    void Awake()
    {
        enemies.Add(this);

        if(Player.players.Count > 0)
            target = Player.players[Random.Range(0, Player.players.Count)];
        else
            Debug.LogError("No Players To Target");

        FindBestOpenTile();
    }

    void OnDestroy()
    {
        enemies.Remove(this);
    }

    private static List<Enemy> InitEnemies()
    {
        if (_enemies == null)
            _enemies = new List<Enemy>();

        return _enemies;
    }

    void FindBestOpenTile()
    {
        if(target == null)
            return;

        Debug.Log("Start Searching for Player");
        targetTile = null;
        Vector3 pos;

        pos = transform.position + new Vector3(20, -200, 0);
        CheckTileGoodTomMove(pos);

        pos = transform.position + new Vector3(-20, -200, 0);
        CheckTileGoodTomMove(pos);

        pos = transform.position + new Vector3(0, -200, 20);
        CheckTileGoodTomMove(pos);

        pos = transform.position + new Vector3(0, -200, -20);
        CheckTileGoodTomMove(pos);
    }

    private void CheckTileGoodTomMove(Vector3 pPos)
    {
        RaycastHit rchit;
        if (Physics.Raycast(pPos, Vector3.up, out rchit, 400))
        {
            Debug.Log("Physics hit "+rchit.transform.name);

            Tile t = rchit.transform.GetComponent<Tile>();

            targetTile = CheckIfCloser(t, targetTile);
        }
    }

    Tile CheckIfCloser(Tile t1, Tile t2)
    {
        if (t1 == null || t1.isRisen) return t2;
        if (t2 == null || t1.isRisen) return t1;

        float t1dist = Vector3.Distance(t1.transform.position, target.transform.position);
        float t2dist = Vector3.Distance(t2.transform.position, target.transform.position);

        return t1dist < t2dist ? t1 : t2;
    }
}
