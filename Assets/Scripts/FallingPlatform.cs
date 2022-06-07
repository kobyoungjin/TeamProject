using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody rb;
    Player player;
    public float remainingTime = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<Player>().GetComponent<Player>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name.Equals("Player"))
        {
            Invoke("Fall", remainingTime);
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            Destroy(gameObject, 0.5f);
        }
    }

    void Fall()
    {
        rb.isKinematic = false;
        player.SetBoolGrounded(false);
    }
}
