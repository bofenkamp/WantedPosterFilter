using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class UIScaler : MonoBehaviour
{
    [SerializeField] private RectTransform wanted;
    [SerializeField] private RectTransform nameText;
    [SerializeField] private RectTransform offense;
    [SerializeField] private RectTransform inputText;
    //dimensions from 0 to 1, showing amount of screen covered
    [SerializeField] private Vector2 wantedDimensions;
    [SerializeField] private Vector2 nameDimensions;
    [SerializeField] private Vector2 offenseDimensions;
    [SerializeField] private Vector2 inputDimensions;
    [SerializeField] private float margin;

    [SerializeField] private Vector2 defaultSize;

    private void Start()
    {
        RectTransform canvas = GetComponent<RectTransform>();
        float w = canvas.sizeDelta.x;
        float h = canvas.sizeDelta.y;

        wanted.sizeDelta = new Vector3(wantedDimensions.x * w, wantedDimensions.y * h, 1);
        nameText.sizeDelta = new Vector3(nameDimensions.x * w, nameDimensions.y * h, 1);
        offense.sizeDelta = new Vector3(offenseDimensions.x * w, offenseDimensions.y * h, 1);
        inputText.sizeDelta = new Vector3(inputDimensions.x * w, inputDimensions.y * h, 1);

        nameText.localPosition = new Vector3(0, h / 2 - wanted.sizeDelta.y, 0);
        offense.localPosition = new Vector3(0, (h * 9 / 16) * margin - h / 2, 0);

        float scalar = h / defaultSize.y;
        TextMeshProUGUI[] texts = new TextMeshProUGUI[]
        {
            wanted.GetComponent<TextMeshProUGUI>(),
            nameText.GetComponent<TextMeshProUGUI>(),
            offense.GetComponent<TextMeshProUGUI>(),
            inputText.GetComponent<TextMeshProUGUI>()
        };
        foreach (TextMeshProUGUI text in texts)
        {
            text.fontSizeMax *= scalar;
            text.fontSizeMin *= scalar;
        }
    }
}
