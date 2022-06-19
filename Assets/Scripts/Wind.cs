using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    float power = 2.0f;
    bool isActivated = false;
    Rigidbody playerRidbody;
    GameObject player;

    float current;  // ���� ��ġ
    float speed = 1.5f;  // ��ǳ�� �ӵ�
    int direction = 1;  
    float max = 6f;
    float min = 0f;
    CharacterControls characterControls;

    private void Start()
    {
       // player = GameObject.Find("Player");
       //playerRidbody = player.GetComponent<Rigidbody>();
        characterControls = GameObject.FindObjectOfType<CharacterControls>().GetComponent<CharacterControls>();
    }

    private void Update()
    {
        //if (isActivated)
        //{
        //    playerRidbody.AddForce(Vector3.right * power, ForceMode.Force); // õõ�� �ڷ� ����������
        //}

        current += Time.deltaTime * direction * speed;

        if (current >= max)
        {
            current = max;
            direction *= -1;    // ���� ��ȯ(����->���, ���->����)
        }
        // �ּҰ����� �����̶� �۾����� �ּҰ����� ������
        else if (current <= min)
        {
            current = min;
            direction *= -1;    // ���� ��ȯ(����->���, ���->����)
        }
        
        transform.position = new Vector3(transform.position.x, current, transform.position.z);  // �÷����� ���̰� ��ȭ
    }

    private void FixedUpdate()
    {
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    // Ʈ���ſ� ���� ������Ʈ�� �÷��̾� �±׸� ������ �ְ� 
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
        // Ʈ���ſ� ���� ������Ʈ�� �÷��̾� �±׸� ������ �ְ� 
        if (other.CompareTag("Player") && characterControls.GetIsActive() == false)
        {
            characterControls.SetIsActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            characterControls.SetIsActive(false);
        }
    }
}
