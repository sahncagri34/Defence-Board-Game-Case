using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : SingletonBehaviour<BulletPool>
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private int poolSize = 10;
    private List<Bullet> bullets = new List<Bullet>();

    public override void Awake()
    {
        base.Awake();
        Initialize();
    }
    private void Initialize()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Bullet bullet = SpawnBullet();
            bullet.gameObject.SetActive(false);
        }
    }
    public Bullet GetBullet()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].gameObject.activeInHierarchy)
            {
                bullets[i].gameObject.SetActive(true);
                return bullets[i];
            }
        }
        return SpawnBullet();
    }

    private Bullet SpawnBullet()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform);
        bullets.Add(bullet);
        return bullet;
    }
}
