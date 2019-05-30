using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Wander", menuName = "SteeringBehaviour/Wander", order = 1)]
public class Wander : SteeringBehaviour
{
    public float offset = 1.0f, radius = 1.0f, jitter = .2f;
    public bool freezeX = false, freezeY = true, freezeZ = false;

    private Vector3 targetDir, randomDir, force;

    public override void OnDrawGizmosSelected(AI owner)
    {
        Gizmos.color = Color.yellow;
        // Draw the trget direction

        Vector3 ownerPosition = owner.transform.position;
        Vector3 offsetPosition = ownerPosition + owner.transform.forward * offset;

        Gizmos.DrawLine(ownerPosition, offsetPosition);

        Gizmos.DrawWireSphere(offsetPosition, radius);
        Gizmos.DrawSphere(ownerPosition + force, 1f);

    }

    public override Vector3 GetForce(AI owner)
    {
        force = Vector3.zero;

        // float randX = Random.Range(float.MinValue, float.MaxValue) * 5f;
        // float randZ = Random.Range(float.MinValue, float.MaxValue) * 5f;
        randomDir = Random.onUnitSphere;

        if (freezeX) randomDir.x = 0;
        if (freezeY) randomDir.y = 0;
        if (freezeZ) randomDir.z = 0;

        randomDir *= jitter;

        targetDir += randomDir;

        targetDir = targetDir.normalized * radius;

        force = targetDir + owner.transform.forward.normalized * offset;      

        return force.normalized;
    }
}
