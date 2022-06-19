using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    GameManager gameManager;
    GameObject gate;
    GameObject mainCam;
    Camera doorCam;
    bool isFinished = false;
    bool isOpen = false;
    AudioSource audioSource;
    AudioSource mainAudio;
    GameObject openAudio;
    private void Start()
    {
        gate = GameObject.Find("Ex)").transform.Find("Gate").gameObject;
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        mainCam = GameObject.Find("Camera Holder").transform.GetChild(0).transform.GetChild(0).gameObject;
        doorCam = GameObject.Find("Ex)").transform.Find("DoorCamera").GetComponent<Camera>();
        //audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();
        mainAudio = mainCam.GetComponent<AudioSource>();
        openAudio = GameObject.Find("Camera Holder").transform.GetChild(0).transform.GetChild(1).gameObject;
    }

    private void Update()
    {
        if (gate.transform.position.y > 10)
        {
            isFinished = true;
        }

        if (gameManager.GetCountItem() == 0 && !isOpen)
        {
            mainAudio.Stop();
            openAudio.SetActive(true);
            mainCam.SetActive(false);
            doorCam.gameObject.SetActive(true);
            
            TransGate();
        }
    }

    public void TransGate()
    {
        if (isFinished)
        {
            isOpen = true;
            Invoke("TurnOnCam", 2.0f);
            mainAudio.Play();
            audioSource.Stop();
            return;
        }

        //audioSource.Play();
        gate.transform.position += new Vector3(0, 2 * Time.deltaTime, 0);
        //this.transform.Translate(new Vector3(0, 2 * Time.deltaTime, 0));
    }

    void TurnOnCam()
    {
        mainCam.SetActive(true);
        doorCam.gameObject.SetActive(false);
    }
}
