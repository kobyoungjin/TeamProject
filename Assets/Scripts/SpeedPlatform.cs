using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPlatform : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>().GetComponent<Player>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "SlowPlatform")
        {
            player.SetPlayerSpeed(8.0f);
        }
        else if(collision.gameObject.name == "FasterPlatform")
        {
            player.SetPlayerSpeed(2.0f);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        player.SetPlayerSpeed(5.0f);
    }
}


