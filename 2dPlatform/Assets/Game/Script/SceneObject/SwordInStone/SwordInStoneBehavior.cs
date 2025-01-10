using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInStoneBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public Object Neko;
    bool canTryToGetSword = false;

    //�����涼�����ڼ��ν��ı���
    int tapCounter = 0;
    int tapTargetCount = 10;
    bool isChecking = false;
    bool canFirstCheck = true;
    bool checkSucceeded = false;
    float getCheckTimer = 0f;
    float getCheckDuration = 2.0f;
    float giveUpCheckTimer = 0f;
    float giveUpCheckDuration = 0.5f;



    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        //ColliderCheck();
    }

    // Update is called once per frame
    void Update()
    {
        TryToGetSword();
    }



    void TryToGetSword()
    {
        if (canTryToGetSword)
        {
            HintKeyAppear();
            TapKeyCheck();
        }
    }
    void HintKeyAppear()
    {

    }//������ʾͼ�꣨δ��ɣ�


    /// <summary>
    /// ����Ƿ��ڰν�������
    /// </summary>
    void TapKeyCheck()
    {
        if (Input.GetKeyDown(KeyCode.J) && canFirstCheck && !checkSucceeded)
        {
            isChecking = true;
            canFirstCheck = false;
            Debug.Log("�״μ��");
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
        //checkTimer = Time.deltaTime;
        getCheckTimer += Time.deltaTime;
        if (getCheckTimer >= getCheckDuration)//�������ʱ������ʧ��
        {
            FailToGetSword();
        }
    }//�ν���ʱ��
    void GetCountCheck()
    {
        //GiveUpGetCheck();
        Debug.Log("�������");
        if (Input.GetKeyDown(KeyCode.J) && !canFirstCheck)
        {
            tapCounter++;
            //Debug.Log($"{tapCounter}");
        }
        if (tapCounter >= tapTargetCount)//����ɹ��ﵽ�˴���
        {
            Debug.Log("succeed to get sword");
            SwordIsGot();
            canFirstCheck = false;
            checkSucceeded = true;
        }
    }//�ν�������
    void GiveUpGetCheck()
    {
        ///
        ///��δ��ɣ��߼����󣬻�������
        ///
        giveUpCheckTimer += Time.deltaTime;
        if (giveUpCheckTimer >= giveUpCheckDuration)//�������ʱ������ʧ��
        {
            FailToGetSword();
        }
    }//�ν����ּ�ʱ����δ��ɣ�
    void IsGettingSword()
    {
        animator.SetBool("isGetting", true);
    }//�ν���
    void FailToGetSword()
    {
        //��ô���˳����״̬�����ҿ������½��е�һ�μ��
        getCheckTimer = 0;
        tapCounter = 0;
        isChecking = false;
        canFirstCheck = true;
        animator.SetBool("isGetting", false);
        Debug.Log("fail to get sword");
    }//�ν�ʧ��
    void SwordIsGot()
    {
        animator.SetTrigger("got");
    }//�ν��ɹ�


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
        if (other.gameObject == Neko)
        {
            canTryToGetSword = true;
            Debug.Log("canGetSword is: " + canTryToGetSword);
        }
    }//����
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Neko)
        {
            canTryToGetSword = false;
            Debug.Log("canGetSword is:" + canTryToGetSword);
        }
    }//�˳�

}
