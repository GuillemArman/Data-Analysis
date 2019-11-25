using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundChecker : MonoBehaviour
{
    public GameObject goalGO;

    EventManager manager;

    Goal goal;
    bool enteredGoal = false;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<EventManager>();
        goal = goalGO.GetComponent<Goal>();
    }

    // Update is called once per frame
    void Update()
    {
        if (goal.GetFinished())
        {
            if (!enteredGoal)
            {
                manager.AddRoundEndEvent(manager.round);
                manager.round++;
                enteredGoal = true;
            }
        }
        else
        {
            enteredGoal = false;
        }
    }
}
