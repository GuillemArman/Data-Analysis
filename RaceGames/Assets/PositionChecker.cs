using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public class PositionChecker : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject car;

    public float cadency = 0.1f;

    float time = 0.0f;
    EventManager manager;


    void Start()
    {
        manager = GetComponent<EventManager>();

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;


        if (time >= cadency)
        {
            float speed = car.GetComponent<CarController>().CurrentSpeed;
            Vector3 dir = car.transform.rotation.eulerAngles.normalized;
            manager.AddPositionEvent(car.transform.position, car.transform.rotation, dir * speed);
            time = 0.0f;
        }
    }
}
