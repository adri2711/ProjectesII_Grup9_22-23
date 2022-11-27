using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    //INTERNAL VARIABLES (only used in this script)
    public List<Sentence> rawSentences = new List<Sentence>();
    public List<Sentence> completedText = new List<Sentence>();

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

    //RULES (changed each level with: TextGenerator.instance.Setup() ) 
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


    //Setup (receives rules values as parameters)
    public void Setup(int textSentenceCount, int keywordRep, List<float> sentenceTypeChances, List<float> writingStyleChances, bool allowSentenceRep)
    {
        //Get rawSentences from json

        usedSentencesCount = textSentenceCount;
        totalSentencesCount = rawSentences.Count;


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

        keywordRepetition = keywordRep;


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

    //Generate list of sentences according to rules
    public void GenerateListOfSentences()
    {
        //GENERATE N SENTENCES BY TYPE
        for (int i = 0; i < usedSentencesCount; i++)
        {
            //Generate a sentence type for each used sentence, according to SentenceType chances

            //For each used sentence, now generate a writing style, according to WritingStyle chances AND SentenceType constraints

            //Add the new sentence to completedText list
        }

    }

}
