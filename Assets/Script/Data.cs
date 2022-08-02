using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    //Variables 
    private static Data _instance; //Is the instance of the object that will show up in each scene 
    public int score = 0;

    //==================================================================================================================
    // Base Functions 
    //==================================================================================================================

    //Creates the object, if one already has been created in another scene destroy this one and the make a new one 
    private void Awake()
    {
        //Checks if there already exits a copy of this, if does destroy it and let the new one be created 
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //==================================================================================================================
    // Data Update Methods 
    //==================================================================================================================

    //If player collected a fruit this will update the value to true
    public void SetScore(int s)
    {
        score = s;
    }

    //Returns the list of collected fruits, used in start of each level and check in the end scene 
    public int GetScore()
    {
        return score;
    }
}
