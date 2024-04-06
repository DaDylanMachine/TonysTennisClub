using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Arrays that store the objectives and their respective difficulties. Contents are added in Unity under the GameManager GameObject.
    public GameObject[] objectivesArrayEasy;
    public GameObject[] objectivesArrayMedium;
    public GameObject[] objectivesArrayHard;
    // GameObject variable to connect to the checklist UI.
    public GameObject gridContent;
    // GameObject variable used to store an objective so it can be formatted correctly.
    GameObject newObjective;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTasks();
    }

    // Function that spawns the objectives and adds them to the checklist when the game starts.
    // WILL BE MADE RANDOM FOR FULL GAME, BUT THIS IS HOW IT WILL LOOK FOR THE DEMO.
    void SpawnTasks()
    {
        // Spawns the objective inside the array.
        newObjective = Instantiate(objectivesArrayEasy[0]);
        // Places the objective as a child of the list so it can be formatted correctly.
        newObjective.transform.SetParent(gridContent.GetComponent<Transform>());
        // Makes the objective the correct scale.
        newObjective.transform.localScale = new Vector3(1, 1, 1);

        // Repeat the last set of code for the remaining objectives.
        newObjective = Instantiate(objectivesArrayEasy[1]);
        newObjective.transform.SetParent(gridContent.GetComponent<Transform>());
        newObjective.transform.localScale = new Vector3(1, 1, 1);
        newObjective = Instantiate(objectivesArrayHard[0]);
        newObjective.transform.SetParent(gridContent.GetComponent<Transform>());
        newObjective.transform.localScale = new Vector3(1, 1, 1);
        newObjective = Instantiate(objectivesArrayHard[1]);
    }
}
