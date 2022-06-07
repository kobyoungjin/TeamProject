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

    // 타이머를 멈추는 메소드
    public void StopTimer()
    {
        WoodLog.IsOver = true;
    }

    // 타이머를 시작하는 메소드
    public void StartTimer()
    {
        WoodLog.IsOver = false;
        time = 10;  // 타이머 시작 시간을 n초로 맞추기 위해 n+1부터 카운트 다운
    }

    // 타이머가 다 됐을 때 발동하는 메소드
    private void TimeOver()
    {
        Destroy(gameObject);
    }
}
