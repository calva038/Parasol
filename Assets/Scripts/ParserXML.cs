using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using TMPro;

public class ParserXML : MonoBehaviour
{
    internal static string optionOneObject = "Option 1";
    internal static string optionTwoObject = "Option 2";
    internal static string optionThreeObject = "Option 3";
    internal static string optionFourObject = "Option 4";
    public string optionOne;
    public string optionTwo;
    public string optionThree;
    public string optionFour;
    public static int whichConvo;
    public static List<Dictionary<string, string>> allTextDic;
    // Use this for initialization
    void Start()
    {
        allTextDic = parseFile();
    }

    public List<Dictionary<string, string>> parseFile()
    {
        TextAsset txtXmlAsset = Resources.Load<TextAsset>("test");
        var doc = XDocument.Parse(txtXmlAsset.text);
        var allDict = doc.Element("Dialogue").Elements("English").Elements("LevelOne").Elements("Lily").Elements("Conversation").Elements("Section");
        List<Dictionary<string, string>> allTextDic = new List<Dictionary<string, string>>();
        foreach (var oneDict in allDict)
        {
            var fourStrings = oneDict.Elements("Option");
            XElement element1 = fourStrings.ElementAt(0);
            XElement element2 = fourStrings.ElementAt(1);
            XElement element3 = fourStrings.ElementAt(2);
            XElement element4 = fourStrings.ElementAt(3);
            optionOne = element1.ToString().Replace("<Option>", "").Replace("</Option>", "");
            optionTwo = element2.ToString().Replace("<Option>", "").Replace("</Option>", "");
            optionThree = element3.ToString().Replace("<Option>", "").Replace("</Option>", "");
            optionFour = element4.ToString().Replace("<Option>", "").Replace("</Option>", "");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("Option One", optionOne);
            dic.Add("Option Two", optionTwo);
            dic.Add("Option Three", optionThree);
            dic.Add("Option Four", optionFour);

            allTextDic.Add(dic);
        }
        //optionOneObject = optionOne;
        return allTextDic;

    }

    // Update is called once per frame
    void Update()
    {
        optionOneObject = allTextDic[whichConvo]["Option One"];
        optionTwoObject = allTextDic[whichConvo]["Option Two"];
        optionThreeObject = allTextDic[whichConvo]["Option Three"];
        optionFourObject = allTextDic[whichConvo]["Option Four"];
    }
}
