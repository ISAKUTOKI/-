using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeowbodyHealthBehaviour : MonoBehaviour
{
    public Animator animator;

    // Ѫ������
    public float maxHealth = 100f; // ���Ѫ��
    private float currentHealth; // ��ǰѪ��

    [HideInInspector]public bool isHurt;


    void Start()
    {
        isHurt = false;
        // ��ʼ��Ѫ��
        currentHealth = maxHealth;
    }

    // �ܵ��˺��ķ���
    public void TakeDamage(float damageAmount)
    {
        if (currentHealth <= 0) return; // ����Ѿ����������ٴ����˺�

        isHurt=true;
        // ����Ѫ��
        currentHealth -= damageAmount;
        animator.SetTrigger("hurt");
        Debug.Log(gameObject.name + " took " + damageAmount + " damage! Current health: " + currentHealth);

        // ����Ƿ�����
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // �����߼�
    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");

        // ���ٵ��˶��󣨿��Ը��������滻Ϊ�������������ȣ�
        animator.SetTrigger("die");
    }

    // ��ȡ��ǰѪ������ѡ��
    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    // ����Ѫ������ѡ��
    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }
}
