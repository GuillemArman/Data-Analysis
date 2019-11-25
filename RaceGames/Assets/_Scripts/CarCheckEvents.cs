using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCheckEvents : MonoBehaviour
{
    public GameObject event_manager;

    private EventManager em_script;
    private bool fall_registered = false;

    // Start is called before the first frame update
    void Start()
    {
        em_script = event_manager.GetComponent<EventManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5.0f && !fall_registered)
        {
            em_script.AddErrorEvent(EventManager.ErrorType.FALL_OFF);
            fall_registered = true;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            int obstacle_id = other.gameObject.GetComponent<Obstacle>().GenID();
            em_script.AddHitEvent(obstacle_id);
        }
    }
}
