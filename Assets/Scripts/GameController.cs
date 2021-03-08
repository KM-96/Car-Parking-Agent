using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] carPrefabs;
    public GameObject[] parkPrefabs;
    public GameObject[] startPoints;
    public GameObject target;
    public GameObject car;
    public List<Vector3> goals;

    void Start()
    {
        // KM: We will stick to only BatMobil for our project 
        int randomCar = 0;
        int randomCarSpot = Random.Range(0, startPoints.Length);

        car = Instantiate(carPrefabs[randomCar], startPoints[randomCarSpot].transform.position, startPoints[randomCarSpot].transform.rotation);
        Instantiate(parkPrefabs[0], target.transform.position, target.transform.rotation);
        CarAgent.FindObjectOfType<CarAgent>().isStun = true;
        CarAgent.FindObjectOfType<CarAgent>().setTarget(target);

        goals = new List<Vector3>();
        goals.Add(new Vector3(-3.2f, 0.3f, 12f));
        goals.Add(new Vector3(7f, 0.3f, 12f));
        goals.Add(new Vector3(12f, 0.3f, 12f));
        goals.Add(new Vector3(1f, 0.3f, -11f));
        goals.Add(new Vector3(1f, 0.3f, 1f));

    }


    Vector3 getGoalLocation()
    {
        int random = Random.Range(0, 5);
        return goals[random];
    }

    void Update()
    {
        if (ParkControler.FindObjectOfType<ParkControler>().hasPark == true)
        {
            Destroy(car);
            int randomCar = 0;
            int randomCarSpot = Random.Range(0, startPoints.Length);

            target.transform.position = getGoalLocation();
            car = Instantiate(carPrefabs[randomCar], startPoints[randomCarSpot].transform.position, startPoints[randomCarSpot].transform.rotation);
            Instantiate(parkPrefabs[0], target.transform.position, target.transform.rotation);
            //Instantiate(powerUpPrefab, PowerUpSpawnPosition(), powerUpPrefab.transform.rotation);
            CarAgent.FindObjectOfType<CarAgent>().isStun = true;
            CarAgent.FindObjectOfType<CarAgent>().setTarget(target);
            cameraFollow.FindObjectOfType<cameraFollow>().targets.RemoveAt(0);
        }
    }
}
