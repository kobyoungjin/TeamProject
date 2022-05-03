using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 5f;
    Rigidbody rb;

    float jumpForce = 5f;
    private bool isGrounded;

    Camera cam;
    public float rotSpeed = 10f;

    bool isMovable;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        isMovable = true;
    }

    void Update()
    {
        if (isMovable == false)
        {
            return;
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
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = false;
        }
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

    public void SetBoolGrounded(bool isGrounded)
    {
        this.isGrounded = isGrounded;
    }
}
