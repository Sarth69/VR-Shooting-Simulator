using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Transform rotationPoint;
    public Transform maxLeft;
    public Transform maxRight;
    public float speed;
    public int scoreValue = 1;

    private bool rotated = false;
    private bool hit = false;

    private void Start()
    {
        StartCoroutine(translateTarget());
    }

    public void targetHit()
    {
        if (!hit)
        {
            StartCoroutine(rotateTarget());
            Score.score = scoreValue;
            hit = true;
        }
    }

    IEnumerator rotateTarget()
    {
        bool continueCoroutine = true;
        while (continueCoroutine)
        {
            if (rotated)
            {
                transform.RotateAround(rotationPoint.position, transform.right, -90);
                rotated = false;
                continueCoroutine = false;
                hit = false;
                yield return null;
            } else
            {
                transform.RotateAround(rotationPoint.position, transform.right, 90);
                rotated = true;
                yield return new WaitForSeconds(5);
            }
        }
    }

    IEnumerator translateTarget()
    {
        bool movingLeft = Random.Range(0, 1) < 0.5f ? true : false;
        while (true)
        {
            if (!hit)
            {
                if (movingLeft)
                {
                    if (Vector3.Distance(transform.position, maxLeft.position) < 0.1f)
                    {
                        movingLeft = false;
                    } else
                    {
                        transform.position = Vector3.MoveTowards(transform.position, maxLeft.position, speed * Time.deltaTime);
                    }
                    yield return null;
                } else
                {
                    if (Vector3.Distance(transform.position, maxRight.position) < 0.1f)
                    {
                        movingLeft = true;
                    }
                    else
                    {
                        transform.position = Vector3.MoveTowards(transform.position, maxRight.position, speed * Time.deltaTime);
                    }
                    yield return null;
                }
            }
            yield return null;
        }
    }
}
