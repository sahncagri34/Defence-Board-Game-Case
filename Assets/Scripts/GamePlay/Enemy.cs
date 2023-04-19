using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyData currentEnemyData;
    private bool canMove = false;
    private float currentHealth;
    public event Action<Enemy> OnDestroyed;

    private void Awake()
    {
        EventsHandler.OnGameFinished += EventsHandler_OnGameFinished;
    }
    public void Set(EnemyData enemyData, Vector3 spawnPos)
    {
        canMove = true;

        this.currentEnemyData = enemyData;
        transform.position = spawnPos;
        currentHealth = enemyData.Health;

    }

    private void Update()
    {
        if (!canMove)
            return;

        transform.position += Vector3.down * currentEnemyData.Speed * Time.deltaTime;
    }
    public bool Compare(EnemyType enemyType)
    {
        if (currentEnemyData == null)
            return false;

        return currentEnemyData.EnemyType == enemyType;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DefencerBase"))
        {
            EventsHandler.InvokeOnGameFinished();

            Reset();
        }
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Defencer"))
        {
            OnDestroyed?.Invoke(this);
            Reset();
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            OnDestroyed?.Invoke(this);
            Reset();
        }
    }

    private void Reset()
    {
        canMove = false;
        transform.position = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void EventsHandler_OnGameFinished()
    {
        Reset();
    }
    private void OnDestroy() {
        EventsHandler.OnGameFinished -= EventsHandler_OnGameFinished;
    }
}
