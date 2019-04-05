using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSpeakingImage : MonoBehaviour
{
    public RawImage ObjectwithImage;
    public Texture Expression1;
    public Texture Expression2;

    void Start()
    {
        ObjectwithImage.texture = Expression1;
    }

    void Update()
    {
        if (ParserXML.whichConvo == 0)
        {
            ObjectwithImage.texture = Expression1;
        }
        else if (ParserXML.whichConvo == 1)
        {
            ObjectwithImage.texture = Expression2;
        }
    }
}
