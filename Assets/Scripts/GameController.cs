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

    void Start()
    {
        // KM: We will stick to only BatMobil for our project 
        int randomCar = 0;
        int randomCarSpot = Random.Range(0, startPoints.Length);

        car = Instantiate(carPrefabs[randomCar], startPoints[randomCarSpot].transform.position, startPoints[randomCarSpot].transform.rotation);
        Instantiate(parkPrefabs[0], target.transform.position, target.transform.rotation);
        CarAgent.FindObjectOfType<CarAgent>().isStun = true;
        CarAgent.FindObjectOfType<CarAgent>().setTarget(target);

    }
    void Update()
    {
        if (ParkControler.FindObjectOfType<ParkControler>().hasPark == true)
        {
            Destroy(car);
            int randomCar = 0;
            int randomCarSpot = Random.Range(0, startPoints.Length);

            //int randomXValue = Random.Range(1, 3) == 1 ? Random.Range(-14, -5) : Random.Range(8, 14);
            //int randomYValue = Random.Range(1, 3) == 1 ? Random.Range(-14, -8) : Random.Range(3, 14);
            float randomXValue = -3.8f;
            float randomYValue = 12.2f;
            target.transform.position = new Vector3(randomXValue, 0.3f, randomYValue);

            car = Instantiate(carPrefabs[randomCar], startPoints[randomCarSpot].transform.position, startPoints[randomCarSpot].transform.rotation);
            Instantiate(parkPrefabs[0], target.transform.position, target.transform.rotation);
            //Instantiate(powerUpPrefab, PowerUpSpawnPosition(), powerUpPrefab.transform.rotation);
            CarAgent.FindObjectOfType<CarAgent>().isStun = true;
            CarAgent.FindObjectOfType<CarAgent>().setTarget(target);
            cameraFollow.FindObjectOfType<cameraFollow>().targets.RemoveAt(0);
        }
    }
}
