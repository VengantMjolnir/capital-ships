using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[ExecuteInEditMode]
public class Target : MonoBehaviour
{
    public RuntimeTargetSet targetingList;
    [SerializeField]
    private Vector3[] targetPoints;

    // Start is called before the first frame update
    void Start()
    {
        if (targetingList != null)
        {
            targetingList.Add(this);
        }
    }

    public Vector3 GetTargetPoint(int seed = -1)
    {
        Random rng = new Random(seed != -1 ? seed : UnityEngine.Random.Range(0, int.MaxValue));

        if (targetPoints == null || targetPoints.Length == 0)
        {
            return transform.position;
        }
        return transform.position + targetPoints[rng.Next(targetPoints.Length)];
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (targetPoints != null)
        {
            for (int i = 0; i < targetPoints.Length; ++i)
            {
                Gizmos.DrawWireSphere(transform.position + targetPoints[i], 0.1f);
            }
        }
    }
}
