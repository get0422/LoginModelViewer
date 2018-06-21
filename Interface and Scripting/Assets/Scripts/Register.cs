using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class Register : MonoBehaviour
{
    // Private Variables
    private GameObject obj_username;
    private GameObject obj_email;
    private GameObject obj_password;
    private GameObject obj_passwordConf;
    private GameObject obj_Error;
    private string str_username;
    private string str_email;
    private string str_password;
    private string str_passwordConf;
    private string registrationForm;

    // Use this for Initialization
    void Start()
    {
        // Sets Variables
        obj_username = GameObject.Find("RegisterUsername");
        obj_email = GameObject.Find("RegisterEmail");
        obj_password = GameObject.Find("RegisterPassword");
        obj_passwordConf = GameObject.Find("RegisterPassComf");
        obj_Error = GameObject.Find("RegisterError");
    }

    // Update is called once Per Frame
    void Update()
    {
        // Checks if Tab is Pushed
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Checks What the User is Current Typing On
            if (obj_username.GetComponent<InputField>().isFocused)
                obj_email.GetComponent<InputField>().Select();

            if (obj_email.GetComponent<InputField>().isFocused)
                obj_password.GetComponent<InputField>().Select();

            if (obj_password.GetComponent<InputField>().isFocused)
                obj_passwordConf.GetComponent<InputField>().Select();
        }

        // Checks if Enter is Pushed then if all InputFields where Filled 
        if (Input.GetKeyDown(KeyCode.Return))
            if (str_username != "" && str_email != "" && str_password != "" && str_passwordConf != "")
                RegisterButton();

        // Sets Current Strings to their respective InputFields
        str_username = obj_username.GetComponent<InputField>().text;
        str_email = obj_email.GetComponent<InputField>().text;
        str_password = obj_password.GetComponent<InputField>().text;
        str_passwordConf = obj_passwordConf.GetComponent<InputField>().text;

    }

    public void RegisterButton()
    {
        // Sets BitArray
        BitArray Checks = new BitArray(4, false);

        // Checks InputFields
        Checks[0] = CheckUsername(Checks[0]);
        Checks[1] = CheckEmail(Checks[1]);
        Checks[2] = CheckPassword(Checks[2]);
        Checks[3] = CheckPasswordMatch(Checks[3]);

        // if All Checks Pass
        if (Checks[0] == true && Checks[1] == true && Checks[2] == true && Checks[3] == true)
        {   
            EncryptPassword();

            // Adds All InputFields to 
            registrationForm = ("Username: " + str_username + Environment.NewLine + "Email: " + str_email + Environment.NewLine + "Password: " + str_password);
            System.IO.File.WriteAllText(str_username + ".txt", registrationForm);

            // Clears InputFields
            obj_username.GetComponent<InputField>().text = "";
            obj_email.GetComponent<InputField>().text = "";
            obj_password.GetComponent<InputField>().text = "";
            obj_passwordConf.GetComponent<InputField>().text = "";

            // Registration Completion
            obj_Error.GetComponent<Text>().text = "You Are Now Registered";
        }
    }

    private bool CheckUsername(bool user)
    {
        // Username
        if (str_username != "")
        {
            if (!System.IO.File.Exists(str_username + ".txt"))
            {
                user = true;
            }
            else
            {
                obj_Error.GetComponent<Text>().text = "Error: Name Already Exists";
            }
        }
        else
        {
            obj_Error.GetComponent<Text>().text = "Error: No Username Entered";
        }
        return user;
    }

    private bool CheckEmail(bool email)
    {
        // Email
        if (str_email != "")
        {
            if (str_email.Contains("@"))
            {
                if (str_email.Contains("."))
                {
                    email = true;
                }
                else
                {
                    obj_Error.GetComponent<Text>().text = "Error: Enter Valid Email";
                }
            }
            else
            {
                obj_Error.GetComponent<Text>().text = "Error: Enter Valid Email";
            }
        }
        else
        {
            obj_Error.GetComponent<Text>().text = "Error: No Email Entered";
        }
        return email;
    }

    private bool CheckPassword(bool pass)
    {
        // Password
        if (str_password != "")
        {
            if (str_password.Length > 5)
            {
                for (int i = 0; i < str_password.Length; i++)
                {
                    if (char.IsDigit(str_password[i]))
                        pass = true;
                }
                if (pass == false)
                {
                    obj_Error.GetComponent<Text>().text = "Error: Does not contain a Number";
                }
            }
            else
            {
                obj_Error.GetComponent<Text>().text = "Error: Is not at least 6 Characters Long";
            }
        }
        else
        {
            obj_Error.GetComponent<Text>().text = "Error: No Password Entered";
        }
        return pass;
    }

    private bool CheckPasswordMatch(bool confpass)
    {
        // Comfirm Password
        if (str_passwordConf != "")
        {
            if (str_passwordConf == str_password)
            {
                confpass = true;
            }
            else
            {
                obj_Error.GetComponent<Text>().text = "Error: Password Does Not Match";
            }
        }
        else
        {
            obj_Error.GetComponent<Text>().text = "Error: Password Does Not Match";
        }
        return confpass;
    }

    private void EncryptPassword()
    {
        // Sets Temperary Variables
        bool clear = true;
        int x = 1;

        // Iterates throgh the Password and Encrypts it
        foreach (char item in str_password)
        {
            if (clear)
            {
                str_password = "";
                clear = false;
            }
            x++;

            // Times the letter * number
            char Enc = (char)(item * x);

            // Then Adds it to The Cleared Password
            str_password += Enc.ToString();
        }
    }
}