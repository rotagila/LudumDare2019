 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public GridManager gridManager;
    private FieldOfView fov;
    public Transform[] patrolPoints;
    public Transform player;
    public GameObject lvlFader;

    public Sprite enemyBack;
    public Sprite enemyFront;
    public Sprite enemySide;

    public List<Vector2Int> path;
    public int current = 0;
    public int patCurrent = 0;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    float minDist = 0.1f;
    float maxRange = 12f;
    public bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        fov = GetComponentInChildren<FieldOfView>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void move(bool patrolling)
    {
        Vector3 target;
        Vector3Int pointCell;
        if (patrolling)
        {
            pointCell = gridManager.grid.WorldToCell(patrolPoints[patCurrent].position);
            target = gridManager.grid.GetCellCenterWorld(pointCell);
        }
        else
        {
            target = gridManager.grid.GetCellCenterWorld((Vector3Int)path[current]);
        }
        if (Vector2.Distance(target, (Vector2)transform.position) < minDist)
        {
            if (patrolling)
            {
                patCurrent++;
                if (patCurrent >= patrolPoints.Length)
                {
                    patCurrent = 0;
                }
            }
            else
            {
                current++;
                if (current >= path.Count)
                {
                    current = 0;
                }
            }
        }
        lookAtDirection(target);
        if(patrolling)
            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * patrolSpeed);
        else
            transform.position = Vector2.MoveTowards(transform.position, target, Time.deltaTime * chaseSpeed);
    }

    void lookAtDirection(Vector3 target)
    {
        float difX = Mathf.Abs(target.x - transform.position.x);
        float difY = Mathf.Abs(target.y - transform.position.y);
        if (difX > difY)
        {
            if (target.x < transform.position.x)
            {
                Debug.Log("FacingLeft");
                GetComponent<SpriteRenderer>().sprite = enemySide;
                GetComponent<SpriteRenderer>().flipX = true;
                transform.GetChild(0).eulerAngles = new Vector3(180, 90, 0);
                transform.GetChild(1).eulerAngles = new Vector3(0, 0, 180);

            }
            if (target.x > transform.position.x)
            {
                Debug.Log("FacingRight");
                GetComponent<SpriteRenderer>().sprite = enemySide;
                GetComponent<SpriteRenderer>().flipX = false;
                transform.GetChild(0).eulerAngles = new Vector3(0, 90, 0);
                transform.GetChild(1).eulerAngles = new Vector3(0, 0, 0);
            }

        }
        else
        {
            if (target.y < transform.position.y)
            {
                Debug.Log("FacingDown");
                GetComponent<SpriteRenderer>().sprite = enemyFront;
                GetComponent<SpriteRenderer>().flipX = false;
                transform.GetChild(0).eulerAngles = new Vector3(90, 90, 0);
                transform.GetChild(1).eulerAngles = new Vector3(0, 0, 270);
            }
            if (target.y > transform.position.y)
            {
                Debug.Log("FacingUp");
                GetComponent<SpriteRenderer>().sprite = enemyBack;
                GetComponent<SpriteRenderer>().flipX = false;
                transform.GetChild(0).eulerAngles = new Vector3(270, 90, 0);
                transform.GetChild(1).eulerAngles = new Vector3(0, 0, 90);
            }
        }
    }

    public void chasePlayer()
    {
        path = gridManager.getPath(transform.position, player.transform.position);
        if (path != null)
        {
            canMove = true;
        }
        //si le garde est atteint le joueur, il s'arrête
        if (Vector3.Distance(transform.position, player.transform.position) <= player.GetComponent<SpriteRenderer>().size.x + minDist)
        {
            canMove = false;
            //On peut faire mourrir le joueur par exemple
            lvlFader.GetComponent<LevelFader>().FadeToLevel();
        }
        if (canMove)
            move(false);
        if (Vector2.Distance(transform.position, player.transform.position) > maxRange)
            fov.chasePlayer = false;
    }

    public void patrol()
    {
        if(patrolPoints != null && patrolPoints.Length >= 1)
        {
            move(true);
        }
    }


}
