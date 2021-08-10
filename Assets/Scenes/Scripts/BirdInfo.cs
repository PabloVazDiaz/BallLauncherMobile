using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameController;

[CreateAssetMenu(fileName = "NewBird", menuName = "CreateNewBird")]
public class BirdInfo : ScriptableObject
{

    [SerializeField] public GameObject BirdPrefab;
    [SerializeField] public BirdType birdType;
    [SerializeField] public Sprite BirdSprite;
    [SerializeField] public int remainingBirdPoints;


}
