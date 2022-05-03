using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPS : MonoBehaviour
{
    GameObject player;

    public float distance = 7.0f;   // currentZoom���� ��Ȯ�� �̸����� ����
    public float minZoom = 5.0f;
    public float maxZoom = 10.0f;
    public float sensitivity = 100f; // ���콺 ����

    float x;
    float y;
    void Start()
    {
        // �÷��̾� �±׸� ���� ���ӿ�����Ʈ(=�÷��̾�)�� ã�Ƽ� �ֱ�
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RotateAround();
        CalculateZoom();
    }

    // ī�޶� Ȯ���� ���
    void CalculateZoom()
    {
        // ���콺 �� ��/�ƿ�
        distance -= Input.GetAxis("Mouse ScrollWheel");

        // �� �ּ�/�ִ� ����
        // Clamp�Լ� : �ִ�/�ּҰ��� �������ְ� ����
        distance = Mathf.Clamp(distance, minZoom, maxZoom);
    }

    void RotateAround()
    {
        // ���콺�� ��ġ�� �޾ƿ���
        x += Input.GetAxis("Mouse X") * sensitivity * 0.01f; // ���콺 �¿� ������ ����
        y -= Input.GetAxis("Mouse Y") * sensitivity * 0.01f; // ���콺 ���� ������ ����

        // ī�޶� ���̰�(������������) ����
        if (y < 10)  // �ٴ��� ���� �ʰ�
            y = 10;
        if (y > 50) // Top View(�������� ��������)�� �ϰ� �ʹٸ� 90���� �ٲٱ�
            y = 50;

        // player.transform�� ���� ����Ұǵ� �ʹ� �� ġȯ => target
        Transform target = player.transform;

        // ī�޶� ȸ���� ������ �̵��� ��ġ ���
        Quaternion angle = Quaternion.Euler(y, x, 0);

        Vector3 destination = angle * (Vector3.back * distance) + target.position + Vector3.zero;

        transform.rotation = angle;             // ī�޶� ���� ����
        transform.position = destination;   // ī�޶� ��ġ ����
    }
}
