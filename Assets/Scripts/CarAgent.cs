using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using System;

/*
 * Agent Actions - Turn Right, Turn Left, Do Nothing
 * Total Actions - 3
 */
public class CarAgent : Agent
{

    private Rigidbody carRb;
    private Vector3 curPos, curRot;
    private GameObject target;
    private bool isColliding = false;
    public float carSpeed = 800f;
    public float carTurnSpeed = 5.0f;
    public bool isStun = true;
    public bool hasPowerUp = false;
    private float bestDistance = 30f;
    private float previousDistance = 0f;

    public void setTarget(GameObject target)
    {
        this.target = target;
    }
    public override void Initialize()
    {
        carRb = GetComponent<Rigidbody>();
        cameraFollow.Instance.AddTarget(transform);

        curPos = this.transform.position;
        curRot = this.transform.eulerAngles;
    }

    public override void OnEpisodeBegin()
    {
        bestDistance = 30f;
    }

    public override void OnActionReceived(float[] actions)
    {
        if (Mathf.FloorToInt(actions[0]) == 0)
        {
            Vector3 forward = (transform.forward * carSpeed);
            carRb.AddForce(forward);
        }
        else if (Mathf.FloorToInt(actions[0]) == 1)
        {
            transform.Rotate(Vector3.up * carTurnSpeed);
        }
        else 
        {
            transform.Rotate(Vector3.down * carTurnSpeed);
        }
        float distanceToTarget = Vector3.Distance(this.transform.position, target.transform.position);
        if (distanceToTarget < bestDistance)
        {
            AddReward(0.01f);
            ScoreScript.rewardValue = GetCumulativeReward();
            bestDistance = distanceToTarget;
            previousDistance = distanceToTarget;
        } 
        else if (distanceToTarget < previousDistance)
        { 
            AddReward(0.01f);
            ScoreScript.rewardValue = GetCumulativeReward();
            previousDistance = distanceToTarget;
        }
        else
        {
            AddReward(-0.02f);
            ScoreScript.rewardValue = GetCumulativeReward();
            previousDistance = distanceToTarget;
        }
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0f;
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow))
        {
            actionsOut[0] = 1f; 
        }
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
        {
            actionsOut[0] = 2f;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.position);
        sensor.AddObservation(target.transform.position);
    }

    void Update()
    {
        isColliding = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isColliding)
        {
            return;
        }
        isColliding = true;

        if (other.gameObject.tag == "Park")
        {
            ScoreScript.parkingScoreValue++;
            AddReward(1f);
            ScoreScript.rewardValue = GetCumulativeReward();
            EndEpisode();
        }
        if (other.gameObject.tag == "Obstacle" && !hasPowerUp)
        {
            ScoreScript.obstacleHitScoreValue++;
            AddReward(-0.1f);
            ScoreScript.rewardValue = GetCumulativeReward();
            EndEpisode();
            this.transform.position = curPos;
            this.transform.eulerAngles = curRot;
        }
        if (other.gameObject.tag == "Wall" && !hasPowerUp)
        {
            ScoreScript.wallHitScoreValue++;
            AddReward(-0.1f);
            ScoreScript.rewardValue = GetCumulativeReward();
            EndEpisode();
            this.transform.position = curPos;
            this.transform.eulerAngles = curRot;
        }
        if (other.gameObject.name == "Portal_1")
        {
            Debug.Log("Portal1");
            Vector3 offset = new Vector3(1, -1, 0.5f);
            this.transform.position = GameObject.Find("Portal_2").transform.position + offset;
            this.transform.eulerAngles = new Vector3(0, 45, 0);
        }
        if (other.gameObject.name == "Portal_2")
        {
            Debug.Log("Portal2");
            Vector3 offset = new Vector3(1, -0.5f, -1);
            this.transform.position = GameObject.Find("Portal_1").transform.position + offset;
            this.transform.eulerAngles = new Vector3(0, 135, 0);
        }
    }
}
