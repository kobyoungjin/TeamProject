using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpwan : MonoBehaviour
{
    CharacterControls characterControls;

    void Start()
    {
        characterControls = GameObject.FindObjectOfType<CharacterControls>().GetComponent<CharacterControls>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            characterControls.SetCheckPoint(transform.position);
            this.gameObject.SetActive(false);
        }
    }
}
