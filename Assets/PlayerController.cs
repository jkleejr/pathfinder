using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class PlayerController : MonoBehaviour
{
    public Camera cam;

    public NavMeshAgent agent;

    public ThirdPersonCharacter character;

    void Start()
    {
        agent.updateRotation = false; // bc rotation is handled by the character
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // gets mouse position
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            // convert to ray and store it 
            RaycastHit hit;
            // shoot ray
            if (Physics.Raycast(ray, out hit))
            {
                // move agent
                agent.SetDestination(hit.point);
            } 
        }
        // destination is determined by agent
        // call desiredvelocity, whwere our agent wants to move
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false); // stop moving
        }
    }
}

