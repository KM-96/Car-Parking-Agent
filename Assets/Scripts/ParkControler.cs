using UnityEngine;

public class ParkControler : MonoBehaviour
{
    private Light lt;
    public GameObject parkPrefab;
    public bool hasPark = false;

    void Update()
    {
        lt = GameObject.Find("Spot Light").GetComponent<Light>();
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Park score = " + ScoreScript.parkingScoreValue +
            " Obstacle score = " + ScoreScript.obstacleHitScoreValue +
            " Wall score = " + ScoreScript.wallHitScoreValue);
        if (other && other.gameObject && other.GetComponent<CarAgent>())
        {
                other.GetComponent<CarAgent>().enabled = false;
                lt.color = Color.green;
                hasPark = true;
                Destroy(parkPrefab, 1);
        }
    }
}
