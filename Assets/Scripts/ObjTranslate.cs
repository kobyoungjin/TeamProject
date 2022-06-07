using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjTranslate : MonoBehaviour
{
    GameObject canon;

    private void Start()
    {
       
    }
    void Update()
    {
        if(this.gameObject.transform.position.y < -20)
            Destroy(this.gameObject);


    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject, 2.0f);
        }

        if (collision.gameObject.name == "DestoryPlane")
            Destroy(this.gameObject);
    }
}
