using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerp : MonoBehaviour
{
    public Transform target;
    public float speed;
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, target.position, speed *Time.deltaTime);
    }
}
