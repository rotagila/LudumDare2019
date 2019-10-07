 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public GridManager gridManager;
    public FieldOfView fov;
    public Transform player;

    public List<Vector2Int> path;
    public int current = 0;
    float speed = 2f;
    float minDist = 0.01f;
    public bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        fov = GetComponent<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        //A remplacer par si le joueur est détécté;
        if (Input.GetButton("Jump"))
        {
            Debug.Log("space pressed");
            path = gridManager.getPath(transform.position, player.position);
            if(path != null)
            {
                canMove = true;
            }
        }
        if (canMove)
            move();
    }

    void move()
    {
        Vector3 gridSpotCellCenter = gridManager.grid.GetCellCenterWorld((Vector3Int)path[current]);
        if (Vector2.Distance(gridSpotCellCenter, (Vector2)transform.position) < minDist)
        {
            current++;
            if(current >= path.Count)
            {
                current = 0;
                canMove = false;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, gridSpotCellCenter, Time.deltaTime * speed);
    }
}
