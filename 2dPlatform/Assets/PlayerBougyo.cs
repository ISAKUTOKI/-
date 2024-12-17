using Assets.Game.Script.HUD_interface.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBougyo : MonoBehaviour
{
    // Start is called before the first frame update

    public WeaponPowerSystem WPS;
    public float canBougyoValue = 0.1f;
    private bool canBougyo = false;
    private bool inBougyo = false;

    void Start()
    {
        //Debug.Log(WPS.power);
    }

    // Update is called once per frame
    void Update()
    {
        //�����ܷ������״̬
        CanBougyo();

        //��������
        BougyoCheck();

        if (Input.GetKeyDown(KeyCode.F12))
            Debug.Log(WPS.power);
    }

    void CanBougyo()
    {
        if (WPS.power >= canBougyoValue)
        {
            canBougyo = true;
        }
        else
        {
            canBougyo = false;
        }
    }

    void BougyoCheck()
    {
        //����K���ҿ��Է������Ҳ��ڷ����Ǿͽ������
        if (Input.GetKeyDown(KeyCode.K) && canBougyo && inBougyo == false)
        {
            PlayerBehaviour.instance.animator.SetTrigger("bougyo_start");
            PlayerBehaviour.instance.animator.SetBool("bougyo_cyuu", true);
            //debug��ʼ����
            Debug.Log("bougyo start");
            //������
            inBougyo = true;
        }

        //�ڷ����в����ɿ���k����ֹͣ����
        if (Input.GetKeyUp(KeyCode.K) && canBougyo && inBougyo == true || canBougyo == false)
        {
            PlayerBehaviour.instance.animator.SetTrigger("bougyo_out");
            PlayerBehaviour.instance.animator.SetBool("bougyo_cyuu", false);
            Debug.Log("bougyo out");
            inBougyo = false;
        }

    }

    ///����ϵͳ
    //��bougyo_start�׶Σ������������canDeflect�Ĺ������͵���
    //�����������ڵ�ǰ֡ͣ�٣����Ҳ��ŵ������˵���Ч
    //���û��Ӧ�ͼ������з�������
    //����а��¹�������ֱ�ӽ��빥������



}
