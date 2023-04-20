using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawningInfo
{
    public int Id;
    public float timeToSpawn;
    public int rotationSegment;
    public string spawnSide;

    public SpawningInfo(int id,float spawntime,int rotseg, string spawnside) 
    {
        Id = id;
        timeToSpawn = spawntime;
        rotationSegment = rotseg;
        spawnSide = spawnside;
    }
}

[System.Serializable]
public class SpawningInfoList 
{
    public List<SpawningInfo> list;
}
