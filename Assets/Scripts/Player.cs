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

        // ���� Ȯ��
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        Move();
    }

    void MoveTranslate()
    {
        // WASD�� �̵��ϱ� // KeyCode���� �ٲٸ� Ư�� ��ư���� �̵� ����!
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
        // �̵� �Է�
        float h = Input.GetAxisRaw("Horizontal");   // �¿� �̵�
        float v = Input.GetAxisRaw("Vertical");       // ���� �̵�

        // �̵� ����
        Vector3 direction = new Vector3(h, 0, v);
        rb.velocity = direction * speed;
    }

    void Move()
    {
        // Scene View���� �÷��̾ �ٶ󺸴� ����(����)���� �÷��̾� �Ӹ����� �������� ��� ������ ������
        // ����) y���� �ݿ��ϸ� ī�޶� ���� ������ ���� �ٴ��̳� �ϴ��� �ٶ󺸰� �ǰ�, �̴�� ������ ���� �ϴ÷� ��õ�ϰų� ������ �Ĺ��� �� �� �־� y���� �ݵ�� 0����!
        Debug.DrawRay(this.transform.position + new Vector3(0, 1, 0), new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z).normalized, Color.blue);

        // �̵� �Է�
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // ���� �ֽ�
        // ����ȭ�� ���� 1��ŭ�� ����(���Ⱚ) ����
        Vector3 lookFoward = new Vector3(cam.transform.forward.x, 0f, cam.transform.forward.z).normalized; // ī�޶��� ���� Ȯ��
        Vector3 lookRight = new Vector3(cam.transform.right.x, 0f, cam.transform.right.z).normalized;              // ī�޶��� ������ Ȯ��

        // ĳ���Ͱ� ������ ���� ���
        Vector3 moveDir = lookFoward * moveInput.y + lookRight * moveInput.x;
        Quaternion rotateFoward = Quaternion.LookRotation(lookFoward);  // �÷�� ���� �ٶ�
        rb.rotation = Quaternion.Slerp(rb.rotation, rotateFoward, rotSpeed * Time.deltaTime);   // �ε巴�� ȸ��
        this.transform.position += moveDir * Time.deltaTime * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ���� ���� ��� 
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = true;
            SetGravity(-12.0f);
        }

        // �������� ������ ���� ���
        if (collision.gameObject.name == "SlowPlatform")
        {
            SetPlayerSpeed(8.0f);
            SetGravity(-12.0f);
        }
        // �������� ������ ���� ���
        else if (collision.gameObject.name == "FasterPlatform")
        {
            SetPlayerSpeed(2.0f);
            SetGravity(-20.0f);
        }
    }

    // ������
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

    // �÷��̾ ���� ���·� ����� �ڷ�ƾ
    public IEnumerator SturnState()
    {
        float sec = 0f;
        // 3�ʵ��� ���� ����(������ �� ����)
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

    // Player ���ǵ� �����ϴ� �Լ�
    public void SetPlayerSpeed(float speed)
    {
        this.speed = speed;
    }

    // �÷��̾� ���� ��ġ �����ϴ� �Լ�
    public void SetStartPos(Vector3 pos)
    {
        startPos = pos;
    }

    // ������Ʈ �߷� �����ϴ� �Լ�
    public void SetGravity(float y)
    {
        Vector3 gravity = new Vector3(0, y, 0);
        Physics.gravity = gravity;
    }
}
