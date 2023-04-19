using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defencer : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private BoxCollider2D defencerCollider;
    private DefencerData currentDefencerData;
    private Radar radar;
    private bool isEnemyFound = false;
    private float currentInterval = 0f;
    private Vector3 lastShootDirection;

    private void Awake() {
        radar = GetComponent<Radar>();

        radar.OnFoundEnemy += Radar_OnFoundEnemy;
        radar.OnCrashedEnemy += Radar_OnCrashedEnemy;
        EventsHandler.OnGameFinished += EventsHandler_OnGameFinished;
    }

    private void Update() 
    {
        currentInterval += Time.deltaTime;
        if (isEnemyFound)
        {
            if (currentInterval >= currentDefencerData.Interval)
            {
                currentInterval = 0f;
                isEnemyFound = false;
                Shoot(lastShootDirection);
            }
        }
    }
    private void OnDestroy() {
        radar.OnFoundEnemy -= Radar_OnFoundEnemy;
        radar.OnCrashedEnemy -= Radar_OnCrashedEnemy;
        EventsHandler.OnGameFinished -= EventsHandler_OnGameFinished;
    }

    public void Set(DefencerData defencerData)
    {
        this.currentDefencerData = defencerData;
        SetColliderRange(defencerData.Range);
    }

    private void SetColliderRange(float range)
    {
        float colliderWidth = 0f;
        float colliderHeight = 0f;
        float colliderX = 0f;
        float colliderY = 0f;

        if (currentDefencerData.Directions.HasFlag(Directions.Forward))
        {
            colliderHeight += range;
            colliderY += range / 2;
        }
        if (currentDefencerData.Directions.HasFlag(Directions.Backward))
        {
            colliderHeight += range;
            colliderY -= range / 2;
        }
        if (currentDefencerData.Directions.HasFlag(Directions.Left))
        {
            colliderWidth += range;
            colliderX -= range / 2;
        }
        if (currentDefencerData.Directions.HasFlag(Directions.Right))
        {
            colliderWidth += range;
            colliderX += range / 2;
        }
        colliderWidth = Mathf.Max(colliderWidth, 1f);
        defencerCollider.size = new Vector2(colliderWidth, colliderHeight);
        defencerCollider.offset = new Vector2(colliderX, colliderY);
    }

    private void Radar_OnFoundEnemy(Vector3 direction)
    {
        lastShootDirection = direction;   
        isEnemyFound = true;
    }
    private void Radar_OnCrashedEnemy()
    {
        isEnemyFound = false;
       Destroy();
    }
 

    private void Shoot(Vector3 direction)
    {
        Bullet bullet = BulletPool.Instance.GetBullet();
        bullet.Set(direction, currentDefencerData.Damage);
        bullet.transform.position = transform.position;

        lastShootDirection = Vector3.zero;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void EventsHandler_OnGameFinished()
    {
       Destroy ();
    }
}
