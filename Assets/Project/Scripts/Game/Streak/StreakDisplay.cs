using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class StreakDisplay : MonoBehaviour
{
    private StreakMultipartText streakText;
    private TextMeshProUGUI streakTMP;
    void Start()
    {
        streakTMP = GetComponentInChildren<TextMeshProUGUI>();

        streakText = new StreakMultipartText();
        streakText.Setup();
    }

    public void UpdateScore(int streak)
    {
        streakText.UpdateScore(streak);
    }
    private void Update()
    {
        streakTMP.text = streakText.GetFullFormattedText();
    }


}
