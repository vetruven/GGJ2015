using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public float life = 100;
    public ParticleSystem deathParticleSystem;

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

        EventManager.OnArenaChange += FindBestOpenTile;
        FindBestOpenTile();
    }


    public void RemoveLife(float pDamage)
    {
        life -= pDamage;
        if (life < 0)
            DestroyMonster();
    }

    private void DestroyMonster()
    {
        
    }


    void OnDestroy()
    {
        enemies.Remove(this);
        EventManager.OnArenaChange -= FindBestOpenTile;
    }

    private static List<Enemy> InitEnemies()
    {
        if (_enemies == null)
            _enemies = new List<Enemy>();

        return _enemies;
    }

    void Update()
    {
        if (GameModel.isPlaying)
        {
            MoveEnemy();
            CheckIfCloseToEnemy();
        }
    }

    private void MoveEnemy()
    {
        if (transform.position != targetTile.transform.position)
            transform.position = Vector3.MoveTowards(transform.position, targetTile.transform.position,Time.deltaTime*speed);
        else
            FindBestOpenTile();
    }

    private void CheckIfCloseToEnemy()
    {
        
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

            Tile t = rchit.transform.GetComponentInParent<Tile>();
            if (t == null || t.isRisen) return;

            if (targetTile == null)
                targetTile = t;
            else
                targetTile = CheckIfCloser(t, targetTile);
        }
    }

    Tile CheckIfCloser(Tile t1, Tile t2)
    {
        float t1dist = Vector3.Distance(t1.transform.position, target.transform.position);
        float t2dist = Vector3.Distance(t2.transform.position, target.transform.position);

        return t1dist < t2dist ? t1 : t2;
    }
}
