using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum SentenceType
{
    COMPLETE,          //Subjecte + predicat 
    PREDICATE,         //(No impersonal)
    SUBJECT,
    IMPERSONAL,
    QUESTION,
    ANSWER,
    VERB_OBJECT,        //Complement del verb (fa referència a l'enunciat anterior)
    NOUN_OBJECT,        //Complement del nom (fa referència a l'enunciat anterior)
    NOUN_PHRASE,        //Sintagma nominal (independent de l'enunciat anterior)
    PLAYER_ATTENTION    //Directly references the player in order to grab their attention
}

[System.Serializable]
public enum WritingStyle
{
    BASIC,                          //"Normal"/ basic Sentence
    LONG_WITH_COMMAS,               //Sentence longer than usual, with multiple commas
    SINGLE_WORD,                    //Can be a noun, a verb, an adjective...
    ARISTOTELIAN_PROPOSITION,       //S is P
    MIDDLE_COLON,                   //SentencePart1 : sentencePart2.
    MIDDLE_SEMICOLON,               //SentencePart1 ; sentencePart2.
    COORDINATING_CONJUNCTION_HALF   //Needs another Sentence. Together they will be: Sentence1. Or/but/because/etc. sentence 2.

}

[System.Serializable]
public class SentenceCollection
{
    public Sentence[] sentenceArray;
    public List<Sentence> value = new List<Sentence>();
}

[System.Serializable]
public class Sentence : MonoBehaviour
{
    //Internal logic
    //public string text;       //TextPart already has a text variable
    public string keyword;
    public SentenceType sentenceType;
    public WritingStyle writingStyle;

    public bool alreadyInText;

    public TextPart textPart;

    //POSAR-LI COLOR --> Per poder fer constructor de text part
    //QUE CADA SENTENCE TINGUI COM A VARIABLE PÚBLICA UNA TEXTPART --> AIXÍ ÉS MÉS FÀCIL FER EL JSON

    //Interaction with TextPart, MultipartText, etc. (game json & format system)
    //public TextPart textPart = new TextPart();
}
