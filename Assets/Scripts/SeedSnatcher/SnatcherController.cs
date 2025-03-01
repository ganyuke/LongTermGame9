using System.Collections.Generic;
using SeedSnatcher.Movement;
using UnityEngine;

namespace SeedSnatcher
{
    public enum SnatcherState
    {
        Idle,
        Diving
    }
    
    public class SnatcherController : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private string targetTag = "ExpiredSeed";
        private ISnatcherMovement snatcherMovement;
        private SnatcherState snatcherState;
        private Dictionary<SnatcherState, ISnatcherMovement> snatcherMovements;
        
        private void SetTarget(GameObject newTarget)
        {
            target = newTarget;
        }

        // Find seed GameObject that have been on the ground too long 
        private void FindTarget()
        {
            var possibleTarget = GameObject.Find(targetTag);
            // TODO: check whether already targeted by another snatcher
            SetTarget(possibleTarget);
        }

        private bool HasTarget()
        {
            return (bool)target;
        }

        private void SetState(SnatcherState newState, Vector3 targetPosition)
        {
            snatcherState = newState;
            snatcherMovements.TryGetValue(newState, out snatcherMovement);
            snatcherMovement?.Init(transform.position, targetPosition);
        }

        private void Start()
        {
            snatcherMovements = new Dictionary<SnatcherState, ISnatcherMovement>()
            {
                { SnatcherState.Idle, GetComponent<SnatcherIdle>()},
                { SnatcherState.Diving, GetComponent<SnatcherDive>()}
            };
            
            SetState(SnatcherState.Idle, transform.position + new Vector3(-10, 0, 0));
        }

        private void Update()
        {
            if (HasTarget() && snatcherState != SnatcherState.Diving)
            {
                SetState(SnatcherState.Diving, target.transform.position);
            } else if (!HasTarget() && snatcherState != SnatcherState.Idle)
            {
                SetState(SnatcherState.Idle, transform.position + new Vector3(-10, 0, 0));
            }
            snatcherMovement.Loop();
        }
    }
}