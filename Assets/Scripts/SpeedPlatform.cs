using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPlatform : MonoBehaviour
{
    private CharacterControls player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<CharacterControls>().GetComponent<CharacterControls>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "SlowPlatform")
        {
            player.SetPlayerSpeed(7.0f);
        }
        else if(collision.gameObject.name == "FasterPlatform")
        {
            player.SetPlayerSpeed(18.0f);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        player.SetPlayerSpeed(10.0f);
    }
}


