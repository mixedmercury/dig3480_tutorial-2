using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{

    public float time;
    public int xMove;
    public int yMove;
    private float startX;
    private float startY;
    private float newX;
    private float newY;

    void Start()
    {
        startX = transform.position.x;
        startY = transform.position.y;
        newX = startX + xMove;
        newY = startY + yMove;
        Vector2 beginPos = new Vector2(startX, startY);
        Vector2 endPos = new Vector2(newX, newY);
        StartCoroutine(Move(beginPos, endPos, time));
    }

    IEnumerator Move(Vector3 beginPos, Vector3 endPos, float time)
    {
        for(float t = 0; t < 1; t += Time.deltaTime / time)
        {
            transform.position = Vector3.Lerp(beginPos, endPos, t);
            yield return null;
        }
        StartCoroutine(MoveBack(endPos, beginPos, time));
    }
    IEnumerator MoveBack(Vector3 endPos, Vector3 beginPos, float time)
    {
        for(float t = 0; t < 1; t += Time.deltaTime / time)
        {
            transform.position = Vector3.Lerp(endPos, beginPos, t);
            yield return null;
        }
        StartCoroutine(Move(beginPos, endPos, time));
    }
}
