using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    int chasePlayerParamID;
    FieldOfView fov;
    Animator animator;
    void Start()
    {
        chasePlayerParamID = Animator.StringToHash("ChasePlayer");
        animator = GetComponent<Animator>();
        fov = GetComponentInChildren<FieldOfView>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(chasePlayerParamID, fov.chasePlayer);
    }
}
