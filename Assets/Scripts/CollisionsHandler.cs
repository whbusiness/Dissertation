using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionsHandler : MonoBehaviour
{
    public static int amountCollidedWith;
    [SerializeField]
    private Prefabs thisSpawnableObject;
    private ScoreTxt scoreTxt;
    private AudioSource[] source;
    private bool collisionDetected = false;
    public static DateTime timeEnded;
    ArrowController arrowController;
    private void Awake()
    {
        amountCollidedWith = 0;
        collisionDetected = false;
        source = GameObject.FindGameObjectWithTag("CollisionSFX").GetComponents<AudioSource>();
        scoreTxt = FindObjectOfType<ScoreTxt>();
        arrowController = FindObjectOfType<ArrowController>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == gameObject.name)
        {
            print(amountCollidedWith);
            amountCollidedWith++;
            if (amountCollidedWith >= 2)
            {
                if (!source[1].isPlaying)
                {
                    print("Play SFX");
                    source[1].Play();
                }
                scoreTxt.OnScoreUpdate(thisSpawnableObject.scoreIncrease);
                if(collision.gameObject.name != "Watermelon")
                {
                    thisSpawnableObject.SpawnCollisionFoodType(gameObject.transform.position);
                }
            }
            if (amountCollidedWith <= 2)
            {
                Destroy(gameObject);
            }
        }
        if (!collision.gameObject.CompareTag("Bounds"))
        {
            collisionDetected = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Bounds"))
        {
            collisionDetected = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LoseTrigger") && collisionDetected)
        {
            if (arrowController._kr.IsRunning)
            {
                arrowController._kr.Stop();
            }
            arrowController._kr.Dispose();
            arrowController._kr = null;
            timeEnded = DateTime.Now;
            SceneManager.LoadScene("LostScene");
        }
    }
}
