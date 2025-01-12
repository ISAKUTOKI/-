using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SIS_JKey : MonoBehaviour
{
    public GameObject JKey;
    public SpriteRenderer JKeyRenderer;

    [HideInInspector] public bool seeable = false;
    [HideInInspector] public bool canSelfRotate = false;
    [HideInInspector] public bool canSelfScale = false;
    [HideInInspector] public bool isTappingKey = false;
    [HideInInspector] public bool isTapScaling = false;

    //����Ϊ��ת�õı���
    private float rotationSpeed = 3.0f; // ��ת�ٶ�
    private float maxRotationAngle = 2.0f; // �����ת�Ƕ�
    private bool rotateClockwise = true; // ��ת����trueΪ˳ʱ�룬falseΪ��ʱ��
    private float currentRotationAngle = 0.0f; // ��ǰ��ת�Ƕ�

    //����Ϊ�����õı���
    private float scaleSpeed = 1.8f; // �����ٶ�
    private float maxScale = 1.3f; // ������ű���
    private float minScale = 1.0f; // ��С���ű���
    private bool isScalingUp = true; // ���ŷ���trueΪ�Ŵ�falseΪ��С
    private Vector3 originalScale; // �����ԭʼ���ű���

    void Start()
    {
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        HintKeyVisibility();
        if (canSelfRotate)
            SelfRotate();
        if (canSelfScale)
            SelfScale();
        if (isTappingKey)
            TappingKey();
    }


    void HintKeyVisibility()
    {
        if (seeable)
            JKeyRenderer.enabled = true;
        if (!seeable)
            JKeyRenderer.enabled = false;
    }
    void SelfRotate()
    {
        if (rotateClockwise)
        {
            currentRotationAngle += rotationSpeed * Time.deltaTime;
            if (currentRotationAngle >= maxRotationAngle)
            {
                rotateClockwise = false; // �ﵽ���Ƕȣ��ı���ת����
            }
        }
        else
        {
            currentRotationAngle -= rotationSpeed * Time.deltaTime;
            if (currentRotationAngle <= -maxRotationAngle)
            {
                rotateClockwise = true; // �ﵽ��С�Ƕȣ��ı���ת����
            }
        }
        // Ӧ����ת������
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, currentRotationAngle);
    }
    void SelfScale()
    {
        // �������ŷ�����µ�ǰ���ű���
        if (isScalingUp)
        {
            // �𲽷Ŵ�
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale * maxScale, scaleSpeed * Time.deltaTime);
            // ����Ƿ�ﵽ������ű���
            if (transform.localScale.x >= originalScale.x * maxScale - 0.1f)
            {
                isScalingUp = false; // �ı����ŷ���Ϊ��С
            }
        }
        else
        {
            // ����С
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale * minScale, scaleSpeed * Time.deltaTime);
            // ����Ƿ�ﵽ��С���ű���
            if (transform.localScale.x <= originalScale.x * minScale + 0.1f)
            {
                isScalingUp = true; // �ı����ŷ���Ϊ�Ŵ�
            }
        }
    }

    void TappingKey()
    {
        if (isTappingKey && Input.GetKeyDown(KeyCode.J) && !isTapScaling) // ����Ƿ���J�����ҵ�ǰû�����Ų����ڽ���
        {
            StartCoroutine(ScaleJKey());
            //Debug.Log("OK");
        }
    }
    IEnumerator ScaleJKey()
    {
        isTapScaling = true; // ��ʼ���Ų��������ñ�־Ϊtrue

        yield return StartCoroutine(ScaleOverTime(1.3f, 0.1f));

        yield return StartCoroutine(ScaleOverTime(0.7f, 0.1f));

        isTapScaling = false; // ���Ų�����ɣ����ñ�־Ϊfalse
    }//�������ŵ�Э��
    IEnumerator ScaleOverTime(float targetScale, float duration)
    {
        float startTime = Time.time;
        Vector3 originalScale = transform.localScale;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            transform.localScale = Vector3.Lerp(originalScale, originalScale * targetScale, t);
            yield return null;
        }

        transform.localScale = originalScale * targetScale;
    }//�������Ŷ�����Э��
}
