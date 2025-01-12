using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwordInStoneBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public GameObject Neko;
    public Camera Camera;
    public SIS_JKey JKey;
    bool canTryToGetSword = false;

    //����Ϊ���ڼ��ν��ı���
    int tapCounter = 0;
    int tapTargetCount = 13;//��Ҫ���µĴ���
    bool isChecking = false;
    bool canFirstCheck = true;
    bool checkSucceeded = false;
    float getCheckTimer = 0f;
    float getCheckDuration = 2.0f;//�涨ʱ����

    //����Ϊ����ñ���
    bool cameraCanBackToInitial = false;
    float cameraInitialSize;
    float cameraCurrentSize;
    //float cameraScaleWaitTimer = 0f;
    //float cameraScaleWaitDuration = 1.5f;


    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        CameraInitialSet();
        //ColliderCheck();
    }
    void Update()
    {
        if (canTryToGetSword)
            TapKeyCheck();
    }

    /// <summary>
    /// ���Neko�Ƿ������ײ��
    /// </summary>
    void ColliderCheck()
    {
        Collider2D collider = GetComponent<BoxCollider2D>();
        if (collider != null)
            Debug.Log("collider " + collider + " is got");
        else
            Debug.Log("FAILED");
    }//����Ƿ����
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == Neko && !checkSucceeded)
        {
            canTryToGetSword = true;
            JKey.seeable = true;
            JKey.canSelfRotate = true;
            JKey.canSelfScale = true;
            //Debug.Log("canGetSword is: " + canTryToGetSword);
        }
    }//����
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Neko)
        {
            canTryToGetSword = false;
            JKey.seeable = false;
            JKey.canSelfRotate = false;
            JKey.canSelfScale = false;
        }
    }//�˳�

    /// <summary>
    /// ����Ƿ��ڰν�������
    /// </summary>
    void TapKeyCheck()
    {
        if (Input.GetKeyDown(KeyCode.J) && canFirstCheck && !checkSucceeded)
        {
            isChecking = true;
            canFirstCheck = false;
            //Debug.Log("�״μ��");
        }//��һ�ΰ���J�����ҿ��Խ��е�һ�μ�飬����Կ�ʼ������������Ҳ����ٽ��е�һ�μ��
        if (isChecking)
        {
            IsGettingSword();
            GetTimeCheck();
            GetCountCheck();
            //Debug.Log("isChecking is:" + isChecking);
        }//��������ڽ��м�飬��ô�͵��ü�ʱ���ͼ�����
    }//����Ƿ�ν�
    void GetTimeCheck()
    {
        getCheckTimer += Time.deltaTime;
        if (getCheckTimer >= getCheckDuration)//�������ʱ������ʧ��
        {
            FailToGetSword();
        }
    }//�ν���ʱ��
    void GetCountCheck()
    {
        //Debug.Log("aaaaaaaaaaaaaaaaaaaa" + tapCounter);
        //GiveUpGetCheck();
        //Debug.Log("�������");
        if (Input.GetKeyDown(KeyCode.J) && !canFirstCheck)
        {
            tapCounter++;
            //GiveUpToGetCheck();
            //Debug.Log("aaaaaaaaaaaaaaaaaaaa"+tapCounter);
        }
        if (tapCounter >= tapTargetCount)//����ɹ��ﵽ�˴���
        {
            SwordIsGot();
        }
    }//�ν�������
    void IsGettingSword()
    {
        cameraCanBackToInitial = false;
        animator.SetBool("isGetting", true);
        CameraTapScale();
        JKey.canSelfRotate = false;
        JKey.canSelfScale = false;
        JKey.isTappingKey = true;
    }//�ν���
    void FailToGetSword()
    {
        //��ô���˳����״̬����������״̬
        //giveUpCheckTimer = 0;
        cameraCanBackToInitial = true;
        getCheckTimer = 0;
        tapCounter = 0;
        isChecking = false;
        animator.SetBool("isGetting", false);
        //Debug.Log("ʧ��");
        JKey.canSelfRotate = true;
        JKey.canSelfScale = true;
        JKey.isTappingKey = false;
        StartCoroutine(CameraTryBackToInitialCoroutine());
        StartCoroutine(FailedTimerCoroutine(0.5f));
    }//�ν�ʧ��
    IEnumerator FailedTimerCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canFirstCheck = true;
    }//�ȴ�һС������ٴΰν�
    void SwordIsGot()
    {
        canFirstCheck = false;
        cameraCanBackToInitial = true;
        checkSucceeded = true;
        animator.SetTrigger("got");
        //Debug.Log("�γ�����");
        JKey.seeable = false;
        StartCoroutine(CameraTryBackToInitialCoroutine());
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }//�ν��ɹ�

    /// <summary>
    /// ��������Ч��
    /// </summary>
    void CameraInitialSet()
    {
        cameraInitialSize = Camera.orthographicSize;
        cameraCurrentSize = cameraInitialSize;
    }//�����ֵ��ʼ��
    void CameraTapScale()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(CameraTapScaleCoroutine(0));
        }
    }//���°�����ʱ�����һ�ξ�ͷ����
    IEnumerator CameraTapScaleCoroutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        cameraCurrentSize = cameraCurrentSize * 0.97f;
        Camera.orthographicSize = cameraCurrentSize;
    }
    IEnumerator CameraTryBackToInitialCoroutine()
    {
        //Debug.Log("�������ʼ�ߴ�Ϊ " + cameraInitialSize);
        if (cameraCanBackToInitial && checkSucceeded)
        {
            yield return new WaitForSeconds(1.5f);
            Camera.orthographicSize = cameraInitialSize;
            cameraCurrentSize = cameraInitialSize;
            //Debug.Log("�ɹ�");
            //Debug.Log("�������ǰ�ߴ�Ϊ " + Camera.orthographicSize);
        }//�ɹ������
        if (cameraCanBackToInitial && !checkSucceeded)
        {
            yield return new WaitForSeconds(0);
            Camera.orthographicSize = cameraInitialSize;
            cameraCurrentSize = cameraInitialSize;
            //Debug.Log("�������ǰ�ߴ�Ϊ " + Camera.orthographicSize);
        }//ʧ�ܵ����
    }
    //void CameraTryBackToInitial()
    //{
    //    if (cameraCanBackToInitial && checkSucceeded)
    //    {
    //        cameraScaleWaitTimer += Time.deltaTime;//��ʱ��ʼ
    //        if (cameraScaleWaitTimer >= cameraScaleWaitDuration)
    //        {
    //            Camera.orthographicSize = Mathf.Lerp(Camera.orthographicSize, cameraInitialSize, Time.deltaTime * 2.0f);
    //            cameraCurrentSize = cameraInitialSize;
    //        }
    //        return;
    //    }
    //    if (cameraCanBackToInitial)
    //    {
    //        Camera.orthographicSize = Mathf.Lerp(Camera.orthographicSize, cameraInitialSize, Time.deltaTime * 1.0f);
    //        cameraCurrentSize = cameraInitialSize;
    //        //Debug.Log("������");
    //    }
    //}//��ͷ��λ
}