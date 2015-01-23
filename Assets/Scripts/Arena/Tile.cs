using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject cap;
    public Vector2 coord;
    float speed = 40;
    public bool isTaken { get { return GetIsTaken(); } }


    public bool isRisen = false; 
    bool isMoving = false;

    public Vector3 origPos;
    public Vector3 targetPos;

    void Awake()
    {
        origPos = transform.position;
    }

    public void PullUp()
    {
        targetPos = origPos + new Vector3(0, Random.Range(10, 25), 0);
        isRisen = true;
        isMoving = true;
    }

    public void PullDown()
    {
        targetPos = origPos;
        isRisen = false;
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
            isMoving = transform.position != targetPos;
        }
    }

    private bool GetIsTaken()
    {
        if (isRisen)
            return true;

        foreach (var p in Player.players)
            if (Vector3.Distance(transform.position, p.transform.position) < 40)
                return true;

        return false;
    }
}
