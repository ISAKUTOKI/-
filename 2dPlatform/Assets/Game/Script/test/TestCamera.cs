using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    public Transform target; // Ҫ�����Ŀ������
    public float smoothSpeed = 0.125f; // ƽ���ٶ�
    public Vector3 offset; // ����������Ŀ���λ��ƫ��

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
