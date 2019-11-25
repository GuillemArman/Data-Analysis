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

    public void Start()
    {
        eventmanager = GetComponent<EventManager>();
    }

    public void Save_Pos(List <EventManager.EventPosition> positions)
    {

        string[] title = new string[1];
        title[0] = "Positions";
        Save(title);
       
        string[] str_positions = new string[(13)];

        for (int i = 0; i<positions.Count; i++)
        {
            int j = 0;

            str_positions[j++] = positions[i].sessionID.ToString();
            str_positions[j++] = positions[i].timeStamp.ToString();
            str_positions[j++] = positions[i].round.ToString();
            str_positions[j++] = positions[i].pos.x.ToString();
            str_positions[j++] = positions[i].pos.y.ToString();
            str_positions[j++] = positions[i].pos.z.ToString();
            str_positions[j++] = positions[i].rot.x.ToString();
            str_positions[j++] = positions[i].rot.y.ToString();
            str_positions[j++] = positions[i].rot.z.ToString();
            str_positions[j++] = positions[i].rot.w.ToString();
            str_positions[j++] = positions[i].vel.x.ToString();
            str_positions[j++] = positions[i].vel.y.ToString();
            str_positions[j++] = positions[i].vel.z.ToString();

            Save(str_positions);

        }
        
    }

    public void Save_Sessions(List<EventManager.EventPosition> sessions)
    {

        string[] title = new string[1];
        title[0] = "Sessions";
        Save(title);

        string[] str_sessions = new string[(13)];

        for (int i = 0; i < sessions.Count; i++)
        {
            int j = 0;

            
            str_sessions[j++] = sessions[i].sessionID.ToString();
           

            Save(str_sessions);

        }

    }

    public void Save_Hits(List<EventManager.EventPosition> hits)
    {

        string[] title = new string[1];
        title[0] = "Hits";
        Save(title);

        string[] str_hits = new string[(13)];

        for (int i = 0; i < hits.Count; i++)
        {
            int j = 0;


            str_hits[j++] = hits[i].sessionID.ToString();


            Save(str_hits);

        }

    }

    public void Save_roundEnd(List<EventManager.EventPosition> roundEnd)
    {

        string[] title = new string[1];
        title[0] = "RoundEnd";
        Save(title);

        string[] str_roundend = new string[(13)];

        for (int i = 0; i < roundEnd.Count; i++)
        {
            int j = 0;


            str_roundend[j++] = roundEnd[i].sessionID.ToString();


            Save(str_roundend);

        }

    }

    public void Save_Errors(List<EventManager.EventPosition> errors)
    {

        string[] title = new string[1];
        title[0] = "Errors";
        Save(title);

        string[] str_errors = new string[(13)];

        for (int i = 0; i < errors.Count; i++)
        {
            int j = 0;


            str_errors[j++] = errors[i].sessionID.ToString();


            Save(str_errors);

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

   


    // Following method is used to retrive the relative path as device platform
    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + "Saved_data.csv";
#else
        return Application.dataPath +"/"+"Saved_data.csv";
#endif
    }
}

