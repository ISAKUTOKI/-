using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    public float speed = 5.0f; // �����ƶ��ٶ�

    void Update()
    {
        // ��ȡˮƽ�ʹ�ֱ����
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // �����ƶ�����
        Vector3 movement = new Vector3(horizontal, vertical, 0);

        // ��������λ��
        transform.position += movement * speed * Time.deltaTime;
    }
}
