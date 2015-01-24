using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject graphicsBody;
    public float speed = 10f;
    public float life = 100;
    float origLife = 100;
    public float damage = 30;

    public ParticleSystem deathParticleSystem;
    public Slider lifeSlider;

    private static List<Enemy> _enemies;
    public static List<Enemy> enemies { get { return InitEnemies();} }

    public Tile targetTile;
    public Player target;

    private bool isDead;

    void Awake()
    {
        origLife = life;
        lifeSlider.value = life / origLife;
        PickANewTarget();
        FindBestOpenTile();

        enemies.Add(this);
        EventManager.OnArenaChange += FindBestOpenTile;
        EventManager.OnPlayerExplode += PickANewTarget;
    }

    void PickANewTarget()
    {
        if (Player.players.Count > 0)
            target = Player.players[Random.Range(0, Player.players.Count)];
        else
            Debug.LogError("No Players To Target");
    }

    public void RemoveLife(float pDamage)
    {
        life -= pDamage;
        lifeSlider.value = life/origLife;

        if (life < 0)
            DestroyMonster();
    }

    private void DestroyMonster()
    {
        graphicsBody.SetActive(false);
        deathParticleSystem.Play();
        isDead = true;
        EventManager.EnemyExplode(transform.position, damage);

        enemies.Remove(this);
        collider.enabled = false;
        EventManager.OnArenaChange -= FindBestOpenTile;
        EventManager.OnPlayerExplode -= PickANewTarget;
    }

    private static List<Enemy> InitEnemies()
    {
        if (_enemies == null)
            _enemies = new List<Enemy>();

        return _enemies;
    }

    void Update()
    {
        if (GameModel.isPlaying && !isDead && target != null)
        {
            MoveEnemy();
            CheckIfCloseToPlayers();
        }

        if(isDead && !deathParticleSystem.isPlaying)
            Destroy(gameObject);
    }

    private void MoveEnemy()
    {
        if (rigidbody.position != targetTile.transform.position)
            rigidbody.position = Vector3.MoveTowards(rigidbody.position, targetTile.transform.position, Time.deltaTime * speed);
        else
            FindBestOpenTile();
    }

    private void CheckIfCloseToPlayers()
    {
        foreach (Player p in Player.players)
            if(Vector3.Distance(transform.position, p.transform.position) < 15)
                DestroyMonster();
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

        if (targetTile != null)
            graphicsBody.transform.forward = (targetTile.transform.position - transform.position).normalized;
    }

    private void CheckTileGoodTomMove(Vector3 pPos)
    {
        RaycastHit rchit;
        if (Physics.Raycast(pPos, Vector3.up, out rchit, 400))
        {
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
