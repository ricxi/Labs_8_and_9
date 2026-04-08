using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform gunpoint;
    [SerializeField] private float projectileForce = 0f;

    private InputAction _fire;

    private void Awake()
    {
        _fire = InputSystem.actions.FindAction("Player/Attack");
    }

    private void OnEnable()
    {
        _fire.started += FirePooledBullet;
    }

    private void OnDisable()
    {
        _fire.started -= FirePooledBullet;
    }

    private void FirePooledBullet(InputAction.CallbackContext context)
    {
        Bullet bullet = BulletObjectPool.Instance.Get();
        bullet.transform.SetPositionAndRotation(gunpoint.position, gunpoint.rotation);
        bullet.gameObject.SetActive(true);
        bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * projectileForce, ForceMode.Impulse);
    }
}
