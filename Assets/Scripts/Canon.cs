using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    GameObject player;
    GameObject rotater;
    GameObject[] items;
    GameObject fruitIns;
    Camera cam;


    float speed = 10.0f;
    public float itemSpwanTime = 2.5f;
    bool isShoot = true;

    void Start()
    {
        player = GameObject.Find("Player");
        items = Resources.LoadAll<GameObject>("Prefabs/CanonItems");
        rotater = this.transform.GetChild(1).gameObject;
        fruitIns = GameObject.Find("FruitIns");

        cam = Camera.main;
    }

    void Update()
    {
        Debug.DrawRay(rotater.transform.position + new Vector3(0, 2, 0), rotater.transform.forward * 5, Color.red);

        if (isShoot)
            StartCoroutine(Fruit());
    }

    void Shoot()
    {
        GameObject fruit = RandomFruit();
        GameObject canonPos = rotater.transform.GetChild(0).gameObject;

        int angle = RandomCanonTrans();
        rotater.transform.localRotation = Quaternion.Euler(0, rotater.transform.localRotation.y + angle, 0);
        GameObject ins = Instantiate(fruit, canonPos.transform.GetChild(0).transform.position, rotater.transform.localRotation);
        ins.transform.parent = fruitIns.transform;
        ins.name = fruit.name;

        ins.GetComponent<Rigidbody>().AddRelativeForce(canonPos.transform.position * speed);


        //if (fruit.name == "banana")
            //ins.transform.rotation = new Quaternion(0, rotater.transform.rotation.y, 0, 0);

        //int random = Random.Range(3, 8);
        //ins.GetComponent<Rigidbody>().AddForce(rotater.transform.forward * 10);
        //ins.transform.Translate(rotater.transform.position * random * Time.deltaTime, Space.Self);
        

        isShoot = false;
    }

    int RandomCanonTrans()
    {
        int angle = 0;

        while (true)
        {
            angle = Random.Range(-15, 15);
            if (angle % 5 == 0)
            {
                //Debug.Log(angle);
                return angle;
            }
                
        }
        //Debug.Log(rotation.transform.eulerAngles);
    }

    GameObject RandomFruit()
    {
        int num = Random.Range(0, items.Length);
        GameObject fruit = items[num];

        return fruit;
    }

    IEnumerator Fruit()
    {
        Shoot();
        yield return new WaitForSeconds(itemSpwanTime);
        isShoot = true;
    }
}
