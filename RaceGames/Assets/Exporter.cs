using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class Exporter : MonoBehaviour
{
    private List<string[]> rowData = new List<string[]>();
    EventManager eventmanager;

    string delimiter = ";";

    public void Start()
    {
        eventmanager = GetComponent<EventManager>();
    }

    public void Save_Pos(List <EventManager.EventPosition> positions)
    {

        for (int i = 0; i < positions.Count; i++)
        {
            string str = "";
            str += positions[i].sessionID.ToString() + delimiter;
            str += positions[i].timeStamp.ToString() + delimiter;
            str += positions[i].round.ToString() + delimiter;

            str += positions[i].pos.x.ToString() + delimiter;
            str += positions[i].pos.y.ToString() + delimiter;
            str += positions[i].pos.z.ToString() + delimiter;

            str += positions[i].rot.x.ToString() + delimiter;
            str += positions[i].rot.y.ToString() + delimiter;
            str += positions[i].rot.z.ToString() + delimiter;
            str += positions[i].rot.w.ToString() + delimiter;

            str += positions[i].vel.x.ToString() + delimiter;
            str += positions[i].vel.y.ToString() + delimiter;
            str += positions[i].vel.z.ToString();

            SaveLine(str, "Data/Positions.csv");
        }      
    }

    public void Save_Sessions(List<EventManager.EventSession> sessions)
    {

        for (int i = 0; i < sessions.Count; i++)
        {

            string str_sessions = "";
            str_sessions += sessions[i].sessionID.ToString() + delimiter;
            str_sessions += sessions[i].playerID + delimiter;
            str_sessions += sessions[i].timeStamp.ToString() + delimiter;
            str_sessions += sessions[i].sessionType.ToString();


            SaveLine(str_sessions, "Data/Sessions.csv");
        }

    }

    public void Save_Hits(List<EventManager.EventHit> hits)
    {

        for (int i = 0; i < hits.Count; i++)
        {

            string str_hits = "";
            str_hits += hits[i].sessionID.ToString() + delimiter;
            str_hits += hits[i].timeStamp.ToString() + delimiter;
            str_hits += hits[i].obstacleId.ToString();

            SaveLine(str_hits, "Data/Hits.csv");

        }
    }

    public void Save_roundEnd(List<EventManager.EventRoundEnd> roundEnd)
    {

        for (int i = 0; i < roundEnd.Count; i++)
        {
            string str_roundend = "";


            str_roundend += roundEnd[i].sessionID.ToString() + delimiter;
            str_roundend += roundEnd[i].round.ToString() + delimiter;
            str_roundend += roundEnd[i].timeStamp.ToString() ;

            SaveLine(str_roundend, "Data/RoundEnd.csv");

        }

    }

    public void Save_Errors(List<EventManager.EventError> errors)
    {


        for (int i = 0; i < errors.Count; i++)
        {

            string str_errors = "";

            str_errors += errors[i].sessionID.ToString() + delimiter;
            str_errors += errors[i].timeStamp.ToString() + delimiter;
            str_errors += errors[i].errorType.ToString();


            SaveLine(str_errors, "Data/Errors.csv");

        }

    }


    public void Save(string[] list)
    {

        // Creating First row of titles manually..
        string[] rowDataTemp = new string[1];
        rowDataTemp[0] = name;
        rowData.Add(rowDataTemp);

        // You can add up the values in as many cells as you want.
        for (int i = 0; i < list.Length; i++)
        {
            rowDataTemp = new string[1];
            rowDataTemp[0] = list[i];
            rowData.Add(rowDataTemp);
        }

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = getPath();

        StreamWriter outStream = System.IO.File.AppendText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    public void SaveLine(string str, string filename)
    {

        string filePath = Application.dataPath + "/" + filename;
       
        StreamWriter outStream = System.IO.File.AppendText(filePath);
        outStream.WriteLine(str);
        outStream.Close();

    }

   


    // Following method is used to retrive the relative path as device platform
    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/Saved_data.csv";
#else
        return Application.dataPath +"/"+"Saved_data.csv";
#endif
    }
}

