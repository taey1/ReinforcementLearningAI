using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class PlatformAgent : Agent
{
    public GameObject spawnArea;
    public GameObject area;
    public float speed;
    public float rotationSpeed;
    PlatformArea m_MyArea;
    Rigidbody m_AgentRb;
    public override void Initialize()
    {
        m_AgentRb = GetComponent<Rigidbody>();
        m_MyArea = area.GetComponent<PlatformArea>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.InverseTransformDirection(m_AgentRb.velocity));
        //sensor.AddObservation(m_MyArea.GetCorrectPlatform().transform.localPosition);
        //sensor.AddObservation(transform.localPosition);
        //sensor.AddObservation(m_AgentRb.velocity);
        //sensor.AddObservation(transform.localRotation);
        //sensor.AddObservation(m_MyArea.GetCorrectPlatform().transform.position - transform.position);
        //sensor.AddObservation(StepCount / (float)MaxStep);
    }

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

    public override void OnActionReceived(float[] vectorAction)
    {
        AddReward(-0.0002f);
        MoveAgent(vectorAction);
    }

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

    public override void OnEpisodeBegin()
    {
        ResetAgent();
        m_MyArea.ResetArea();
    }

    private void ResetAgent()
    {
        transform.position = spawnArea.transform.position;
        transform.rotation = spawnArea.transform.rotation;
        m_AgentRb.velocity = Vector3.zero;
    }

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
