using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public int Score;
    List<Pig> pigs = new List<Pig>();
    Queue<BirdType> availableBirds = new Queue<BirdType>();

    [SerializeField]
    List<BirdInfo> possibleBirds = new List<BirdInfo>();
    [SerializeField]
    GameObject FinalLevelPanel;

    public enum BirdType
    {
        Normal,
        Frag,
        Rocket
    }
    
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        pigs.AddRange(FindObjectsOfType<Pig>());
        foreach (Pig pig in pigs)
        {
            pig.OnDeath += OnEnemyDeath;
        }
       
        availableBirds.Enqueue(BirdType.Normal);
        availableBirds.Enqueue(BirdType.Frag);
        availableBirds.Enqueue(BirdType.Normal);

    }
 

    private void OnEnemyDeath(int points, Pig deadPig)
    {
        Score += points;
        UIController.instance.ChangeScore(Score);
        pigs.Remove(deadPig);
        if (pigs.Count < 1)
            LevelOver();
    }

    public GameObject NextBird()
    {
        //UI bird change
        if (availableBirds.Count > 0)
        {
            BirdType typeToFind = availableBirds.Dequeue();
            return possibleBirds.Find(x => x.birdType == typeToFind).BirdPrefab;
        }
        else
        {
            LevelOver();
            return null;
        }

    }

    private void LevelOver()
    {
        while (availableBirds.Count > 0)
        {
            BirdType typeToFind = availableBirds.Dequeue();
            Score += possibleBirds.Find(x => x.birdType == typeToFind).remainingBirdPoints;
        }
        //Show Final Panel
        FinalLevelPanel.SetActive(true);
        Debug.Log("Cest Fini!");
    }
}
