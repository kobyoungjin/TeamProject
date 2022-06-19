using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldBox : MonoBehaviour
{
    GameObject openBox;
    GameObject closeBox;
    ParticleSystem particleSystem;

    private void Start()
    {
        closeBox = GameObject.Find("TreasureRoom").transform.GetChild(0).gameObject;
        openBox = GameObject.Find("TreasureRoom").transform.GetChild(1).gameObject;
        particleSystem = openBox.transform.GetChild(1).GetComponent<ParticleSystem>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            closeBox.SetActive(false);
            openBox.SetActive(true);

            particleSystem.Play();
            Invoke("SceneChange", 3f);
            //SceneManager.LoadScene("Ending");
        }
    }

    private void SceneChange()
    {
        SceneManager.LoadScene("Ending");

    }
}
