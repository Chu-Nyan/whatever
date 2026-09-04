using Chunity.Core;
using System;
using UnityEngine;
using UnityEngine.AI;
using Whatever.ObjectControl;

public class Character : MonoBehaviour, ISceneObject, IMovementReceiver
{
    private int _instanceID;

    [SerializeField]
    private NavMeshAgent _nav;

    private event Action<Character> Deactivated;

    public int InstanceID
    {
        get => _instanceID;
    }

    public int ReceiverID
    {
        get => _instanceID;
    }

    private void Awake()
    {
        _instanceID = IDGenerator.Next();
        if (NavMesh.SamplePosition(transform.position, out var hit, 2f, NavMesh.AllAreas))
            transform.position = hit.position;

        _nav.enabled = true;
    }

    public void SetDestination(Vector3 destination)
    {
        _nav.SetDestination(destination);
    }

    public void SetPositionWithNavMash(Vector3 position)
    {
        if (NavMesh.SamplePosition(position, out var hit, 2f, NavMesh.AllAreas) == true)
        {
            _nav.Warp(hit.position);
        }
    }

    public void StopMovement()
    {
        _nav.ResetPath();
    }

    public void SetActive(bool value)
    {
        if (gameObject.activeSelf == value)
            return;

        if (value == false)
        {
            Deactivated?.Invoke(this);
            Deactivated = null;
        }

        _nav.enabled = false;
        gameObject.SetActive(value);
    }

    public void RegisterDeactivated(Action<Character> callback)
    {
        Deactivated += callback;
    }
}
