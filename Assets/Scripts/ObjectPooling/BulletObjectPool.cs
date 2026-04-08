using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletObjectPool : PersistentSingleton<BulletObjectPool>
{
    [SerializeField] private Bullet bulletPrefab;
    private Queue<Bullet> _pool = new Queue<Bullet>();

    public Bullet Get()
    {
        if (_pool.Count == 0)
        {
            AddBullet(1);
        }
        return _pool.Dequeue();
    }

    private void AddBullet(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var prefab = Instantiate(bulletPrefab);
            prefab.gameObject.SetActive(false);
            _pool.Enqueue(prefab);
        }
    }

    public void ReturnToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        _pool.Enqueue(bullet);
    }
}
