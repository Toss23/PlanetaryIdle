using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Header("Rotation around Sun")]
    [SerializeField] private float radius = 700f;
    [SerializeField] private float speedAround = 0.1f;
    [SerializeField] private float angle = 0f;

    [Header("Local Rotation")]
    [SerializeField] private float speedLocal = 0.1f;

    private void LateUpdate()
    {
        angle += speedAround * Time.deltaTime;
        transform.position = new Vector3(radius * Mathf.Sin(angle * Mathf.Deg2Rad), 0f, radius * Mathf.Cos(angle * Mathf.Deg2Rad));
        transform.eulerAngles += new Vector3(0f, - speedLocal * Time.deltaTime, 0f);
    }

    private void OnDrawGizmos()
    {
        float stepSize = 0.1f;

        Gizmos.color = Color.white;

        Vector3 beginPoint = Vector3.zero;
        Vector3 firstPoint = Vector3.zero;

        for (float stepSum = 0; stepSum < 2 * Mathf.PI; stepSum += stepSize)
        {
            Vector3 endPoint = new Vector3(radius * Mathf.Cos(stepSum), 0f, radius * Mathf.Sin(stepSum));

            if (stepSum == 0)
                firstPoint = endPoint;
            else
                Gizmos.DrawLine(beginPoint, endPoint);

            beginPoint = endPoint;
        }

        Gizmos.DrawLine(firstPoint, beginPoint);
    }
}
