using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCollider : MonoBehaviour
{
    public float restoreTime = 8.0f;

    private void OnCollisionEnter(Collision collision)
    {
        StopAllCoroutines();

        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;

            StartCoroutine(Time());
        }

    }

    void ChangeColor()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.white;
    }

    IEnumerator Time()
    {
        yield return new WaitForSeconds(restoreTime);
        ChangeColor();
    }
}
