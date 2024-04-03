using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayNextObject : MonoBehaviour
{
    [SerializeField]
    private GameObject[] nextObject;
    private List<GameObject> activeGO = new();
    // Update is called once per frame
    public void DisplayObject()
    {
        StopDisplayingPrev();
        switch (ArrowController.spawnObject)
        {
            case 0://spawn soda
                nextObject[0].SetActive(true);
                activeGO.Add(nextObject[0]);
                break;
            case 1://spawn carrot
                nextObject[1].SetActive(true);
                activeGO.Add(nextObject[1]);
                break;
            case 2://spawn apple
                nextObject[2].SetActive(true);
                activeGO.Add(nextObject[2]);
                break;
            case 3://spawn doughnut
                nextObject[3].SetActive(true);
                activeGO.Add(nextObject[3]);
                break;
            case 4://spawn burger
                nextObject[4].SetActive(true);
                activeGO.Add(nextObject[4]);
                break;
        }
    }
    void StopDisplayingPrev()
    {
        foreach(var obj in activeGO)
        {
            obj.SetActive(false);
        }
        activeGO.Clear();
    }
}
