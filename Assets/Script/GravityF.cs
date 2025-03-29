using System.Collections.Generic;
using UnityEngine;

public class GravityF : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.00067f;
    public static List<GravityF> gravityObjectList;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (gravityObjectList == null)
            gravityObjectList = new List<GravityF>();

        gravityObjectList.Add(this);
    }

    private void FixedUpdate()
    {
        foreach (var obj in gravityObjectList)
        {
            if (obj != this)
                Attract(obj);
        }
    }

    void Attract(GravityF other)
    {
        Rigidbody otherRb = other.rb;
        Vector3 direction = rb.position - otherRb.position;
        float distance = direction.magnitude;

        float forceMagnitude = G * (rb.mass * otherRb.mass) / Mathf.Pow(distance, 2);
        Vector3 gravityForce = forceMagnitude * direction.normalized;

        otherRb.AddForce(gravityForce);
    }
}
