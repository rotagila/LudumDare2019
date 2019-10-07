using UnityEngine;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour {

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
        Vector3 fovAngle1 = DirFromAngle(-fovAngle / 2);
        Vector3 fovAngle2 = DirFromAngle(fovAngle / 2);

        Debug.DrawRay(transform.position, fovAngle1*fovRadius, Color.white);
        Debug.DrawRay(transform.position, fovAngle2*fovRadius, Color.white);

        FindVisibleTargets();
        foreach(Transform t in visibleTargets)
        {
            chasePlayer = true;
            Debug.DrawLine(transform.position, t.position, Color.red);
        }
    }

	void FindVisibleTargets() {
		visibleTargets.Clear();
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, fovRadius, targetMask);
        //GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < targets.Length; i++) {
            
			Transform target = targets[i].transform;
            
            //if (target.gameObject.tag == "Player")
            //{
                
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                if (Vector2.Angle(transform.up, dirToTarget) < fovAngle / 2)
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
		angle += transform.eulerAngles.y;
		return new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
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
