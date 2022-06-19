using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    GameManager gameManager;
    GameObject gate;
    GameObject mainCam;
    Camera doorCam;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        mainCam = GameObject.Find("Camera Holder").transform.GetChild(0).transform.GetChild(0).gameObject;
        doorCam = GameObject.Find("Ex)").transform.Find("DoorCamera").GetComponent<Camera>();
    }

    private void Update()
    {
        if (gameManager.GetCountItem() == 0)
        {
            mainCam.SetActive(false);
            doorCam.gameObject.SetActive(true);
            TransGate();
        }
    }

    public void TransGate()
    {
        if (transform.position.y >= 10)
        {
            mainCam.SetActive(true);
            doorCam.gameObject.SetActive(false);
            return;
        }

        transform.Translate(Vector3.up * Time.deltaTime);
    }
}
