using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BetterAgent : MonoBehaviour
{
    public float maxDistance, speed, angularSpeed, acceleration;
    public Vector3 target, offset;
    NavMeshPath path;
    Rigidbody rb;

    void Start()
    {
        path = new NavMeshPath();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //Try to generate a path
        NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path);

        if (path.status != NavMeshPathStatus.PathComplete)
        {
            //Path failed, find a nearby position
            NavMeshHit hit;
            if (NavMesh.SamplePosition(target, out hit, maxDistance, NavMesh.AllAreas))
                target = hit.position;

            NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path);
        }

        Quaternion rotation = transform.rotation;

        //Look at target
        transform.LookAt(path.corners.Length > 1 ? path.corners[1] + offset : target + offset);

        //Move towards target
        rb.velocity += transform.forward * acceleration * Time.fixedDeltaTime;
        if(rb.velocity.magnitude > speed)
            rb.velocity = transform.forward * speed;

        //Rotation stuff idk I made it up (and will probably regret)
        Quaternion targetRotation = Quaternion.LookRotation((path.corners.Length > 1 ? path.corners[1] : target) + offset - transform.position);
        transform.rotation = Quaternion.RotateTowards(rotation, targetRotation, angularSpeed * Time.fixedDeltaTime);

        //Draw the NavMeshPath in the Scene view, this part can be removed if you don't want to see the path
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
        }

        Warp(transform.position);
    }

    public bool SetDestination(Vector3 pos)
    {
        //Do I even have to explain what it does
        target = pos;

        return true;
    }

    public bool Warp(Vector3 pos)
    {
        //Try to find a position close to the target position
        NavMeshHit hit;
        if (NavMesh.SamplePosition(pos, out hit, maxDistance, NavMesh.AllAreas))
        {
            //It worked, nice
            rb.MovePosition(hit.position + offset);
            return true;
        }

        //It didn't work, try increasing the maxDistance or smth
        return false;
    }
}
