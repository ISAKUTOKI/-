using Assets.Game.Script.HUD_interface.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowbodyAttackBehaviour : MonoBehaviour
{
    public WeaponPowerSystem weaponPowerSystem;
    public PlayerHealthBehaviour NekoHealth;
    public Animator animator;
    // ���������Χ
    public float minAttackInterval = 2f;
    public float maxAttackInterval = 5f;

    // ������ȴʱ��
    public float attack1Cooldown = 3f;
    public float attack2Cooldown = 5f;

    // �������
    public Transform Neko;

    // ��ʱ��
    private float attackTimer;
    private float attack1Timer;
    private float attack2Timer;

    [HideInInspector]public bool canAttackNeko;

    void Start()
    {
        // ��ʼ����ʱ��
        ResetAttackTimer();
        ResetCooldowns();
    }

    void Update()
    {
        // ���¼�ʱ��
        attackTimer -= Time.deltaTime;
        UpdateCooldowns();

        // �����ʱ��������ִ�й���
        if (attackTimer <= 0)
        {
            PerformRandomAttack();
            ResetAttackTimer(); // ���ù��������ʱ��
        }
    }

    // ���ù��������ʱ��
    private void ResetAttackTimer()
    {
        attackTimer = Random.Range(minAttackInterval, maxAttackInterval);
    }

    // �������й�������ȴʱ��
    private void ResetCooldowns()
    {
        attack1Timer = 0;
        attack2Timer = 0;
    }

    // �������й�������ȴʱ��
    private void UpdateCooldowns()
    {
        if (attack1Timer > 0)
            attack1Timer -= Time.deltaTime;
        if (attack2Timer > 0)
            attack2Timer -= Time.deltaTime;
    }

    // ���ѡ��һ�ֹ�����ʽ��ִ��
    private void PerformRandomAttack()
    {
        // ��ȡ���õĹ�����ʽ
        int availableAttacks = GetAvailableAttacks();

        if (availableAttacks == 0)
        {
            Debug.Log("No attacks available! Waiting for cooldowns...");
            return;
        }

        // ���ѡ��һ�ֿ��õĹ�����ʽ
        int attackType = Random.Range(1, availableAttacks + 1);

        // ������λ�ò���������
        CheckPlayerPosition();

        switch (attackType)
        {
            case 1:
                Attack1();
                break;
            case 2:
                Attack2();
                break;
            default:
                Debug.LogWarning("Invalid attack type selected!");
                break;
        }
    }

    // ��ȡ��ǰ���õĹ�����ʽ����
    private int GetAvailableAttacks()
    {
        int count = 0;

        if (attack1Timer <= 0) count++;
        if (attack2Timer <= 0) count++;
        return count;
    }

    // ������λ�ò���������
    private void CheckPlayerPosition()
    {
        if (Neko == null) return;

        // �����������ڵ��˵�λ��
        Vector3 directionToPlayer = Neko.position - transform.position;

        // ���������Ҳ�
        if (directionToPlayer.x > 0)
        {
            FlipLeft();
        }
        // �����������
        else if (directionToPlayer.x < 0)
        {
            FlipRight();
        }
    }

    // ��������Ϊ�Ҳ�
    private void FlipRight()
    {
        Debug.Log("Flipping Right!");
        transform.localScale = new Vector3(1, 1, 1); // Ĭ�ϳ����Ҳࣩ
    }

    // ��������Ϊ���
    private void FlipLeft()
    {
        Debug.Log("Flipping Left!");
        transform.localScale = new Vector3(-1, 1, 1); // ˮƽ��ת����ࣩ
    }

    // ������ʽ1
    private void Attack1()
    {
        if (attack1Timer > 0)
            return; // ���������ȴ�У�������

        animator.SetTrigger("attack1");

        Debug.Log("���ڽ��� Attack 1!");
        attack1Timer = attack1Cooldown; // ������ȴʱ��
        // ������ʵ�ֹ���1�ľ����߼�
    }

    // ������ʽ2
    private void Attack2()
    {
        if (attack2Timer > 0) return; // ���������ȴ�У�������

        animator.SetTrigger("attack2");

        Debug.Log("���ڽ��� Attack 2!");
        attack2Timer = attack2Cooldown; // ������ȴʱ��
        // ������ʵ�ֹ���2�ľ����߼�
    }

    private void FastAttack()
    {
        Debug.Log("���ڽ��� FastAttack!");
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (weaponPowerSystem.power < 0.1f)
            {
                animator.SetTrigger("fastAttack");
            }
        }
    }

    [HideInInspector]
    public void AttackTakeDamage(int attackDamage)
    {
        if (canAttackNeko)
        {
            NekoHealth.TakeDamage(attackDamage);
        }
    }
}
