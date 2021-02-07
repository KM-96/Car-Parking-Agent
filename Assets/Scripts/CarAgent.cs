using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

/*
 * Agent Actions - Turn Right, Turn Left, Do Nothing
 * Total Actions - 3
 */
public class CarAgent : Agent
{

    private Rigidbody carRb;
    private Vector3 curPos, curRot;
    private float screenWidth;
    private float screenHeight;
    public GameObject powerUpPrefab;
    public GameObject target;
    public float carSpeed = 800f;
    public float carTurnSpeed = 5.0f;
    public bool isStun = true;
    public bool hasPowerUp = false;

    public override void Initialize()
    {
        carRb = GetComponent<Rigidbody>();
        cameraFollow.Instance.AddTarget(transform);

        curPos = this.transform.position;
        curRot = this.transform.eulerAngles;

        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    public override void OnActionReceived(float[] actions)
    {
        Vector3 forward = (transform.forward * carSpeed);
        carRb.AddForce(forward);

        if (Mathf.FloorToInt(actions[0]) == 1)
        {
            transform.Rotate(Vector3.up * carTurnSpeed);
        }
        if (Mathf.FloorToInt(actions[0]) == 2)
        {
            transform.Rotate(Vector3.down * carTurnSpeed);
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
        sensor.AddObservation(new Vector3(-4.889f, 0f , 14.662f));
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Park")
        {
            AddReward(10f);
            EndEpisode();
        }
        if (other.gameObject.tag == "Obstacle" && !hasPowerUp)
        {
            AddReward(-1.0f);
            EndEpisode();
            this.transform.position = curPos;
            this.transform.eulerAngles = curRot;
        }
        if (other.gameObject.tag == "Wall" && !hasPowerUp)
        {
            AddReward(-1.0f);
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
        if (other.gameObject.tag == "PowerUp")
        {
            Debug.Log("PowerUp");
            hasPowerUp = true;
            powerUpPrefab.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpRoutine());
        }
        Debug.Log("Reward = " + GetCumulativeReward());
    }
    IEnumerator PowerUpRoutine()
    {
        yield return new WaitForSeconds(8);
        hasPowerUp = false;
        powerUpPrefab.SetActive(false);
    }
}
