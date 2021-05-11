using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour
{

	public float viewRadius;
	[Range(0, 360)]
	public float viewAngle;

	public LayerMask targetMask;
	public LayerMask objectMask;
	public LayerMask obstacleMask;

	//[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();
	public List<Transform> visibleObjects = new List<Transform>();
	void Start()
	{
		StartCoroutine("FindTargetsWithDelay", .2f);
	}
	IEnumerator FindTargetsWithDelay(float delay)
	{
		while (true)
		{
			yield return new WaitForSeconds(delay);
			FindVisibleTargets();
		}
	}

/*
	void Update() {
		FindVisibleTargets();
	}
*/
	public void FindVisibleTargets()
	{
		visibleTargets.Clear();
		visibleObjects.Clear();

		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
		Collider[] objectsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, objectMask);

		for (int i = 0; i < targetsInViewRadius.Length; i++)
		{
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
			{
				float dstToTarget = Vector3.Distance(transform.position, target.position);

				if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
				{
					visibleTargets.Add(target);
					// Debug.Log("Found Target");
				}
			}
		}

		for (int i = 0; i < objectsInViewRadius.Length; i++)
		{
			Transform targetObj = objectsInViewRadius[i].transform;
			Vector3 dirToTarget = (targetObj.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
			{
				float dstToTarget = Vector3.Distance(transform.position, targetObj.position);

				if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
				{
					visibleObjects.Add(targetObj);
					// Debug.Log("Found Target");
				}
			}
		}

	}


	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}
