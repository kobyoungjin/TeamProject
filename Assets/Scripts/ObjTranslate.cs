using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjTranslate : MonoBehaviour
{
    GameObject canon;
    float time;
    private int currentTime = 0;
    public static bool IsOver = false;

    private void Start()
    {
        StartTimer();
    }
    void Update()
    {
        if (this.gameObject.transform.position.y < -20)
            Destroy(this.gameObject);

        if (ObjTranslate.IsOver == false && time > 0)
        {
            time -= Time.deltaTime;
            currentTime = (int)time;
            if (currentTime <= 0)
            {
                currentTime = 0;
                StopTimer();
                TimeOver();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject, 0.5f);
        }

        if (collision.gameObject.name == "DestoryPlane")
            Destroy(this.gameObject);

        if (collision.gameObject.name == "FruitWall")
            Destroy(this.gameObject);
    }

    // Ÿ�̸Ӹ� ���ߴ� �޼ҵ�
    public void StopTimer()
    {
        ObjTranslate.IsOver = true;
    }

    // Ÿ�̸Ӹ� �����ϴ� �޼ҵ�
    public void StartTimer()
    {
        ObjTranslate.IsOver = false;
        time = 2.0f;  // Ÿ�̸� ���� �ð��� n�ʷ� ���߱� ���� n+1���� ī��Ʈ �ٿ�
    }

    // Ÿ�̸Ӱ� �� ���� �� �ߵ��ϴ� �޼ҵ�
    private void TimeOver()
    {
        Destroy(gameObject);
    }
}
