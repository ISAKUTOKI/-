using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraFollow : MonoBehaviour
{
    public Transform knight; // Ŀ�����壨��ɫ��
    public Vector3 offset = new Vector3(0, 0, -15); // �����Ŀ�������λ��ƫ��
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (knight != null)
        {
            Vector3 desiredPosition = knight.position + offset;
            transform.position = desiredPosition;
        }
    }
}
