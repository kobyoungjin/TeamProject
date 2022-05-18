using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    float power = 2.3f;
    bool isActivated = false;
    Rigidbody playerRidbody;
    GameObject player;

    float current;  // 현재 위치
    float speed = 1.5f;  // 선풍기 속도
    int direction = 1;  
    float max = 6f;
    float min = 0f;


    private void Start()
    {
        player = GameObject.Find("Player");
        playerRidbody = player.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (isActivated)
        {
            playerRidbody.AddForce(Vector3.right * power, ForceMode.Force); // 천천히 뒤로 날려버리기
        }

        current += Time.deltaTime * direction * speed;

        if (current >= max)
        {
            current = max;
            direction *= -1;    // 방향 전환(음수->양수, 양수->음수)
        }
        // 최소값보다 조금이라도 작아지면 최소값으로 재조정
        else if (current <= min)
        {
            current = min;
            direction *= -1;    // 방향 전환(음수->양수, 양수->음수)
        }
        
        transform.position = new Vector3(transform.position.x, current, transform.position.z);  // 플랫폼의 높이값 변화
    }

    private void FixedUpdate()
    {
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    // 트리거에 들어온 오브젝트가 플레이어 태그를 가지고 있고 
    //    if (other.CompareTag("Player") && isActivated == false)
    //    {
    //        isActivated = true;
    //    }
    //}


    IEnumerator ActivateWind()
    {
        float sec = 0f;
        while (sec < 2f)
        {
            yield return new WaitForSeconds(0.1f);
            sec += 0.1f;
        }
        
        playerRidbody = null;
    }

   
    private void OnTriggerStay(Collider other)
    {
        // 트리거에 들어온 오브젝트가 플레이어 태그를 가지고 있고 
        if (other.CompareTag("Player") && isActivated == false)
        {
            isActivated = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActivated = false;
        }
    }
}
