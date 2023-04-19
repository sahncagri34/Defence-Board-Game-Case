using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public event Action<Vector3> OnFoundEnemy;
    public event Action OnCrashedEnemy;
    private bool isEnemyInTrigger = false;
    private float timer = 0.0f;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Enemy")) 
        {
            Vector3 direction = other.transform.position - transform.position;
            direction.Normalize();
            OnFoundEnemy?.Invoke(direction);

            isEnemyInTrigger = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.CompareTag("Enemy"))
        {
            if (!isEnemyInTrigger)
            {
                isEnemyInTrigger = true;
                timer = 0.0f;
            }

            timer += Time.deltaTime;
            if (timer >= Tools.DefaultEventInterval)
            {
                Vector3 direction = other.transform.position - transform.position;
                direction.Normalize();
                OnFoundEnemy?.Invoke(direction);
                timer = 0.0f;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            isEnemyInTrigger = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            OnCrashedEnemy?.Invoke();
        }
    }
    
}
