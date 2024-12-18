using Assets.Game.Script.HUD_interface.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBougyo : MonoBehaviour
{
    // Start is called before the first frame update

    public WeaponPowerSystem WPS;
    public deflectArea deflectArea;
    public float canBougyoValue = 0.1f;
    private bool canBougyo = false;
    private bool inBougyo = false;
    private bool canDeflect = false;

    void Start()
    {
        //Debug.Log(WPS.power);
    }

    // Update is called once per frame
    void Update()
    {
        //�����ܷ������״̬
        CanBougyoSwitch();

        //��������
        BougyoCheck();

    }

    void CanBougyoSwitch()
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
            if (canBougyo && !inBougyo)
            {
                PlayerBehaviour.instance.animator.SetTrigger("bougyo_start");
                PlayerBehaviour.instance.animator.SetBool("bougyo_cyuu", true);
                //Debug.Log("bougyo start");
                inBougyo = true;
            }
            //û�����ͽ�������˳�
            else if (!canBougyo)
            {
                PlayerBehaviour.instance.animator.SetTrigger("no_power_bougyo");
            }
        }
        if (Input.GetKeyUp(KeyCode.K) && canBougyo && inBougyo || inBougyo && !canBougyo)
        {
            PlayerBehaviour.instance.animator.SetTrigger("bougyo_out");
            PlayerBehaviour.instance.animator.SetBool("bougyo_cyuu", false);
            Debug.Log("bougyo out");
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
        CheckForDeflection();
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
    private void CheckForDeflection()
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
