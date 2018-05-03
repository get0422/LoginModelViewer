using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

// Change to Struct
[System.Serializable]
public class Model
{
    public GameObject gameModel;
    public bool isActive = false;
}

public class ModelScrollList : MonoBehaviour {

    // Public Variables
    // The Prefab that we Return Instances of
    public GameObject prefab;
    // List of Models
    public List<Model> modelList;

    // Private Variables
    private Transform contentPanel;


	// Use this for Initialization
	void Start () {
        // Sets Panel
        contentPanel = GameObject.FindGameObjectWithTag("ContentList").transform;

        // Adds the list of objects to the Panel
        Add();
    }
	
	private void Add () {
        // Iterates through the list of Models
        for (int i = 0; i < modelList.Count; i++) 
        {
            // Sets Current Model
            Model item = modelList[i];

            // Sets Button
            GameObject newButton = GetObject();

            // Makes Current Parent the Panel
            newButton.transform.SetParent(contentPanel);

            // Changes the size dependent on how big the logout Button is
            newButton.transform.localScale = GameObject.Find("Logout").transform.localScale;

            // Sets up the Models List of Buttons
            ListButton listButton = newButton.GetComponent<ListButton>();
            listButton.Setup(item, this);
        }
    }

    public GameObject GetObject()
    {
        // Create GameObject
        GameObject spawnedGameObject;

        // Create a New Instance
        spawnedGameObject = (GameObject)GameObject.Instantiate(prefab);

        // Put the Instance in the Root of the Scene and Enable it
        spawnedGameObject.transform.SetParent(null);
        spawnedGameObject.SetActive(true);

        // Return a Reference to the Instance
        return spawnedGameObject;
    }
}
