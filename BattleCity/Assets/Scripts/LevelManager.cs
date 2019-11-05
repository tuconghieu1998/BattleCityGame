using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    int smallTanksInThisLevel, fastTanksInThisLevel, bigTanksInThisLevel, armoredTanksInThisLevel, stageNumber;
    public static int smallTanks, fastTanks, bigTanks, armoredTanks;
    [SerializeField]
    float spawnRateInThisLevel = 5;       //newly added
    public static float spawnRate { get; private set; } //newly added
    private void Awake()
    {
        MasterTracker.stageNumber = stageNumber;
        smallTanks = smallTanksInThisLevel;
        fastTanks = fastTanksInThisLevel;
        bigTanks = bigTanksInThisLevel;
        armoredTanks = armoredTanksInThisLevel;
        spawnRate = spawnRateInThisLevel; //newly added
    }
}