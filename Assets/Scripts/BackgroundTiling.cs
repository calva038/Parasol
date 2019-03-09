using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class BackgroundTiling : MonoBehaviour
{
    public int offsetX = 2;
    public bool hasARightTile = false;
    public bool hasALeftTile = false;
    public bool reverseScale = false;
    private float spriteWidth = 0f;
    private Camera cam;
    private Transform myTransform;

    private void Awake()
    {
        cam = Camera.main;
        myTransform = transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasALeftTile == false || hasARightTile == false)
        {
            float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;
            float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth / 2) - camHorizontalExtend;
            if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightTile == false)
            {
                MakeNewTitle(1);
                hasARightTile = true;
            }
            else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftTile == false)
            {
                MakeNewTitle(-1);
                hasALeftTile = true;
            }
        }
    }

    void MakeNewTitle (int rightOrLeft)
    {
        Vector3 newPosition = new Vector3 (myTransform.position.x + myTransform.localScale.x * spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        Transform newTile = (Transform)Instantiate (myTransform, newPosition, myTransform.rotation);
        if (reverseScale == true)
        {
            newTile.localScale = new Vector3(newTile.localScale.x * -1, newTile.localScale.y, newTile.localScale.z);
        }
        newTile.parent = myTransform;
        if (rightOrLeft > 0)
        {
            newTile.GetComponent<BackgroundTiling>().hasALeftTile = true;
        }
        else
        {
            newTile.GetComponent<BackgroundTiling>().hasARightTile = true;
        }
    }
}
