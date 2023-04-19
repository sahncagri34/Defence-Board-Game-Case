using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 3f;
    private Vector3 direction;
    private float lifeTime = 6f;
    private float currentLifeTime = 0f;
    private bool canMove = false;
    private float damage = 0f;
    private void Update()
    {
        if (canMove)
        {
            transform.position += direction * speed * Time.deltaTime;
            currentLifeTime += Time.deltaTime;
            if (currentLifeTime >= lifeTime)
            {
                Reset();
            }
        }
    }

    public void Set(Vector3 direction, float damage)
    {
        canMove = true;
        currentLifeTime = 0f;
        this.damage = damage;
        this.direction = direction;

        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Reset();
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
    private void Reset()
    {
        canMove = false;
        gameObject.SetActive(false);
        transform.position = Vector3.zero;
    }
}
