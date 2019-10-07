using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerState : StateMachineBehaviour
{
    public GridManager gridManager;
    public FieldOfView fov;
    public GameObject player;

    public Guard guard;

    public List<Vector2Int> path;
    public int current = 0;
    float speed = 2f;
    float minDist = 0.01f;
    public bool canMove = false;
    public Transform transform;
    public float minRange = 0.2f;
    public float maxRange = 10f;
    public int direction;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        gridManager = FindObjectOfType<GridManager>();
        transform = animator.transform;
        guard = animator.gameObject.GetComponent<Guard>();
        fov = FindObjectOfType<FieldOfView>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        guard.test();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
