using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

// Gustaf Ybring och Patric Wåhlin

// The agent
public class PlatformAgent : Agent
{
    public GameObject spawnArea;
    public GameObject area;
    public float speed;
    public float rotationSpeed;
    PlatformArea m_MyArea;
    Rigidbody m_AgentRb;

    // Runs on startup, retrieves the rigidbody and area components.
    public override void Initialize()
    {
        m_AgentRb = GetComponent<Rigidbody>();
        m_MyArea = area.GetComponent<PlatformArea>();
    }

    // Collects observations, in this case the agents local direction.
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.InverseTransformDirection(m_AgentRb.velocity));

    }

    // Moves the agent depending on its decided action.
    public void MoveAgent(float[] act)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var action = Mathf.FloorToInt(act[0]);
        switch (action)
        {
            case 1:
                dirToGo = transform.forward * 1f;
                break;
            case 2:
                dirToGo = transform.forward * -1f;
                break;
            case 3:
                rotateDir = transform.up * 1f;
                break;
            case 4:
                rotateDir = transform.up * -1f;
                break;
        }
        transform.Rotate(rotateDir, Time.deltaTime * rotationSpeed);
        m_AgentRb.AddForce(dirToGo * speed, ForceMode.VelocityChange);
    }

    // Incoming actions, adds a negative reward everytime its called to push the agent to minimize its total amount of actions.
    public override void OnActionReceived(float[] vectorAction)
    {
        AddReward(-0.0002f);
        MoveAgent(vectorAction);
    }

    // Used to manually control the agent to test the environment.
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0;
        if (Input.GetKey(KeyCode.D))
        {
            actionsOut[0] = 3;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            actionsOut[0] = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            actionsOut[0] = 4;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            actionsOut[0] = 2;
        }
    }

    // Called initially and everytime the current episode ends because the agent collided with either the goal or a obstacle.
    // Resets the agent position and the area it acts in.
    public override void OnEpisodeBegin()
    {
        ResetAgent();
        m_MyArea.ResetArea();
    }

    // Resets the agents position and velocity.
    private void ResetAgent()
    {
        transform.position = spawnArea.transform.position;
        transform.rotation = spawnArea.transform.rotation;
        m_AgentRb.velocity = Vector3.zero;
    }

    // Checks if the agent has reached the goal (object with "correct" tag) or a obstacle (object with "wrong" tag).
    // Rewards the agent respectively and ends the episode.
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("correct"))
        {
            SetReward(2f);
            EndEpisode();
        }
        else if (collision.gameObject.CompareTag("wrong"))
        {
            SetReward(-0.1f);
            EndEpisode();
        }
    }

}
