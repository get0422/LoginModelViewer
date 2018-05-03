using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListButton : MonoBehaviour {

    // Public Variables
    public Button button;
    public Text label;

    // Private Variables
    private Model model;
    private ModelScrollList scrollList;

	// Sets Everything Up
	public void Setup (Model currentModel, ModelScrollList currentScrollList) {
        // Sets Model and Text
        model = currentModel;
        label.text = model.gameModel.name;

        // Sets Object List
        scrollList = currentScrollList;

        // Sets OnButton Click Event for Object
        button.onClick.AddListener(Task);
    }

    private void Task()
    {
        // Goes through the Model List
        for (int i = 0; i < scrollList.modelList.Count; i++)
        {
            // Checks if they are Active
            if(scrollList.modelList[i].isActive == true)
                scrollList.modelList[i].isActive = false;

            // Turns Any Active Objects off
            scrollList.modelList[i].gameModel.SetActive(scrollList.modelList[i].isActive);
        }

        // Sets current Model to Active Model
        model.isActive = true;
        model.gameModel.SetActive(model.isActive);

        // Sends Model List to PlayerLog
        PlayerLog playerLog = GameObject.Find("UserLogPanel").GetComponent<PlayerLog>();
        playerLog.UpdateModelList(scrollList.modelList);
    }
}
