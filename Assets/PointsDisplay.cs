using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsDisplay : MonoBehaviour
{
    [SerializeField]
    TMP_Text pointsTotalText;

    void Start()
    {
        pointsTotalText.text = ShipController.points.ToString();
    }
}
