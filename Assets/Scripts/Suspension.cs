using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suspension : MonoBehaviour
{
    public WheelCollider[] wheelColliders = new WheelCollider[4];
    public GameObject[] wheelMeshes = new GameObject[4];

    private Vector3 wheelCCenter;
    private RaycastHit hit;

    public float springForce = 6000;
    public float damperForce = 1500;

    void FixedUpdate()
    {
        for (int i = 0; i < 4; i++)
        {
            wheelCCenter = wheelColliders[i].transform.TransformPoint(wheelColliders[i].center);

            if (Physics.Raycast(wheelCCenter, -wheelColliders[i].transform.up, out hit, wheelColliders[i].suspensionDistance + wheelColliders[i].radius))
            {
                wheelMeshes[i].transform.position = hit.point + (wheelColliders[i].transform.up * wheelColliders[i].radius);
            }
            else
            {
                wheelMeshes[i].transform.position = wheelCCenter - (wheelColliders[i].transform.up * wheelColliders[i].suspensionDistance);
            }
        }
    }
}
