﻿using com;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public HpBarFixedWidthBehaviour hpbar;
    public int hpMax;
    private int _hp;
    bool _dead;

    public string dieSound;
    public float deathFadeDelay;
    public HeadKickSlay headKickSlay;

    [HideInInspector]
    public NpcController npcController;
    [HideInInspector]
    public EnemyPatrolBehaviour patrolBehaviour;
    [HideInInspector]
    public EnemySkillBehaviour skillBehaviour;
    [HideInInspector]
    public EnemyPlayerChecker playerChecker;
    [HideInInspector]
    public Animator animator;
    public bool isBoss;

    private void Awake()
    {
        npcController = GetComponent<NpcController>();
        patrolBehaviour = GetComponent<EnemyPatrolBehaviour>();
        skillBehaviour = GetComponent<EnemySkillBehaviour>();
        playerChecker = GetComponent<EnemyPlayerChecker>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _hp = hpMax;
        if (hpbar != null)
        {
            hpbar.Set(1, true);
            hpbar.Hide();
        }
    }

    private void Update()
    {
        DoRoutineMove();
    }

    public void TakeFatalDamage()
    {
        TakeDamage(hpMax + 1);
    }

    public void TakeDamage(int dmg)
    {
        if (_dead) return;

        Debug.Log(this.name + "TakeDamage " + dmg);

        _hp -= dmg;
        if (_hp < 0)
            _hp = 0;
        float ratio = (float)_hp / hpMax;
        if (hpbar != null)
            hpbar.Set(ratio, false);
        if (_hp <= 0)
            Die();
    }

    void Die()
    {
        _dead = true;
        if (hpbar != null)
            hpbar.Hide();

        SoundSystem.instance.Play(dieSound);
        npcController.SetAnimTrigger("die");
        //npcController.myCollider.enabled = false;

        //CapsuleCollider2D col = npcController.myCollider as CapsuleCollider2D;
        //col.size = new Vector2(col.size.x * 0.25f, col.size.y * 0.25f);
        StartCoroutine(DieProcess());

        if (isBoss)
        {
            (GameFlowSystem.instance as GameFlow1).OnBossDead();
            GameFlowSystem.instance.ToggleBossHpBar(false);
        }
    }

    IEnumerator DieProcess()
    {
        yield return new WaitForSeconds(deathFadeDelay);
        npcController.myCollider.enabled = false;
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sr in srs)
            sr.DOFade(0, 2).SetDelay(Random.Range(1, 3f));

        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    void DoRoutineMove()
    {
        if (_dead) return;
        //??
    }

    public bool IsDead { get { return _dead; } }
}