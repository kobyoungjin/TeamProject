using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    GameManager gameManager;

    float speed = 0.2f;
    
    int direction = 1;  // 1이면 Up/Right, 0이면 Down/Left
    float max;
    float min;
    float current;  // 현재 위치

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
        max = transform.position.y + 0.5f;
        min = transform.position.y - 0.5f;
    }

    private void LateUpdate()
    {
        move();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);

            gameManager.DecreaseRemainingItem(1);
        }
    }

    void move()
    {
        current += Time.deltaTime * direction * speed;

        float rotSpeed = 60.0f;

        // 최대값보다 조금이라도 커지면 최대값으로 재조정
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

        // 계산된 위치 값을 대입
        transform.position = new Vector3(transform.position.x, current, transform.position.z);  // 플랫폼의 높이값 변화

        transform.Rotate(new Vector3(0, 1, 0) * rotSpeed * Time.deltaTime, Space.World);
    }
}
