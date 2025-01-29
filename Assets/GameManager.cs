using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //This is the only class that should actually be a singleton
    public static GameManager Instance { get; private set; } //this class should be a singleton
    private void Awake()
    {
        //Singleton enforcing
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            //This object (and its children) should not be destroyed upon loading into a new scene
            Instance = this;
            DontDestroyOnLoad(gameObject);   
        }
    }
}
