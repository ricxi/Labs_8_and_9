using UnityEngine;

public class MinimapController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z);
    }
}
