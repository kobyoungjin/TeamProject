using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    TextMeshProUGUI remainingItemUi;

    public int CountItem;
    public float time;
    private int currentTime = 0;

    bool isOver = false;

    private void Start()
    {
        remainingItemUi = GameObject.Find("RemainingItem(Text)").GetComponent<TextMeshProUGUI>();
        CountItem = GameObject.Find("GoalItem").transform.childCount;

        remainingItemUi.text = "남은 개수 : " + CountItem;
    }


    public void DecreaseRemainingItem(int count)
    {
        CountItem -= count;
        remainingItemUi.text = "남은 개수 : " + CountItem;
    }

    public void SetTime(int time)
    {
        this.time = time;
    }
    public void SetOver(bool isOver)
    {
        this.isOver = isOver;
    }

    public int GetCountItem()
    {
        return CountItem;
    }

    void CountTime()
    {
        if (isOver == false && time > 0)
        {
            time -= Time.deltaTime;
            currentTime = (int)time;
            if (currentTime <= 0)
            {
                currentTime = 0;
                StopTimer();
            }
        }
    }

    // Ÿ�̸Ӹ� ���ߴ� �޼ҵ�
    public void StopTimer()
    {
        isOver = true;
    }

    // Ÿ�̸Ӹ� �����ϴ� �޼ҵ�
    public void StartTimer()
    {
        isOver = false;
        time = 10;  // Ÿ�̸� ���� �ð��� n�ʷ� ���߱� ���� n+1���� ī��Ʈ �ٿ�
    }
}
