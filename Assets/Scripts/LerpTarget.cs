using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTarget : MonoBehaviour
{
    public Vector3 start;
    public Vector3 end;
    void Start()
    {
        StartCoroutine(C1());
    }

    public IEnumerator C1()
    {
        while (true)
        {
            transform.position = start;
            yield return new WaitForSeconds(1);
            transform.position = end;
            yield return new WaitForSeconds(1);
        }
    }
}
