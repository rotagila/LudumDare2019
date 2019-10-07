using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFader : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSceneComplete()
    {
        //Changement de scène
        
        Debug.Log("Changement de scène => à executer dans LevelFader.cs.OnSceneComplete");
        SceneManager.LoadScene(1);
    }

    public void FadeToLevel()
    {
        animator.SetTrigger("FadeOut");
    }
}
