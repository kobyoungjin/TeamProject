using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodLog : MonoBehaviour
{
    float time;
    private int currentTime = 0;

    public static bool IsOver = false;
    private void Start()
    {
        StartTimer();
    }

    void Update()
    {
        GetComponent<Rigidbody>().AddForce(transform.position * Time.deltaTime * -0.6f, ForceMode.Impulse);

        if (WoodLog.IsOver == false && time > 0)
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
        if (collision.gameObject.name == "DestroyPlane")
            Destroy(this.gameObject);

        if (collision.gameObject.CompareTag("Player"))
            Destroy(this.gameObject, 1.0f);

    }

    // Ÿ�̸Ӹ� ���ߴ� �޼ҵ�
    public void StopTimer()
    {
        WoodLog.IsOver = true;
    }

    // Ÿ�̸Ӹ� �����ϴ� �޼ҵ�
    public void StartTimer()
    {
        WoodLog.IsOver = false;
        time = 10;  // Ÿ�̸� ���� �ð��� n�ʷ� ���߱� ���� n+1���� ī��Ʈ �ٿ�
    }

    // Ÿ�̸Ӱ� �� ���� �� �ߵ��ϴ� �޼ҵ�
    private void TimeOver()
    {
        Destroy(gameObject);
    }
}
