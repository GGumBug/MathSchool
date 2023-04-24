using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackRange : MonoBehaviour
{
    public Action   startAttackEvent;
    public Action   stopAttackEvent;

    private int     areaEnemyCount = 0;

    public BoxCollider2D AttackRangeCol { get; private set; }
    

    private void Start()
    {
        AttackRangeCol = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (areaEnemyCount == 0)
            {
                startAttackEvent();
            }
            areaEnemyCount++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            areaEnemyCount--;
            if (areaEnemyCount == 0)
            {
                stopAttackEvent();
            }
        }
    }
}
