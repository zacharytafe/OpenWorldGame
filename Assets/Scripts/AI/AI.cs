using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public bool hasTarget;
    [ShowIf("hasTarget")]public Transform target;

    public float maxVelocity = 15f, maxDistance = 10f;

    [Expandable] public SteeringBehaviour[] behaviours;
    public NavMeshAgent agent;
    private Vector3 velocity;

    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 desiredPosition = transform.position + velocity * Time.deltaTime;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(desiredPosition, .1f);

        // Render all behaviours
        foreach (var behaviour in behaviours)
        {
            behaviour.OnDrawGizmosSelected(this);
        }
    }

    private void Update()
    {
        // Step1). Loop through all behaviours and get forces
        velocity = Vector3.zero;

        // Step 2). Limit force to velocity
        foreach (var behaviour in behaviours)
        {
            // Apply normalised force to force
            float percentage = maxVelocity * behaviour.weighting;
            velocity += behaviour.GetForce(this) * percentage;
        }
        // Step 3). Limit velocity to max velocity
        velocity = Vector3.ClampMagnitude(velocity, maxVelocity);

        // Step 4). Apply velocity to NavMeshAgent destination
        Vector3 desiredPosition = transform.position + velocity * Time.deltaTime;
        NavMeshHit hit;
        // Check if desired position is within navMesh
        if(NavMesh.SamplePosition(desiredPosition, out hit, maxDistance, -1))
        {
            // Set agents destination to hit point
            agent.SetDestination(desiredPosition);
        }
        
    }
}
