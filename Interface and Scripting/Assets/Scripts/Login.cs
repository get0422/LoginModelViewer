using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class Login : MonoBehaviour {

    //Public Variables
    public GameObject obj_LoginScreen;
    public GameObject obj_GameScreen;
    public Load playerLoad;
    public Save playerSave;

    // Private Variables
    private GameObject obj_username;
    private GameObject obj_password;
    private GameObject obj_Error;
    private string str_username;
    private string str_password;
    private string dec_password;
    private string[] Lines;

    // Use this for Initialization
    void Start () {
        // Set Variables
        obj_username = GameObject.Find("LoginUsername");
        obj_password = GameObject.Find("LoginPassword");
        obj_Error = GameObject.Find("LoginError");
    }

    // Update is called once per frame
    void Update () {
        // Checks if Tab is Pushed then What the User is Current Typing On
        if (Input.GetKeyDown(KeyCode.Tab))
            if (obj_username.GetComponent<InputField>().isFocused)
                obj_password.GetComponent<InputField>().Select();

        // Checks if Enter is Pushed then if all InputFields Where Filled 
        if (Input.GetKeyDown(KeyCode.Return)) { 
            if (str_username != "" && str_password != "") { 
                LoginButton(); playerLoad.LoadFile();
            }
        }
        // Sets Current Strings to their respective InputFields
        str_username = obj_username.GetComponent<InputField>().text;
        str_password = obj_password.GetComponent<InputField>().text;
    }

    public void LoginButton()
    {
        bool user = false, pass = false;

        // Checks Username and Password
        user = CheckUsename(user);
        pass = CheckPassword(pass);

        if (user == true && pass == true)
        {
            // Grab Current User
            playerSave.SetUser(str_username);

            // Grabs Users Save
            playerSave.SetUserSave(Lines);
            playerLoad.SetUserLoad(Lines);

            // Clears InputFields
            obj_username.GetComponent<InputField>().text = "";
            obj_password.GetComponent<InputField>().text = "";
            obj_Error.GetComponent<Text>().text = "";

            // Changes To Game
            obj_LoginScreen.SetActive(false);
            obj_GameScreen.SetActive(true);
        }
    }

    public void LogoutButton()
    {
            str_username = "";
            str_password = "";
            dec_password = "";
            Lines = null;
            obj_GameScreen.SetActive(false);
            obj_LoginScreen.SetActive(true);
    }

    private bool CheckUsename(bool user)
    {
        // Username
        if (str_username != "")
        {
            if (System.IO.File.Exists(str_username + ".txt"))
            {
                user = true;
                Lines = System.IO.File.ReadAllLines(str_username + ".txt");
            }
            else
            {
                obj_Error.GetComponent<Text>().text = "Error: Username does not Exist";
            }
        }
        else
        {
            obj_Error.GetComponent<Text>().text = "Error: No Username Entered";
        }
        return user;
    }

    private bool CheckPassword(bool pass)
    {
        // Password
        if (str_password != "")
        {
            if (System.IO.File.Exists(str_username + ".txt"))
            {
                // Sets Temperary Variables
                int x = 1;
                int i = 0;

                // Iterates throgh the Password and Decrypts it
                foreach (char item in Lines[2])
                {
                    // Skips First 10 Characters
                    if (i > 9)
                    {
                        // Increments
                        x++;

                        // Times the letter / number
                        char Dec = (char)(item / x);

                        // Then Adds it to The Cleared Password
                        dec_password += Dec.ToString();
                    }
                    i++;
                }

                // Checks if Stored Password and Typed Password are the Same
                if (str_password == dec_password)
                {
                    pass = true;
                }
                else
                {
                    obj_Error.GetComponent<Text>().text = "Error: Password is Incorrect";
                }
            }
            else
            {
                obj_Error.GetComponent<Text>().text = "Error: Username does not Exist";
            }
        }
        else
        {
            obj_Error.GetComponent<Text>().text = "Error: No Password Entered";
        }
        return pass;
    }
}
