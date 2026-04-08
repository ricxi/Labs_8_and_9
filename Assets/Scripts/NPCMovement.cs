using UnityEngine;
using KBCore.Refs;
using UnityEngine.AI;
using System.Collections.Generic;

[System.Serializable]
public enum NPCStates
{
    Patrol, Chase
}

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovement : MonoBehaviour
{
    [SerializeField, Self] private NavMeshAgent agent;
    [SerializeField] private List<GameObject> waypoints;
    [SerializeField] private NPCStates currentState;
    [SerializeField] private Transform player;

    private Vector3 _destination;
    private int _index;

    private void OnValidate() => this.ValidateRefs();

    private void Start()
    {
        currentState = NPCStates.Patrol;

        if (waypoints.Count < 0)
        {
            Debug.Log("Assign waypoints to NPC");
            return;
        }

        agent.destination = _destination = waypoints[_index].transform.position;
    }

    private void Update()
    {
        switch (currentState)
        {
            case NPCStates.Patrol:
                if (waypoints.Count < 0) return;
                if (Vector3.Distance(transform.position, _destination) < 3f)
                {
                    _index = (_index + 1) % waypoints.Count;
                    _destination = waypoints[_index].transform.position;
                    agent.destination = _destination;
                }
            break;
            case NPCStates.Chase:
                agent.destination = player.position;
            break;
            default:
            break;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            currentState = NPCStates.Chase;
            player = collider.transform;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            currentState = NPCStates.Patrol;
            agent.destination = _destination;
        }
    }
}
