using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    float jumpForce = 5f;
    public float rotSpeed = 10f;

    Rigidbody rb;
    Camera cam;
    Vector3 startPos;
    Vector3 gravity;

    private bool isGrounded;
    private bool isMovable;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        isMovable = true;
        startPos = transform.position;
        gravity = new Vector3(0, -12f, 0);
        Physics.gravity = gravity;
    }

    void Update()
    {
        if (isMovable == false)
        {
            return;
        }

        if(transform.position.y < -10)
        {
            transform.position = startPos;
        }

        // 점프 확인
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        Move();
    }

    void MoveTranslate()
    {
        // WASD로 이동하기 // KeyCode값을 바꾸면 특정 버튼으로 이동 가능!
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
    
    void MoveVelocity()
    {
        // 이동 입력
        float h = Input.GetAxisRaw("Horizontal");   // 좌우 이동
        float v = Input.GetAxisRaw("Vertical");       // 전후 이동

        // 이동 방향
        Vector3 direction = new Vector3(h, 0, v);
        rb.velocity = direction * speed;
    }

    void Move()
    {
        // Scene View에서 플레이어가 바라보는 방향(정면)으로 플레이어 머리에서 레이저를 쏘는 것으로 보여줌
        // 주의) y값을 반영하면 카메라가 보는 각도로 인해 바닥이나 하늘을 바라보게 되고, 이대로 앞으로 가면 하늘로 승천하거나 땅으로 파묻혀 들어갈 수 있어 y값을 반드시 0으로!
        Debug.DrawRay(this.transform.position + new Vector3(0, 1, 0), new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z).normalized, Color.blue);

        // 이동 입력
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // 전방 주시
        // 정규화를 통해 1만큼의 길이(방향값) 구함
        Vector3 lookFoward = new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z).normalized; // 카메라의 전방 확인
        Vector3 lookRight = new Vector3(cam.transform.right.x, 0f, cam.transform.right.z).normalized;              // 카메라의 오른쪽 확인

        // 캐릭터가 움직일 방향 계산
        Vector3 moveDir = lookFoward * moveInput.y + lookRight * moveInput.x;
        Quaternion rotateFoward = Quaternion.LookRotation(lookFoward);  // 플레어가 앞을 바라봄
        rb.rotation = Quaternion.Slerp(rb.rotation, rotateFoward, rotSpeed * Time.deltaTime);   // 부드럽게 회전
        this.transform.position += moveDir * Time.deltaTime * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 땅을 밟을 경우 
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = true;
            SetGravity(-12.0f);
        }

        // 느려지는 장판을 밟을 경우
        if (collision.gameObject.name == "SlowPlatform")
        {
            SetPlayerSpeed(8.0f);
            SetGravity(-12.0f);
        }
        // 빨라지는 장판을 밟을 경우
        else if (collision.gameObject.name == "FasterPlatform")
        {
            SetPlayerSpeed(2.0f);
            SetGravity(-20.0f);
        }
    }

    // 점프시
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        SetPlayerSpeed(5.0f);
    }

    public void CannotMove()
    {
        isMovable = false;
    }

    // 플레이어를 스턴 상태로 만드는 코루틴
    public IEnumerator SturnState()
    {
        float sec = 0f;
        // 3초동안 스턴 상태(움직일 수 없음)
        isMovable = false;
        while (sec < 2f)
        {
            yield return new WaitForSeconds(0.1f);
            sec += 0.1f;
        }

        isMovable = true;
    }

    // 
    public void SetBoolGrounded(bool isGrounded)
    {
        this.isGrounded = isGrounded;
    }

    // Player 스피드 설정하는 함수
    public void SetPlayerSpeed(float speed)
    {
        this.speed = speed;
    }

    // 플레이어 시작 위치 설정하는 함수
    public void SetStartPos(Vector3 pos)
    {
        startPos = pos;
    }

    // 프로젝트 중력 설정하는 함수
    public void SetGravity(float y)
    {
        Vector3 gravity = new Vector3(0, y, 0);
        Physics.gravity = gravity;
    }
}
