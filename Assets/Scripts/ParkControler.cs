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
        Debug.Log("here");
        if (other && other.gameObject && other.GetComponent<CarAgent>())
        {
                Debug.Log(other);
                other.GetComponent<CarAgent>().enabled = false;
                lt.color = Color.green;
                hasPark = true;
                Destroy(parkPrefab, 1);
        }
    }
}
