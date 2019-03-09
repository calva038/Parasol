using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScriptTest : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public float whichOption;

    // Update is called once per frame
    void Update()
    {
        if (whichOption == 1)
            Text.text = ParserXML.optionOneObject;
        else if (whichOption == 2)
            Text.text = ParserXML.optionTwoObject;
        else if (whichOption == 3)
            Text.text = ParserXML.optionThreeObject;
        else if (whichOption == 4)
            Text.text = ParserXML.optionFourObject;
    }
}
