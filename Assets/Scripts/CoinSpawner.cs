using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinSpawner : MonoBehaviour
{
    //GameObject prefab_obj;
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //prefab_obj = Resources.Load("Prefabs/PirateCoin") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
            /*
        GameObject obj = MonoBehaviour.Instantiate(prefab_obj);
        obj.name = "clone";
        Vector3 pos = new Vector3(0, 2, 196);
        obj.transform.position = pos; */
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Entered");
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Coin>();
        /*
        GameObject obj = MonoBehaviour.Instantiate(prefab_obj);
        obj.name = "clone";
        Vector3 pos = new Vector3(0, 2, 196);
        obj.transform.position = pos; */
        SceneManager.LoadScene("3");
    }
}
