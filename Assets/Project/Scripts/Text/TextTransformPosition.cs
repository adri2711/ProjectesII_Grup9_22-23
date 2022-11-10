using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTransformPosition : MonoBehaviour
{
    public float TranslationMultiplier = 1.0f;
    
    // (X) This has to change according to the letter the player has just correctly typed
    public int letterIterator = 0;
    public int minLetter = 0;

    //Positive number (/10 or *10). TranslationMultiplier will increase according to the player's typing speed
    //As only one letter can move at the same time, this movement will speed up if the player types another letter
    //so the previous one disappears and the new one can start moving
    private int typingSpeed = 1;

    private TMP_Text m_TextComponent;
    private bool hasTextChanged;

    private bool mutex = false;

    private void Awake()
    {
        GameEvents.instance.enterCorrectLetter += IncreaseIterator;
        m_TextComponent = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Add(ON_TEXT_CHANGED);
    }

    void OnDisable()
    {
        TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(ON_TEXT_CHANGED);
    }

    private void Start()
    {
        StartCoroutine(TransformTextPosition());
    }

    void ON_TEXT_CHANGED(Object obj)
    {
        if (obj = m_TextComponent)
            hasTextChanged = true;
    }

    void IncreaseIterator(int p)
    {
        //// If the letters have not disappeared yet...
        //if (TranslationMultiplier < 80)
        //{
        //    TranslationMultiplier += 10 * typingSpeed;
        //}
        //else
        //{
        //    letterIterator++;
        //    minLetter++;
        //    TranslationMultiplier = 1;
        //}
        letterIterator++;
        minLetter++;
    }

    IEnumerator TransformTextPosition()
    {
        /*
        while (positionMultiplier < 300)
        {
            m_TextComponent.transform.position = gameObject.transform.position + new Vector3(0, 0.1f * positionMultiplier * Time.deltaTime, 0);
            positionMultiplier++;

            yield return new WaitForSeconds(0.01f);
        }
        */

        m_TextComponent.ForceMeshUpdate();

        TMP_TextInfo textInfo = m_TextComponent.textInfo;

        Matrix4x4 matrix;
        Vector3[][] copyOfVertices = new Vector3[0][];

        hasTextChanged = true;

        while(true)
        {
            
            // Allocate new vertices
            if(hasTextChanged)
            {
                if (copyOfVertices.Length < textInfo.meshInfo.Length)
                    copyOfVertices = new Vector3[textInfo.meshInfo.Length][];

                for (int i = 0; i < textInfo.meshInfo.Length; i++)
                {
                    int length = textInfo.meshInfo[i].vertices.Length;
                    copyOfVertices[i] = new Vector3[length];
                }

                hasTextChanged = false;
            }

            int characterCount = textInfo.characterCount;

            // If No Characters then just yield and wait for some text to be added
            if (characterCount == 0)
            {
                yield return new WaitForSeconds(0.25f);
                continue;
            }

            int lineCount = textInfo.lineCount;

            // Iterate through each line of the text
            for(int i = 0; i < lineCount; i++)
            {
                // (X) Unnecessary? We only want one letter (typed correctLetter), instead of iterating through each letter in the line
                int first = textInfo.lineInfo[i].firstCharacterIndex;
                int last = textInfo.lineInfo[i].lastCharacterIndex;

                //letterIterator = first;

                // Determine the center of each line
                Vector3 centerOfLine = (textInfo.characterInfo[first].bottomLeft + textInfo.characterInfo[last].topRight) / 2;

                // Determine translation vector
                Vector3 translation = new Vector3(0, TranslationMultiplier, 0);
                Vector3 translationDisappear = new Vector3(0, 100, 0);

                // Determine no-rotation quaternion
                Quaternion noRotation = Quaternion.Euler(0, 0, 0);

                //Iterate through each character of the line
                for(int j = first; j <= last; j++)
                {
                    //Skip characters that are not visible
                    if (!textInfo.characterInfo[j].isVisible)
                        continue;

                    // Get the index of the material used by the current character.
                    int materialIndex = textInfo.characterInfo[j].materialReferenceIndex;

                    // Get the index of the first vertex used by this text element.
                    int vertexIndex = textInfo.characterInfo[j].vertexIndex;

                    // Get the vertices of the mesh used by this text element (character or sprite).
                    Vector3[] sourceVertices = textInfo.meshInfo[materialIndex].vertices;

                    // Need to translate all 4 vertices of each quad to aligned with center of character.
                    // This is needed so the matrix TRS is applied at the origin for each character.
                    copyOfVertices[materialIndex][vertexIndex + 0] = sourceVertices[vertexIndex + 0] - centerOfLine;
                    copyOfVertices[materialIndex][vertexIndex + 1] = sourceVertices[vertexIndex + 1] - centerOfLine;
                    copyOfVertices[materialIndex][vertexIndex + 2] = sourceVertices[vertexIndex + 2] - centerOfLine;
                    copyOfVertices[materialIndex][vertexIndex + 3] = sourceVertices[vertexIndex + 3] - centerOfLine;

                    // Setup the matrix translation
                    if (j >= minLetter && j <= letterIterator)
                    {
                        // Apply translation
                        matrix = Matrix4x4.TRS(translation, noRotation, Vector3.one);
                    }
                    else if (j < minLetter)
                    {
                        // Keep the letter outside the screen
                        matrix = Matrix4x4.TRS(translationDisappear, noRotation, Vector3.one);
                    }
                    else
                    {
                        // Do nothing
                        matrix = Matrix4x4.TRS(Vector3.one, noRotation, Vector3.one);
                    }

                    // Apply the matrix TRS to the individual characters relative to the center of the current line.
                    copyOfVertices[materialIndex][vertexIndex + 0] = matrix.MultiplyPoint3x4(copyOfVertices[materialIndex][vertexIndex + 0]);
                    copyOfVertices[materialIndex][vertexIndex + 1] = matrix.MultiplyPoint3x4(copyOfVertices[materialIndex][vertexIndex + 1]);
                    copyOfVertices[materialIndex][vertexIndex + 2] = matrix.MultiplyPoint3x4(copyOfVertices[materialIndex][vertexIndex + 2]);
                    copyOfVertices[materialIndex][vertexIndex + 3] = matrix.MultiplyPoint3x4(copyOfVertices[materialIndex][vertexIndex + 3]);

                    /*
                    else
                    {
                        //Do NOT apply translation
                        matrix = Matrix4x4.TRS(Vector3.one, noRotation, Vector3.one);
                    }
                    */


                }
            }

            // Push changes into meshes
            for(int i = 0; i < textInfo.meshInfo.Length; i++)
            {
                textInfo.meshInfo[i].mesh.vertices = copyOfVertices[i];
                m_TextComponent.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
            }

            //Update TranslationMultiplier
            //TranslationMultiplier += 2;

            yield return new WaitForSeconds(0.01f);
        }
    }

}
