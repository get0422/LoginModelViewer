using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class Save : MonoBehaviour {

    //Public Variables
    public PlayerLog playerLog;

    // Private Variables
    private ModelScrollList contentPanel;
    private string form;
    private string LoggedUser;
    private string[] LoggedUserSave;

    public void SetUser(string currentUser)
    {
        // Grabbs Currently Logged in User
        LoggedUser = currentUser;
    }
    public void SetUserSave(string[] currentUsersSave)
    {
        // Grabbs Currently Logged in User's Save File
        LoggedUserSave = currentUsersSave;
    }

    public void SaveFile()
    {
        // Writes User and their Actions
        SaveUserActions();

        // Adds User Information to the String
        for (int i = 0; i < 3; i++)
        {
            if (LoggedUserSave == null || LoggedUserSave[i] == null)
                continue;

            for (int x = 0; x < LoggedUserSave[i].Length; x++)
            {
                form += LoggedUserSave[i][x];
            }
            form += Environment.NewLine;
        }
        
        // Grabs Model List
        contentPanel = GameObject.FindGameObjectWithTag("ContentList").GetComponent<ModelScrollList>();

        // Saves Model Rotation and Location
        for (int i = 0; i < contentPanel.modelList.Count; i++)
        {
            form += Environment.NewLine + "Position X-Y-Z for " + contentPanel.modelList[i].gameModel.name;
            form += Environment.NewLine;
            form += contentPanel.modelList[i].gameModel.transform.position.x;
            form += Environment.NewLine;
            form += contentPanel.modelList[i].gameModel.transform.position.y;
            form += Environment.NewLine;
            form += contentPanel.modelList[i].gameModel.transform.position.z;

            form += Environment.NewLine;
            form += Environment.NewLine + "Rotation X-Y-Z-W for " + contentPanel.modelList[i].gameModel.name;
            form += Environment.NewLine;
            form += contentPanel.modelList[i].gameModel.transform.rotation.x;
            form += Environment.NewLine;
            form += contentPanel.modelList[i].gameModel.transform.rotation.y;
            form += Environment.NewLine;
            form += contentPanel.modelList[i].gameModel.transform.rotation.z;
            form += Environment.NewLine;
            form += contentPanel.modelList[i].gameModel.transform.rotation.w;
            form += Environment.NewLine + Environment.NewLine;
        }

        // Writes Save to File
        System.IO.File.WriteAllText(LoggedUser + ".txt", form);

        // Clear String
        form = "";
    }

    void SaveUserActions()
    {
        // Sets Current Player
        playerLog = GameObject.Find("UserLogPanel").GetComponent<PlayerLog>();

        // Checks to see if File Exists
        if (System.IO.File.Exists("playerLog.txt"))
            form += Environment.NewLine + Environment.NewLine;

        // Checks to See if there is a User
        if (LoggedUser == null)
            return;

        // Writes Current User
        form += LoggedUser + "'s Log - " + DateTime.Today.ToString() + Environment.NewLine;
        for (int i = 0; i < LoggedUser.Length; i++) {
            form += "-";
        }

        form += "--------" + Environment.NewLine;

        // Writes Users Actions
        for (int index = 0; index < playerLog.Savelog.Count; index++)
        {
            form += "Action #" + index + ": " + playerLog.Savelog[index] + Environment.NewLine;
        }

        // Writes to File
        System.IO.File.AppendAllText("playerLog.txt", form);

        // Clears Log
        playerLog.Savelog = new List<string>();
        form = "";
    }

    public void Quit()
    {
        Application.Quit();
    }

    void OnApplicationQuit()
    {
        SaveFile();
        SaveUserActions();
    }
}
