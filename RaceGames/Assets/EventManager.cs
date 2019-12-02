using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    Exporter exporter;

    int sessionID = 0;
    public string playerId;


    public int round = 0;

    public class Event
    {
        public int sessionID = 0;
        public float timeStamp = 0;

    }

   public class EventPosition : Event
    {
        public int round = 0;

        public Vector3 pos;
        public Quaternion rot;
        public Vector3 vel;

        public string MySerialize() {
            return "Saved_data";
        }
    }

    public class EventSession : Event
    {
        public string playerID;

        // SessionStart or SessionEnd : bool (0 Start, 1 End)
        public bool sessionType = false;
    }

    public class EventHit : Event
    {
        // 1 - Last Obstacle Number : int
        public int obstacleId = 0;
    }

    public class EventRoundEnd : Event
    {
        public int round = 0;
    }

    public  enum ErrorType
    {
        FALL_OFF,
        STUCK
    }
    public class EventError : Event
    {
        public ErrorType errorType;
    }

    List<EventPosition> positions;
    List<EventSession> sessions;
    List<EventHit> hits;
    List<EventRoundEnd> roundEnds;
    List<EventError> errors;


    // Start is called before the first frame update
    void Start()
    {
        exporter = GetComponent<Exporter>();

        sessionID = Random.Range(0, 99999);

        positions = new List<EventPosition>();
        sessions = new List<EventSession>();
        hits = new List<EventHit>();
        roundEnds = new List<EventRoundEnd>();
        errors = new List<EventError>();

        AddSessionEvent(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {

            HeatMapManager.Instance.CreateHeatMap(positions);

        }
    }

    void OnApplicationQuit()
    {
        AddSessionEvent(true);


        exporter.Save_Pos(positions);

        exporter.Save_Sessions(sessions);
        exporter.Save_Hits(hits);
        exporter.Save_roundEnd(roundEnds);
        exporter.Save_Errors(errors);


        Debug.Log("Application ending after " + Time.time + " seconds");

    }

    public void AddPositionEvent(Vector3 _pos, Quaternion _rot, Vector3 _vel)
    {
        EventPosition newEvent = new EventPosition();

        newEvent.sessionID = sessionID;
        newEvent.timeStamp = Time.realtimeSinceStartup;

        newEvent.pos = _pos;
        newEvent.rot = _rot;
        newEvent.vel = _vel;
        newEvent.round = round;

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

    public void AddHitEvent(int obstacleId)
    {
        EventHit newEvent = new EventHit();

        newEvent.sessionID = sessionID;
        newEvent.timeStamp = Time.realtimeSinceStartup;
        newEvent.obstacleId = obstacleId;

        hits.Add(newEvent);
    }

    public void AddRoundEndEvent(int round)
    {
        EventRoundEnd newEvent = new EventRoundEnd();

        newEvent.sessionID = sessionID;
        newEvent.timeStamp = Time.realtimeSinceStartup;

        newEvent.round = round;


        roundEnds.Add(newEvent);
    }

    public void AddErrorEvent(ErrorType error)
    {
        EventError newEvent = new EventError();

        newEvent.sessionID = sessionID;
        newEvent.timeStamp = Time.realtimeSinceStartup;

        newEvent.errorType = error;

        errors.Add(newEvent);
    }

    
}
