using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextGenTester : MonoBehaviour
{
    public static string pathPart2 = "/StreamingAssets/Data/RawSentences/level4RawSentences.json";
    List<float> sentenceTypeChances = new List<float>();
    List<float> writingStyleChances = new List<float>();


    // Start is called before the first frame update
    void Start()
    {
        sentenceTypeChances.Add(0.2f);
        sentenceTypeChances.Add(0.2f);
        sentenceTypeChances.Add(0.2f);
        sentenceTypeChances.Add(0.0f);
        sentenceTypeChances.Add(0.1f);
        sentenceTypeChances.Add(0.0f);
        sentenceTypeChances.Add(0.0f);
        sentenceTypeChances.Add(0.1f);
        sentenceTypeChances.Add(0.0f);
        sentenceTypeChances.Add(0.2f);

        writingStyleChances.Add(0.2f);
        writingStyleChances.Add(0.2f);
        writingStyleChances.Add(0.1f);
        writingStyleChances.Add(0.1f);
        writingStyleChances.Add(0.2f);
        writingStyleChances.Add(0.2f);
        writingStyleChances.Add(0.0f);

        TextGenerator.instance.Setup(Application.dataPath + pathPart2, 10, 1, sentenceTypeChances, writingStyleChances, true);
        TextGenerator.instance.GenerateListOfSentences("4");
    }
}
