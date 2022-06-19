using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    private void Update()
    {
        if(gameManager.GetCountItem() == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
