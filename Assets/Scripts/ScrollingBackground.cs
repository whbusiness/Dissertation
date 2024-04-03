using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField]
    private float xMovement, yMovement;
    [SerializeField]
    private RawImage _rawImage;
    // Update is called once per frame
    void Update()
    {
        _rawImage.uvRect = new Rect(_rawImage.uvRect.position + new Vector2(xMovement, yMovement) * Time.deltaTime, _rawImage.uvRect.size);
    }
}
