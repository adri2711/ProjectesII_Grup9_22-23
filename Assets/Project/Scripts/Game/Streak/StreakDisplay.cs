using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StreakDisplay : MonoBehaviour
{
    private StreakMultipartText streakText;
    private TextMeshProUGUI streakTMP;
    [SerializeField] private Transform scoreDisplay;
    [SerializeField] private GameObject[] scoreSpawnPoints;

    [SerializeField] private float sizeVariance = 0.5f;
    [SerializeField] private Vector2 spawnVariance = new Vector2(15f,30f);
    [SerializeField] private float rotationVariance = 30f;
    [SerializeField] private float lifeTimeVariance = 0.5f;
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
    public void ScoreIncreaseDisplay(int add)
    {
        GameObject root = scoreSpawnPoints[Random.Range(0, scoreSpawnPoints.Count())];

        if (root != null)
        {
            Transform scoreTransform = Instantiate(scoreDisplay, new Vector2(0, 0), Quaternion.identity, root.transform);
            scoreTransform.gameObject.GetComponent<PointText>().Setup(add, Random.Range(1.5f - sizeVariance, 1.5f + sizeVariance), Random.Range(2f - lifeTimeVariance, 2f + lifeTimeVariance));

            Vector2 scorePos = root.transform.position;
            scorePos += new Vector2(Random.Range(-spawnVariance.x, spawnVariance.x), Random.Range(-spawnVariance.y, spawnVariance.y));

            scoreTransform.gameObject.GetComponent<PointText>().SetPosition(scorePos, Random.Range(-rotationVariance, rotationVariance));
        }
    }
    private void Update()
    {
        streakTMP.text = streakText.GetFullFormattedText();
    }


}
