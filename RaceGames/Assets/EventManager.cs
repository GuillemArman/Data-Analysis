using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : MonoBehaviour
{

    int sessionID = 0;
    public string playerId;

    class Event
    {
        public int sessionID = 0;
        public float timeStamp = 0;

    }

    class EventPosition : Event
    {
        public uint round = 0;

        public Vector3 pos;
        public Quaternion rot;
        public Vector3 vel;
    }

    class EventSession : Event
    {
        public string playerID;

        // SessionStart or SessionEnd : bool (0 Start, 1 End)
        public bool sessionType = false;
    }

    class EventHit : Event
    {
        // 1 - Last Obstacle Number : int
        public int obstacleId = 0;
    }

    class EventRoundEnd : Event
    {
        public int round = 0;
        public string roundType;
    }

    public enum ErrorType
    {
        FALL_OFF,
        STUCK
    }
    class EventError : Event
    {
        public ErrorType errorType;
    }


    List<EventPosition> positions;
    List<EventSession> sessions;
    List<EventHit> hits;
    List<EventRoundEnd> roundEnds;
    List<EventError> eventError;


    // Start is called before the first frame update
    void Start()
    {
        sessionID = Random.Range(0, 99999);
    }

    // Update is called once per frame
    void Update()
    {       
    }

    public void AddPositionEvent(Vector3 _pos, Quaternion _rot, Vector3 _vel)
    {
        EventPosition newEvent = new EventPosition();

        newEvent.sessionID = sessionID;
        newEvent.timeStamp = Time.realtimeSinceStartup;

        newEvent.pos = _pos;
        newEvent.rot = _rot;
        newEvent.vel = _vel;

        positions.Add(newEvent);
    }

    public void AddSessionEvent(bool sessiontype)
    {
        EventSession newEvent = new EventSession();

        newEvent.sessionID = sessionID;
        newEvent.timeStamp = Time.realtimeSinceStartup;

        newEvent.playerID = playerId;
        newEvent.sessionType = sessiontype;

        sessions.Add(newEvent);
    }

    public void AddHitEvent(bool sessiontype)
    {
        EventHit newEvent = new EventHit();

        newEvent.sessionID = sessionID;
        newEvent.timeStamp = Time.realtimeSinceStartup;



        hits.Add(newEvent);
    }
}
