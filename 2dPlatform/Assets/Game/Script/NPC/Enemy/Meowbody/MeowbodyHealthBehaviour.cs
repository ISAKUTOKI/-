using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeowbodyHealthBehaviour : MonoBehaviour
{
    public MeowbodyGetComponent component;

    // Ѫ������
    public float maxHealth = 100f; // ���Ѫ��
    private float currentHealth; // ��ǰѪ��



    void Start()
    {
        // ��ʼ��Ѫ��
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            Die();
        }
    }
    // �ܵ��˺��ķ���
    public void TakeDamage(float damageAmount)
    {
        if (currentHealth <= 0) return; // ����Ѿ����������ٴ����˺�

        component.attackSystem.ResetAttackTimer();
        component.attackSystem.ResetCooldowns();
        // ����Ѫ��
        currentHealth -= damageAmount;
        component.animator.SetTrigger("hurt");
        Debug.Log(gameObject.name + " took " + damageAmount + " damage! Current health: " + currentHealth);

        // ����Ƿ�����
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // �����߼�
    public void Die()
    {
        component.animator.SetTrigger("die");
        Debug.Log(gameObject.name + " has died!");
        // ���ٵ��˶��󣨿��Ը��������滻Ϊ�������������ȣ�
        component.attackSystem.DiedAttack();
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
