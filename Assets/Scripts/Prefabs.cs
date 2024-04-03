using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class Prefabs : ScriptableObject
{
    public GameObject foodType;
    public int scoreIncrease;
    public new string name;
    [SerializeField]
    private Prefabs collisionFoodTypeSpawn;
    [SerializeField]
    private int yLocationOnCollisionSpawn;
    public void SpawnPrefab(Vector3 arrowLocation)
    {
        GameObject pref = Instantiate(foodType, arrowLocation, Quaternion.identity);
        pref.name = name;
    }

    public void SpawnCollisionFoodType(Vector3 currentPosition)
    {
        collisionFoodTypeSpawn.SpawnPrefab(currentPosition + new Vector3(0,yLocationOnCollisionSpawn));
    }
}
