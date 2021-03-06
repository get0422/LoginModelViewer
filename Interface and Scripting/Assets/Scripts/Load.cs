﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;


public class Load : MonoBehaviour {

    // Public Variables
    public string[] LoggedUserSave;
    public GameObject OnceLoaded;
    // Private Variables
    private int rest = 0;
    private string form;
    private string num;
    private float[] Numbers;
    private BitArray isLoaded;

    // Use this for Initialization
    void Start()
    {
        // Sets Variables
        num = "";
        isLoaded = new BitArray(4, false);
    }

    public void SetUserLoad(string[] currentUsersSave)
    {
        // Grabbs Currently Logged in User's Save File
        LoggedUserSave = currentUsersSave;

        Numbers = new float[currentUsersSave.Length];
    }

    public void LoadFile()
    {
        // Model Increment
        int increment = 0;

        // Iterates Through Each Line
        for (int x = 4; x < LoggedUserSave.Length; x++)
        {
            if (LoggedUserSave[x].Length == 0 || LoggedUserSave[x][0] == 'P' || LoggedUserSave[x][0] == 'R')
                continue;

            // Iterates Through the Entire Sentencte
            for (int y = 0; y < LoggedUserSave[x].Length; y++)
                num += LoggedUserSave[x][y];

            // Sets Numbers Equal to the Currennt Value Found
            Numbers[increment] = (float)Convert.ToDouble(num);
            increment++;

            // Clears Num
            num = "";
        }
    }

    // Loads Position and Rotation
    public void LoadPosRo(Model currentModelList, int modelnumber)
    {
        // Resets it if another User Logged Out
        if(rest == 0) {
            rest++;
            for (int i = 0; i < isLoaded.Length; i++)
            {
                isLoaded[i] = false;
            }
        }

        // Checks to see if its been loaded
        if (isLoaded[modelnumber] == true)
            return;

        // Grabs Current Model
        GameObject temp = GameObject.Find(currentModelList.gameModel.name);
        
        // Depending on the model it will go further in the document
        int increment = modelnumber * 7;

        // Checks if Object is Null
        if (temp.gameObject.Equals(null))
            return;

        // Checks to see if its a new User
        if(Numbers.Length < 4)
        {
            // Sets Current Models Position
            temp.transform.position = new Vector3(0, 3.051758E-05f, 1.5f);
            increment += 3;

            // Sets Current Models Rotation
            temp.transform.rotation = Quaternion.identity;
            increment += 4;
        }
        else {
            // Sets Current Models Position
            temp.transform.position = new Vector3(Numbers[increment], Numbers[increment + 1], Numbers[increment + 2]);
            increment += 3;

            // Sets Current Models Rotation
            temp.transform.rotation = new Quaternion(Numbers[increment], Numbers[increment + 1], Numbers[increment + 2], Numbers[increment + 3]);
            increment += 4;
        }

        // Sets that it has Already Been Loaded
        isLoaded[modelnumber] = true;
    }

    public void ResetObjects()
    {
        rest = 0;
        OnceLoaded.GetComponent<PlayerLog>().onlyOnce = 0;
    }
}
