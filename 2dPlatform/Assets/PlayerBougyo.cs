using Assets.Game.Script.HUD_interface.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBougyo : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public WeaponPowerSystem WPS;
    public deflectArea deflectArea;
    public float canBougyoValue = 0.15f;
    private bool canBougyo = false;
    public bool inBougyo = false;
    private bool canDeflect = false;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //�����ܷ������״̬
        CanBougyoCheck();

        //��������
        BougyoCheck();

    }

    void CanBougyoCheck()
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

        //����k�ͽ��������ʼ����
        if (Input.GetKeyDown(KeyCode.K))
        {
            PlayerBehaviour.instance.animator.SetTrigger("bougyo_start");
            //�������ͽ����������
            if (Input.GetKey(KeyCode.K)&& canBougyo && !inBougyo)
            {
                PlayerBehaviour.instance.animator.SetTrigger("bougyo_start");
                PlayerBehaviour.instance.animator.SetBool("bougyo_cyuu", true);
                //Debug.Log("bougyo start");
                inBougyo = true;
            }
            //û�����ͽ�������˳�
            else
            {
                PlayerBehaviour.instance.animator.SetTrigger("bougyo_out");
                Debug.Log("bougyo out");
            }
        }

        if (Input.GetKeyUp(KeyCode.K) && canBougyo && inBougyo || inBougyo && !canBougyo)
        {
            PlayerBehaviour.instance.animator.SetBool("bougyo_cyuu", false);

            inBougyo = false;
        }
    }

    ///����ϵͳ
        //��bougyo_start�����Ķ����¼�DeflectCheckStart��DeflectCheckEnd���м�
        //�����������canBeDeflected�Ĺ������͵���
        //�����pass
        //�����������ڵ�ǰ֡ͣ�٣����Ҳ��뵯�����˵���Ч
        //���û��Ӧ�ͼ������з�������
        //����а��¹�������ֱ����deflect_to_attack��Trigger���빥������

    public void DeflectCheckStart()
    {
        // ����Ƿ���Ե���
        CheckForDeflect();
    }

    // �����¼���������鵯��
    public void DeflectCheckEnd()
    {
        // ������Ե�����ִ�е����߼�
        if (canDeflect)
        {
            // �����ڵ�ǰ֡ͣ�٣������뵯����Ч
            PlayerBehaviour.instance.animator.SetTrigger("deflect");
            // ���ŵ�����Ч
            // ���磺Instantiate(deflectEffectPrefab, deflectEffectPosition, Quaternion.identity);
        }
        else
        {
            // ���û�е����ɹ�������ִ�з�������
            // ���������룬��Ϊ���������Ѿ���Animator������
        }
    }

    // ����Ƿ���Ե���
    private void CheckForDeflect()
    {
        // ������Ӽ�鵯�����߼�
        // ���磺����Ƿ��е��˹����������ʱ��ƥ��
        // ��������ɹ������� canDeflect Ϊ true
        // ע�⣺������߼���Ҫ���������Ϸʵ���������д


        //foreach (var p in deflectArea.targets)
        //{
        //    if (p == null)
        //    {
        //        continue;
        //    }
        //    p.Reverse();
        //}
    }

    // ��������빥������
    public void DeflectToAttack()
    {
        // ������¹�������ʹ�� deflect_to_attack �� Trigger ���빥������
        if (Input.GetKeyDown(KeyCode.J))
        {
            PlayerBehaviour.instance.animator.SetTrigger("deflect_to_attack");
        }
    }


}
