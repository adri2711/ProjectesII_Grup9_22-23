using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextSizeAnimator : MonoBehaviour
{
    TextMeshProUGUI textComponent;
    public float waitTime = 2.0f;
    public AudioSource audioSourceScream;
    public AudioSource audioSourceGoblin;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        StartCoroutine(AnimateTextSize());
    }
    private IEnumerator AnimateTextSize()
    {
        while (waitTime > -90.0f )
        {
            audioSourceGoblin.Play();
            
            textComponent.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.2f, 0, 1f);

            //Here it could increase the font size a little
       
            waitTime -= 0.1f;

            if (waitTime < 0.0f && waitTime < -1.0f)
            {
                audioSourceScream.PlayOneShot(audioSourceScream.clip);
            }

            yield return new WaitForSeconds(waitTime);
        }
        
    }
}
