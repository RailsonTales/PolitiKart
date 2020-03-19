using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
    public static GameManager instance = null;

    [SerializeField] private int playerScore = 0;

    private void Awake(){
        if(instance != null){
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(instance);
    }

    public void AddScore(int amount)
    {
        playerScore += amount;
    }

    public void RemoveScore(int amount)
    {
        playerScore -= amount;
    }
}