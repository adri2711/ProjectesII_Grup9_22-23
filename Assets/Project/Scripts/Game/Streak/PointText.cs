using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointText : MonoBehaviour
{
    private TextMeshProUGUI pointTMP;
    private MultipartText pointText;
    private float size = 1f;
    private Color textColor = new Color(0.3f,0.7f,1f);
    private Color numberColor = new Color(0.4f,0.8f,1);
    [SerializeField] private AnimationCurve sizeCurve;
    private float lifeTime = 4f;
    private float time;
    public void Setup(int points, float size = 1f, float lifeTime = 4f)
    {
        pointTMP = GetComponent<TextMeshProUGUI>();
        pointText = new MultipartText();
        pointText.AddPart(new TextPart("number", points.ToString(), numberColor, 0, "jbm"),0);
        pointText.AddPart(new TextPart("text", "+", textColor, 0, "jbm"), 0);
        this.size = size * pointTMP.fontSize;
        this.lifeTime = lifeTime;
        time = 0;

        pointTMP.text = pointText.GetFullFormattedText();

        Destroy(this.gameObject, lifeTime);
    }
    void Update()
    {
        if (pointTMP != null)
        {
            pointTMP.fontSize = size * sizeCurve.Evaluate(time / lifeTime);
            time += Time.deltaTime;
        }
    }
    public void SetPosition(Vector3 newPos, float rotation)
    {
        transform.localPosition = newPos;
        transform.localRotation = Quaternion.Euler(0, 0, rotation);
    }
}
