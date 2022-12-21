using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
public class TextGenerator : MonoBehaviour
{
    public static TextGenerator instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    //"Texts"
    public List<Sentence> rawSentences = new List<Sentence>();
    public List<Sentence> completedText = new List<Sentence>();

    public SentenceCollection rawCollection = new SentenceCollection();

    public static TextParts completeTextParts = new TextParts();
    public static string jsonObject;

    //Sentence classification lists
    List<Sentence> completeTypeSentences = new List<Sentence>();
    List<Sentence> predicateTypeSentences = new List<Sentence>();
    List<Sentence> subjectTypeSentences = new List<Sentence>();
    List<Sentence> impersonalTypeSentences = new List<Sentence>();
    List<Sentence> questionTypeSentences = new List<Sentence>();
    List<Sentence> answerTypeSentences = new List<Sentence>();
    List<Sentence> verbObjectTypeSentences = new List<Sentence>();
    List<Sentence> nounObjectTypeSentences = new List<Sentence>();
    List<Sentence> nounPhraseTypeSentences = new List<Sentence>();
    List<Sentence> playerAttentionTypeSentences = new List<Sentence>();

    List<Sentence> basicStyleSentences = new List<Sentence>();
    List<Sentence> longWithCommasStyleSentences = new List<Sentence>();
    List<Sentence> singleWordStyleSentences = new List<Sentence>();
    List<Sentence> aristotelianPropositionStyleChances = new List<Sentence>();
    List<Sentence> middleColonStyleSentences = new List<Sentence>();
    List<Sentence> middleSemicolonStyleSentences = new List<Sentence>();
    List<Sentence> coordinatingConjunctionStyleSentences = new List<Sentence>();


    public int usedSentencesCount;
    private int totalSentencesCount;
    private List<string> sentenceKeywords;

    //RULES (changed each level with: TextGenerator.instance.Setup(...) ) 
    private int keywordRepetition;          //How many consecutive sentences with the same keyword
    
    private float completeTypeChance;       //Sentence type chances
    private float predicateTypeChance;
    private float subjectTypeChance;
    private float impersonalTypeChance;
    private float questionTypeChance;
    private float answerTypeChance;
    private float verbObjectTypeChance;
    private float nounObjectTypeChance;
    private float nounPhraseTypeChance;
    private float playerAttentionTypeChance;

    private float basicStyleChance;         //Writing style chances
    private float longWithCommasStyleChance;
    private float singleWordStyleChance;
    private float aristotelianPropositionStyleChance;
    private float middleColonStyleChance;
    private float middleSemicolonStyleChance;
    private float coordinatingConjunctionStyleChance;

    bool allowSentenceRepetition;


    //Setup (receives rules values as parameters). EACH TYPE OF CHANCES MUST SUM TO 1. type & style chances order: Drive > Code Documentation > TextGenerator
    public void Setup(string filePath, int textSentenceCount, int keywordRep, List<float> sentenceTypeChances, List<float> writingStyleChances, bool allowSentenceRep)
    {
        //GET RAW SENTENCES FROM JSON
        string jsonString = File.ReadAllText(filePath);
        Debug.Log(jsonString);
        //rawCollection = JsonUtility.FromJson<SentenceCollection>(jsonString);
        //jsonObject jsonObj = jsonObject.Parse(File.ReadAllText(@filePath));
        
        rawCollection = JsonConvert.DeserializeObject<SentenceCollection>(jsonString);

        //Falla la lectura de rawCollection
        Debug.Log(rawCollection.sentenceArray[0].textPart.text);
        rawCollection.value = rawCollection.sentenceArray.ToList();
        rawSentences = rawCollection.value;
        

        usedSentencesCount = textSentenceCount;
        totalSentencesCount = rawSentences.Count;

        Debug.Log("R.S. Count: " + rawSentences.Count);

        //CLASSIFY RAW SENTENCES BY TYPE
        for (int i = 0; i < totalSentencesCount; i++)
        {
            switch(rawSentences[i].sentenceType)
            {
                case SentenceType.COMPLETE:
                {
                    completeTypeSentences.Add(rawSentences[i]);
                    break;
                }
                case SentenceType.PREDICATE:
                {
                    predicateTypeSentences.Add(rawSentences[i]);
                    break;
                }
                case SentenceType.SUBJECT:
                {
                    subjectTypeSentences.Add(rawSentences[i]);
                    break;
                }
                case SentenceType.IMPERSONAL:
                {
                    impersonalTypeSentences.Add(rawSentences[i]);
                    break;
                }
                case SentenceType.QUESTION:
                {
                    questionTypeSentences.Add(rawSentences[i]);
                    break;
                }
                case SentenceType.ANSWER:
                {
                    answerTypeSentences.Add(rawSentences[i]);
                    break;
                }
                case SentenceType.VERB_OBJECT:
                {
                    verbObjectTypeSentences.Add(rawSentences[i]);
                    break;
                }
                case SentenceType.NOUN_OBJECT:
                {
                    nounObjectTypeSentences.Add(rawSentences[i]);
                    break;
                }
                case SentenceType.NOUN_PHRASE:
                {
                    nounPhraseTypeSentences.Add(rawSentences[i]);
                    break;
                }
                case SentenceType.PLAYER_ATTENTION:
                {
                    playerAttentionTypeSentences.Add(rawSentences[i]);
                    break;
                }
            }
        }

        //CLASSIFY RAW SENTENCES BY STYLE
        for (int i = 0; i < totalSentencesCount; i++)
        {
            switch(rawSentences[i].writingStyle)
            {
                case WritingStyle.BASIC:
                {
                    basicStyleSentences.Add(rawSentences[i]);
                    break;
                }
                case WritingStyle.LONG_WITH_COMMAS:
                {
                    longWithCommasStyleSentences.Add(rawSentences[i]);
                    break;
                }
                case WritingStyle.SINGLE_WORD:
                {
                    singleWordStyleSentences.Add(rawSentences[i]);
                    break;
                }
                case WritingStyle.MIDDLE_COLON:
                {
                    middleColonStyleSentences.Add(rawSentences[i]);
                    break;
                }
                case WritingStyle.MIDDLE_SEMICOLON:
                {
                    middleSemicolonStyleSentences.Add(rawSentences[i]);
                    break;
                }
            }        
        }

        //KEYWORDS
        List<string> sentenceKeywordsWithDupes = new List<string>();

        for (int i = 0; i < totalSentencesCount; i++)                       //Get all keywords
        {
            sentenceKeywordsWithDupes.Add(rawSentences[i].keyword);
        }

        sentenceKeywords = sentenceKeywordsWithDupes.Distinct().ToList();   //Discard all duplicate keywords

        keywordRepetition = keywordRep;                                     //Set up how many consecutive sentences with the same keyword


        //CHANCES
        completeTypeChance = sentenceTypeChances[0];
        predicateTypeChance = sentenceTypeChances[1];
        subjectTypeChance = sentenceTypeChances[2];
        impersonalTypeChance = sentenceTypeChances[3];
        questionTypeChance = sentenceTypeChances[4];
        answerTypeChance = sentenceTypeChances[5];
        verbObjectTypeChance = sentenceTypeChances[6];
        nounObjectTypeChance = sentenceTypeChances[7];
        nounPhraseTypeChance = sentenceTypeChances[8];
        playerAttentionTypeChance = sentenceTypeChances[9];

        basicStyleChance = writingStyleChances[0];
        longWithCommasStyleChance = writingStyleChances[1];
        singleWordStyleChance = writingStyleChances[2];
        aristotelianPropositionStyleChance = writingStyleChances[3];
        middleColonStyleChance = writingStyleChances[4];
        middleSemicolonStyleChance = writingStyleChances[5];
        coordinatingConjunctionStyleChance = writingStyleChances[6];


        //SENTENCE REPETITION
        allowSentenceRepetition = allowSentenceRep;
    }

    //Generate list of sentences according to rules. DOES NOT CONSIDER KEYWORD VARIABLES
    public void GenerateListOfSentences(string level)
    {
        //GENERATE N SENTENCES BY TYPE
        for (int i = 0; i < usedSentencesCount; i++)
        {
            int randomNumber;
            
            //1) Generate a sentence type for each used sentence, according to SentenceType chances
            SentenceType generatedSentenceType;
            randomNumber = Random.Range(1, 101);

            if (randomNumber <= (int)(completeTypeChance * 100))
                generatedSentenceType = SentenceType.COMPLETE;
            else if (randomNumber > (int)(completeTypeChance * 100) && randomNumber <= (int)((completeTypeChance + predicateTypeChance) * 100))
                generatedSentenceType = SentenceType.PREDICATE;
            else if (randomNumber > (int)((completeTypeChance + predicateTypeChance) * 100) && randomNumber <= (int)((completeTypeChance + predicateTypeChance + subjectTypeChance) * 100))
                generatedSentenceType = SentenceType.SUBJECT;
            else if (randomNumber > (int)((completeTypeChance + predicateTypeChance + subjectTypeChance) * 100) && randomNumber <= (int)((completeTypeChance + predicateTypeChance + subjectTypeChance + impersonalTypeChance) * 100))
                generatedSentenceType = SentenceType.IMPERSONAL;
            else if (randomNumber > (int)((completeTypeChance + predicateTypeChance + subjectTypeChance + impersonalTypeChance) * 100) && randomNumber <= (int)((completeTypeChance + predicateTypeChance + subjectTypeChance + impersonalTypeChance + questionTypeChance) * 100))
                generatedSentenceType = SentenceType.QUESTION;
            else if (randomNumber > (int)((completeTypeChance + predicateTypeChance + subjectTypeChance + impersonalTypeChance + questionTypeChance) * 100) && randomNumber <= (int)((completeTypeChance + predicateTypeChance + subjectTypeChance + impersonalTypeChance + questionTypeChance + answerTypeChance) * 100))
                generatedSentenceType = SentenceType.ANSWER;
            else if (randomNumber > (int)((completeTypeChance + predicateTypeChance + subjectTypeChance + impersonalTypeChance + questionTypeChance + answerTypeChance) * 100) && randomNumber <= (int)((completeTypeChance + predicateTypeChance + subjectTypeChance + impersonalTypeChance + questionTypeChance + answerTypeChance + verbObjectTypeChance) * 100))
                generatedSentenceType = SentenceType.VERB_OBJECT;
            else if (randomNumber > (int)((completeTypeChance + predicateTypeChance + subjectTypeChance + impersonalTypeChance + questionTypeChance + answerTypeChance + verbObjectTypeChance) * 100) && randomNumber <= (int)((completeTypeChance + predicateTypeChance + subjectTypeChance + impersonalTypeChance + questionTypeChance + answerTypeChance + verbObjectTypeChance + nounObjectTypeChance) * 100))
                generatedSentenceType = SentenceType.NOUN_OBJECT;
            else if (randomNumber > (int)((completeTypeChance + predicateTypeChance + subjectTypeChance + impersonalTypeChance + questionTypeChance + answerTypeChance + verbObjectTypeChance + nounObjectTypeChance) * 100) && randomNumber <= (int)((completeTypeChance + predicateTypeChance + subjectTypeChance + impersonalTypeChance + questionTypeChance + answerTypeChance + verbObjectTypeChance + nounObjectTypeChance + nounPhraseTypeChance) * 100))
                generatedSentenceType = SentenceType.NOUN_PHRASE;
            else
                generatedSentenceType = SentenceType.PLAYER_ATTENTION;

            //2) For each used sentence, now generate a writing style, according to WritingStyle chances AND SentenceType constraints
            WritingStyle generatedWritingStyle = WritingStyle.BASIC;
            randomNumber = Random.Range(1, 101);

            if (randomNumber <= (int)(basicStyleChance * 100))
                generatedWritingStyle = WritingStyle.BASIC;
            else if (randomNumber > (int)(basicStyleChance * 100) && randomNumber <= (int)((basicStyleChance + longWithCommasStyleChance) * 100))
            {
                if(generatedSentenceType != SentenceType.PREDICATE && generatedSentenceType != SentenceType.NOUN_OBJECT)
                generatedWritingStyle = WritingStyle.LONG_WITH_COMMAS;
            }
            else if (randomNumber > (int)((basicStyleChance + longWithCommasStyleChance) * 100) && randomNumber <= (int)((basicStyleChance + longWithCommasStyleChance + singleWordStyleChance) * 100))
            {
                if (generatedSentenceType == SentenceType.SUBJECT)
                    generatedWritingStyle = WritingStyle.SINGLE_WORD;
                else
                    generatedWritingStyle = WritingStyle.BASIC;
            }
            else if (randomNumber > (int)((basicStyleChance + longWithCommasStyleChance + singleWordStyleChance) * 100) && randomNumber <= (int)((basicStyleChance + longWithCommasStyleChance + singleWordStyleChance + aristotelianPropositionStyleChance) * 100))
            {
                if (generatedSentenceType == SentenceType.COMPLETE || generatedSentenceType == SentenceType.ANSWER || generatedSentenceType == SentenceType.PLAYER_ATTENTION)
                    generatedWritingStyle = WritingStyle.ARISTOTELIAN_PROPOSITION;
                else
                    generatedWritingStyle = WritingStyle.BASIC;
            }
            else if (randomNumber > (int)((basicStyleChance + longWithCommasStyleChance + singleWordStyleChance + aristotelianPropositionStyleChance) * 100) && randomNumber <= (int)((basicStyleChance + longWithCommasStyleChance + singleWordStyleChance + aristotelianPropositionStyleChance + middleColonStyleChance) * 100))
            {
                if (generatedSentenceType != SentenceType.PREDICATE)
                    generatedWritingStyle = WritingStyle.MIDDLE_COLON;
                else
                    generatedWritingStyle = WritingStyle.BASIC;
            }
            else if (randomNumber > (int)((basicStyleChance + longWithCommasStyleChance + singleWordStyleChance + aristotelianPropositionStyleChance + middleColonStyleChance) * 100) && randomNumber <= (int)((basicStyleChance + longWithCommasStyleChance + singleWordStyleChance + aristotelianPropositionStyleChance + middleColonStyleChance + middleSemicolonStyleChance) * 100))
            {
                if (generatedSentenceType != SentenceType.QUESTION && generatedSentenceType != SentenceType.NOUN_OBJECT)
                    generatedWritingStyle = WritingStyle.MIDDLE_SEMICOLON;
            }
            else
                generatedWritingStyle = WritingStyle.COORDINATING_CONJUNCTION_HALF;

            //3) Find all sentences in [chosen]TypeSentences which have [chosen]WritingStyle and add them to a new list
            List<Sentence> suitableSentences = new List<Sentence>();

            switch(generatedSentenceType)
            {
                case SentenceType.COMPLETE:
                {
                    for(int j = 0; j < completeTypeSentences.Count; j++)
                    {
                        if (completeTypeSentences[i].writingStyle == generatedWritingStyle)
                            suitableSentences.Add(completeTypeSentences[i]);
                    }

                    break;
                }
                case SentenceType.PREDICATE:
                {
                    for(int j = 0; j < predicateTypeSentences.Count; j++)
                    {
                        if (predicateTypeSentences[i].writingStyle == generatedWritingStyle)
                            suitableSentences.Add(predicateTypeSentences[i]);
                    }

                    break;
                }
                case SentenceType.SUBJECT:
                {
                    for(int j = 0; j < subjectTypeSentences.Count; j++)
                    {
                        if (subjectTypeSentences[i].writingStyle == generatedWritingStyle)
                            suitableSentences.Add(subjectTypeSentences[i]);
                    }

                    break;
                }
                case SentenceType.IMPERSONAL:
                {
                    for(int j = 0; j < impersonalTypeSentences.Count; j++)
                    {
                        if (impersonalTypeSentences[i].writingStyle == generatedWritingStyle)
                            suitableSentences.Add(impersonalTypeSentences[i]);
                    }

                    break;
                }
                case SentenceType.QUESTION:
                {
                    for(int j = 0; j < questionTypeSentences.Count; j++)
                    {
                        if (questionTypeSentences[i].writingStyle == generatedWritingStyle)
                            suitableSentences.Add(questionTypeSentences[i]);
                    }

                    break;
                }
                case SentenceType.ANSWER:
                {
                    for(int j = 0; j < answerTypeSentences.Count; j++)
                    {
                        if (answerTypeSentences[i].writingStyle == generatedWritingStyle)
                            suitableSentences.Add(answerTypeSentences[i]);
                    }

                    break;
                }
                case SentenceType.VERB_OBJECT:
                {
                    for(int j = 0; j < verbObjectTypeSentences.Count; j++)
                    {
                        if (verbObjectTypeSentences[i].writingStyle == generatedWritingStyle)
                            suitableSentences.Add(verbObjectTypeSentences[i]);
                    }

                    break;
                }
                case SentenceType.NOUN_OBJECT:
                {
                    for(int j = 0; j < nounObjectTypeSentences.Count; j++)
                    {
                        if (nounObjectTypeSentences[i].writingStyle == generatedWritingStyle)
                            suitableSentences.Add(nounObjectTypeSentences[i]);
                    }

                    break;
                }
                case SentenceType.NOUN_PHRASE:
                {
                    for(int j = 0; j < nounPhraseTypeSentences.Count; j++)
                    {
                        if (nounPhraseTypeSentences[i].writingStyle == generatedWritingStyle)
                            suitableSentences.Add(nounPhraseTypeSentences[i]);
                    }

                    break;
                }
                case SentenceType.PLAYER_ATTENTION:
                {
                    for(int j = 0; j < playerAttentionTypeSentences.Count; j++)
                    {
                        if (playerAttentionTypeSentences[i].writingStyle == generatedWritingStyle)
                            suitableSentences.Add(playerAttentionTypeSentences[i]);
                    }

                    break;
                }
            }

            //4) Generate a random index for the suitableSentences list
            randomNumber = Random.Range(0, suitableSentences.Count);

            //5) Add the new sentence to completedText list
            Debug.Log(suitableSentences.Count);
            Debug.Log(randomNumber);
            completedText.Add(suitableSentences[randomNumber]);

            //6) Reset suitableSentences
            suitableSentences.Clear();
        }


        //7) Save each Sentence's TextPart to a general TextParts
        for (int i = 0; i < completedText.Count; i++)
        {
            completeTextParts.partArray[i] = completedText[i].textPart;
        }

        //8) Write to JSON
        JsonUtility.ToJson(jsonObject);
        System.IO.File.WriteAllText(Application.dataPath + "/StreamingAssets/Data/GeneratedTexts/generatedTextLevel" + level + ".json", jsonObject);

        //WRITE COMPLETED TEXT TO JSON
        //ONLY has the sentences and the basic info that not-random files have
        //This file can be read by the existent system --> only change the path/name of the file in the scripts that get level data from json

    }

}