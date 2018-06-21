using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLog : MonoBehaviour {

    // Public Variables
    public List<string> Eventlog = new List<string>();
    public List<string> Savelog = new List<string>();
    public int maxLines = 15;
    public short onlyOnce;

    // Private Variables
    private string gui_Text;
    private GUIStyle style;
    private Load load;
    private PlayerLog log;
    private Model lastSelected;
    private List<Model> modelList;

    // Use this for Initialization
    void Start()
    {
        // Sets Variables
        load = GameObject.FindGameObjectWithTag("ContentList").GetComponent<Load>();
        log = GetComponent<PlayerLog>();
        onlyOnce = 0;
        gui_Text = "";
    }

    // Update is called once per frame
    void Update()
    {
        // Logs Based on Model
        LogSpecificObject();

        // Checks if Scrolled and adds to Log 
        if (Input.mouseScrollDelta.y > 0)
            log.AddEvent("Enlarge");

        if (Input.mouseScrollDelta.y < 0)
            log.AddEvent("Shrink");

        // Checks if Panning and adds to Log
        if (Input.GetMouseButtonUp(2))
            log.AddEvent("Panning");
    }

    void OnGUI()
    {
        // Sets GUI Style
        style = GUI.skin.textArea;
        
        // Sets Font Size Based On the Number of Lines
        style.fontSize = ((Screen.height / 4)) / (maxLines + 1);

        // Creats GUI Label
        GUI.Label(new Rect(0, Screen.height - (Screen.height / 4), Screen.width - ((Screen.width / 6) * 5), Screen.height / 4), gui_Text, GUI.skin.textArea);
    }

    public void AddEvent(string eventString)
    {
        // Adds Event To the Logs
        Eventlog.Add(eventString);
        Savelog.Add(eventString);

        // Delets First entry if Maxed
        if (Eventlog.Count >= maxLines)
            Eventlog.RemoveAt(0);

        // Clears guiText
        gui_Text = "";

        // Adds Everything From the Log to guiText
        foreach (string logEvent in Eventlog)
        {
            gui_Text += logEvent;
            gui_Text += "\n";
        }
    }

    public void UpdateModelList(List<Model> currentModelList)
    {
        // Sets Model List 
        modelList = currentModelList;
    }

    void LogSpecificObject()
    {
        // Checks if there are any Models
        if (modelList != null)
        {
            // Iterates through the List of Models
            for (int i = 0; i < modelList.Count; i++)
            {
                // Checks to see What Model is Active
                if (modelList[i].isActive)
                {
                    // Incremented Each Iteration
                    onlyOnce++;

                    // Checks if its the First Time Logging Model
                    if (onlyOnce == 1)
                    {
                        // Adds Clicked Event to Log
                        log.AddEvent("Clicked " + modelList[i].gameModel.name);
                        lastSelected = modelList[i];

                        // Loads Last Postion When Saved
                        load.LoadPosRo(modelList[i], i);
                    }

                    // Checks if Model was Clicked On
                    if (Input.GetMouseButtonDown(0) && GameObject.Find(modelList[i].gameModel.name).GetComponent<MouseDrag>().isMoving)
                        log.AddEvent("Picks-Up " + modelList[i].gameModel.name);

                    // Checks if Model is still being Clicked on
                    if (Input.GetMouseButton(0) && GameObject.Find(modelList[i].gameModel.name).GetComponent<MouseDrag>().isMoving)
                        log.AddEvent("Drags " + modelList[i].gameModel.name);

                    // Checks if Started Rotating
                    if (Input.GetMouseButtonDown(1))
                        log.AddEvent("Started Rotating " + modelList[i].gameModel.name);

                    // Checks if Stopped Rotating
                    if (Input.GetMouseButtonUp(1))
                        log.AddEvent("Stopped Rotating " + modelList[i].gameModel.name);
                }

                // Checks if Last Object is still Active if so Resets
                if (lastSelected != null && lastSelected.isActive == false)
                    onlyOnce = 0;
            }
        }
    }
}
