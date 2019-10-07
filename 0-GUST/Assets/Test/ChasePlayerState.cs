using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerState : StateMachineBehaviour
{
    public GridManager gridManager;
    public FieldOfView fov;
    public GameObject player;
    public List<Vector2Int> path;
    public int current = 0;
    float speed = 2f;
    float minDist = 0.01f;
    public bool canMove = false;
    public Transform transform;
    public float minRange = 0.2f;
    public float maxRange = 4f;
    public int direction;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gridManager = FindObjectOfType<GridManager>();
        transform = animator.transform;
        fov = FindObjectOfType<FieldOfView>();
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        path = gridManager.getPath(transform.position, player.transform.position);
        if (path != null)
        {
            canMove = true;
        }
        //si le garde est atteint le joueur, il s'arrête
        if (Vector3.Distance(transform.position, player.transform.position) <= player.GetComponent<SpriteRenderer>().size.x + minRange)
        {
            canMove = false;
            path.Clear();
        }
        else
        {
            canMove = true;
        }
        if (canMove)
            move();
        if (Vector3.Distance(transform.position, player.transform.position) <= maxRange)
            fov.chasePlayer = false;

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

void move()
    {
        //on récupère le centre de la cellule de la tilemap
        Vector2 gridSpotCellCenter = gridManager.grid.GetCellCenterWorld((Vector3Int)path[current]);

        //on détermine dans quel sens le guarde doit regarder
        float difX = Mathf.Abs(path[current].x - transform.position.x);
        float difY = Mathf.Abs(path[current].y - transform.position.y);
        if(difX > difY)
        {
            if (path[current].x < transform.position.x)
            {
                Debug.Log("FacingLeft");
                transform.eulerAngles = new Vector3(0, 0, 90);
            }
            if (path[current].x > transform.position.x)
            {
                Debug.Log("FacingRight");
                transform.eulerAngles = new Vector3(0, 0, -90);
            }

        }
        else
        {
            if (path[current].y < transform.position.y)
            {
                Debug.Log("FacingDown");
                transform.eulerAngles = new Vector3(0, 0, 180);
            }
            if (path[current].y > transform.position.y)
            {
                Debug.Log("FacingUp");
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        //Si on a atteint le noeud courrant, on passe au suivant
        if (Vector2.Distance(gridSpotCellCenter, (Vector2)transform.position) < minDist)
        {
            current++;
            if (current >= path.Count)
            {
                current = 0;
                canMove = false;
            }
        }
        //Sinon on avance
        transform.position = Vector2.MoveTowards(transform.position, gridSpotCellCenter, Time.deltaTime * speed);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
