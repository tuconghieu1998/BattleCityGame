using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    GameObject[] tanks;
    GameObject tank;
    [SerializeField]
    bool isPlayer;
    [SerializeField]
    GameObject smallTank, fastTank, bigTank, armoredTank;
    enum tankType
    {
        smallTank, fastTank, bigTank, armoredTank
    };
    private void Start()
    {
        if (isPlayer)
        {
            tanks = new GameObject[1] { smallTank };
        }
        else
        {
            tanks = new GameObject[4] { smallTank, fastTank, bigTank, armoredTank };
        }
    }
    public void StartSpawning()
    {
        if (!isPlayer)
        {
            List<int> tankToSpawn = new List<int>();
            tankToSpawn.Clear();
            if (LevelManager.smallTanks > 0) tankToSpawn.Add((int)tankType.smallTank);
            if (LevelManager.fastTanks > 0) tankToSpawn.Add((int)tankType.fastTank);
            if (LevelManager.bigTanks > 0) tankToSpawn.Add((int)tankType.bigTank);
            if (LevelManager.armoredTanks > 0) tankToSpawn.Add((int)tankType.armoredTank);
            int tankID = tankToSpawn[Random.Range(0, tankToSpawn.Count)];
            tank = Instantiate(tanks[tankID], transform.position, transform.rotation);
            if (tankID == (int)tankType.smallTank) LevelManager.smallTanks--;
            else if (tankID == (int)tankType.fastTank) LevelManager.fastTanks--;
            else if (tankID == (int)tankType.bigTank) LevelManager.bigTanks--;
            else if (tankID == (int)tankType.armoredTank) LevelManager.armoredTanks--;
        }
        else
        {
            tank = Instantiate(tanks[0], transform.position, transform.rotation);
        }
    }
    public void SpawnNewTank()
    {
        if (tank != null) tank.SetActive(true);
    }
}