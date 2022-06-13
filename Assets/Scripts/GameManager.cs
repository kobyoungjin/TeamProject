using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject obj;
    GameObject fruitIns;
    Transform LogSpawnPos;

    public float woodLogSpwan = 7.0f;
    bool isIns = true;
    
    void Start()
    {
        obj = Resources.Load<GameObject>("Prefabs/WoodLog");
        fruitIns = GameObject.Find("FruitIns");
        LogSpawnPos = GameObject.Find("WoodLogSpawn").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isIns)
        {
            StartCoroutine(InstanceLog());
        }
    }

    void ins()
    {
        GameObject ins = Instantiate(obj, LogSpawnPos.position, obj.transform.rotation);
        ins.transform.parent = fruitIns.transform;
        ins.name = obj.name;

        isIns = false;
    }

    IEnumerator InstanceLog()
    {
        ins();

        yield return new WaitForSeconds(woodLogSpwan);
        isIns = true;
    }
}
