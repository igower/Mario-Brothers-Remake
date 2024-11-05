using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coins;
    public TextMeshProUGUI level;
    public TextMeshProUGUI time;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = GameManagement.Instance.score.ToString("000000");
        coins.text = "x" + GameManagement.Instance.coins.ToString("00");
        level.text = GameManagement.Instance.GetLevel();
        time.text = GameManagement.Instance.time.ToString("000");
    }
}
