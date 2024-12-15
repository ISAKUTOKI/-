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
        Debug.Log("canDebugLog");
        Debug.Log(WPS.power);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        Debug.Log(WPS.power);

        if (WPS.power > canBougyoValue)
        {
            canBougyo = true;
            //Debug.Log("canbougyo");
        }
        else
        {
            canBougyo = false;
            //Debug.Log("canNotBougyo");
        }



        //����K���ҿ��Է������Ҳ��ڷ����Ǿͽ������
        if (Input.GetKeyDown(KeyCode.K) && canBougyo && inBougyo == false)
        {
            //��������
            PlayerBehaviour.instance.animator.SetTrigger("bougyo_start");
            //debug��ʼ����
            //Debug.Log("bougyo start");
            //������
            inBougyo = true;
        }

        //�ڷ�����
        if (inBougyo == true && Input.GetKeyDown(KeyCode.K) == false)
        {
            PlayerBehaviour.instance.animator.SetTrigger("bougyo_out");
            //Debug.Log("���");
        }

    }


}
