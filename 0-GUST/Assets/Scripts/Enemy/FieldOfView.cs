using UnityEngine;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour {



    private Collider2D[] targets;


	public float fovRadius;
	[Range(0,360)]
	public float fovAngle;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
	public List<Transform> visibleTargets = new List<Transform>();
    public bool chasePlayer = false;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, fovRadius);
    }

    //Ray displayed for debugging
    private void Update()
    {
        Vector2 fovAngle1 = DirFromAngle(-fovAngle / 2);
        Vector2 fovAngle2 = DirFromAngle(fovAngle / 2);

        FindVisibleTargets();
        foreach(Transform t in visibleTargets)
        {
            chasePlayer = true;
        }
        
        if(targets == null || targets.Length == 0)
        {
            chasePlayer = false;
        }
    }

	void FindVisibleTargets() {
		visibleTargets.Clear();
        targets = Physics2D.OverlapCircleAll(transform.position, fovRadius, targetMask);
        //GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < targets.Length; i++) {
            
			Transform target = targets[i].transform;
            
            //if (target.gameObject.tag == "Player")
            //{
                
                Vector2 dirToTarget = new Vector2(target.position.x - transform.position.x, target.position.y-transform.position.y);
                if (Vector2.Angle(dirToTarget,transform.right) < fovAngle / 2)
                {
                    Debug.Log(target.gameObject);
                    float dstToTarget = Vector2.Distance(transform.position, target.position);
                    if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                    {   
                        visibleTargets.Add(target);
                    }
                }
            //}
			
		}
	}

	public Vector2 DirFromAngle(float angle) {
		angle += transform.eulerAngles.z;
		return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
	}

    public Transform getPlayerTransform()
    {
        if(visibleTargets != null && visibleTargets.Count > 0)
        {
            return visibleTargets[0];
        }
        return null;
    }
}
