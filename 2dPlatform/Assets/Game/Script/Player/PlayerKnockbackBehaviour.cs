﻿using System.Collections;
using UnityEngine;

/// <summary>
/// 负责叠在玩家主动的移动之上的一层水平位移，适用于实现击退、攻击自带的垫步等
/// </summary>
public class PlayerKnockbackBehaviour : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public float dec;
    float _speed;
    Vector3 _dir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void KnockBackToRight(float speed)
    {
        KnockBack(new Vector3(1, 0.1f, 0), speed);
    }

    public void KnockBackToLeft(float speed)
    {
        KnockBack(new Vector3(-1, 0.1f, 0), speed);
    }

    public void KnockBack(Vector3 dir, float speed)
    {
        Debug.Log("KnockBack " + dir.x);
        _dir = dir;
        _speed = speed;
    }

    void Update()
    {
        if (_speed <= 0)
            return;
        if (rb.isKinematic)
            return;

        var dt = Time.deltaTime;// com.GameTime.deltaTime
        rb.MovePosition(_dir * _speed * dt);
        _speed -= dec * dt;
    }
}