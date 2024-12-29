using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectAreaBehaviour : MonoBehaviour
{
    private List<Transform> hitSources;

    private void OnEnable()
    {
        hitSources = new List<Transform>();
    }

    private void OnDisable()
    {
        hitSources = new List<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.LogWarning("DeflectAreaBehaviour OnTriggerEnter2D " + collision.gameObject);
        var ene = collision.GetComponent<EnemyBehaviour>();
        if (ene != null)
        {
            if (!hitSources.Contains(ene.transform))
            {
                hitSources.Add(ene.transform);
            }
        }
    }

    /// <summary>
    /// ���յ��ܵ��˺��Ĵ����������ܵĲ������˺���Դ�����ܵ������
    /// 1 �������壬����ʷ��ķ�Ľ�ս�������õ����������˵�transform�жϵ���
    /// 2 ����������projectile��Ҳ����Զ�̹������õ��ǵ��˵��ӵ���transform�жϵ���
    /// 3 ���˵����岿�֣��������è���˵�β�͹������õ�����������ض��Ĺ����ж����transform�жϵ���
    /// </summary>
    /// <param name="hitSource"></param>
    public bool InAreaCheck(Transform hitSource)
    {
        return hitSources.Contains(hitSource);
    }
}
