using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _timer;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            BulletObjectPool.Instance.ReturnToPool(this);
        }
    }

    private void OnEnable()
    {
        _timer = 0;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 1.5f)
        {
            BulletObjectPool.Instance.ReturnToPool(this);
        }
    }
}
