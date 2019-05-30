using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

[CreateAssetMenu(fileName = "Flee", menuName = "SteeringBehaviour/Flee", order = 1)]

public class Flee : SteeringBehaviour
{
    public float stoppingDistance = 1f;

    public override void OnDrawGizmosSelected(AI owner)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(owner.target.position, stoppingDistance);
    }

    public override Vector3 GetForce(AI owner)
    {
        // Create a value to return later
        Vector3 force = Vector3.zero;

        float distance = Vector3.Distance(owner.transform.position, owner.target.position);
        if (distance < stoppingDistance)
        {
            // Modify value here...
            if (owner.hasTarget) // target != null
            {
                // Get direction from AI agent to target
                force += owner.transform.position - owner.target.position;
            }
        }

        // return value
        return force.normalized;
    }
}
