using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FAV : MonoBehaviour
{
    public Vector3 positionToMoveTo1 = new Vector3(0, 2.2f, -2.83f);
    public Vector3 positionToMoveTo2 = new Vector3(0, 2.2f, 2.83f);
    void Start()
    {
        StartCoroutine(Cor1());
    }
    IEnumerator Cor1()
    {
        while (true)
        {
            yield return StartCoroutine(LerpPosition(positionToMoveTo1, 1));
            yield return new WaitForSeconds(1);
            yield return StartCoroutine(LerpPosition(positionToMoveTo2, 1));
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.localPosition;
        while (time < duration)
        {
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = targetPosition;
    }
}
