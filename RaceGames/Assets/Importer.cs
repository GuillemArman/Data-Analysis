using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Importer : MonoBehaviour
{

    List<EventManager.EventPosition> positions;
    List<EventManager.EventSession> sessions;
    List<EventManager.EventHit> hits;
    List<EventManager.EventRoundEnd> roundEnds;
    List<EventManager.EventError> errors;

    string delimiter = ";";

    // Start is called before the first frame update
    void Start()
    {
        positions = new List<EventManager.EventPosition>();
        sessions = new List<EventManager.EventSession>();
        hits = new List<EventManager.EventHit>();
        roundEnds = new List<EventManager.EventRoundEnd>();
        errors = new List<EventManager.EventError>();

        ReadPositions();
        //ReadSessions();
        //ReadHits();
        //ReadRoundEnd();
        //ReadErrors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string[] ReadDoc(string filename)
    {
        string path = Application.dataPath + "/" + filename;

        string fileData = System.IO.File.ReadAllText(path);
        string[] lines = fileData.Split("\n"[0]);

        //var lineData : String[] = (lines[0].Trim()).Split(","[0]);
        //var x : float;
        //float.TryParse(lineData[0], x);

        return lines;
    }

    void ReadPositions()
    {
        string[] lines = ReadDoc("Data/Positions.csv");
        int size = 13;

        if (lines.Length == 0) return;

        for (int i = 0; i < lines.Length; i++)
        {
            string[] lineData = lines[i].Trim().Split(delimiter[0]);

            if (lineData.Length < size) break;

            EventManager.EventPosition newpos = new EventManager.EventPosition();

            newpos.sessionID = int.Parse(lineData[0]);
            newpos.timeStamp = float.Parse(lineData[1]);
            newpos.round = int.Parse(lineData[2]);

            newpos.pos.x = float.Parse(lineData[3]);
            newpos.pos.y = float.Parse(lineData[4]);
            newpos.pos.z = float.Parse(lineData[5]);

            newpos.rot.x = float.Parse(lineData[6]);
            newpos.rot.y = float.Parse(lineData[7]);
            newpos.rot.z = float.Parse(lineData[8]);
            newpos.rot.w = float.Parse(lineData[9]);

            newpos.vel.x = float.Parse(lineData[10]);
            newpos.vel.y = float.Parse(lineData[11]);
            newpos.vel.z = float.Parse(lineData[12]);

            positions.Add(newpos);
        }
    }

    void ReadSessions()
    {
        string[] lines = ReadDoc("Data/Sessions.csv");

        if (lines.Length == 0) return;

        for (int i = 0; i < lines.Length; i++)
        {
            string[] lineData = lines[i].Trim().Split(delimiter[0]);

            if (lineData.Length < 4) break;

            EventManager.EventSession newevent = new EventManager.EventSession();

            newevent.sessionID = int.Parse(lineData[0]);
            newevent.playerID = lineData[1];
            newevent.timeStamp = float.Parse(lineData[2]);
            newevent.sessionType = bool.Parse(lineData[3]);

            sessions.Add(newevent);
        }
    }

    void ReadHits()
    {
        string[] lines = ReadDoc("Data/Hits.csv");

        if (lines.Length == 0) return;

        for (int i = 0; i < lines.Length; i++)
        {
            string[] lineData = lines[i].Trim().Split(delimiter[0]);

            if (lineData.Length < 3) break;

            EventManager.EventHit newevent = new EventManager.EventHit();

            newevent.sessionID = int.Parse(lineData[0]);
            newevent.timeStamp = float.Parse(lineData[1]);
            newevent.obstacleId = int.Parse(lineData[2]);

            hits.Add(newevent);
        }
    }

    void ReadRoundEnd()
    {
        string[] lines = ReadDoc("Data/RoundEnd.csv");

        if (lines.Length == 0) return;

        for (int i = 0; i < lines.Length; i++)
        {
            string[] lineData = lines[i].Trim().Split(delimiter[0]);

            if (lineData.Length < 3) break;

            EventManager.EventRoundEnd newevent = new EventManager.EventRoundEnd();

            newevent.sessionID = int.Parse(lineData[0]);
            newevent.round = int.Parse(lineData[1]);
            newevent.timeStamp = float.Parse(lineData[2]);

            roundEnds.Add(newevent);
        }
    }

    void ReadErrors()
    {
        string[] lines = ReadDoc("Data/Errors.csv");

        if (lines.Length == 0) return;

        for (int i = 0; i < lines.Length; i++)
        {
            string[] lineData = lines[i].Trim().Split(delimiter[0]);

            if (lineData.Length < 3) break;

            EventManager.EventError newevent = new EventManager.EventError();

            newevent.sessionID = int.Parse(lineData[0]);
            newevent.timeStamp = float.Parse(lineData[1]);
            string errortype = lineData[2];

            if (errortype.Equals("FALL_OFF"))    newevent.errorType = EventManager.ErrorType.FALL_OFF;

            else if (errortype.Equals("STUCK"))  newevent.errorType = EventManager.ErrorType.STUCK;

            errors.Add(newevent);
        }
    }

}
