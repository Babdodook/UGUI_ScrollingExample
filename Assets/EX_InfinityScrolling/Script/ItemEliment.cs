using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEliment : MonoBehaviour
{
    [Header("자료")]
    public Text label = null;
    public Text score = null;
    public Image icon = null;

    [Header("데이터 값")]
    public int ScoreValue = -1;
    

    public int ScoreVal
    {
        get { return int.Parse(score.text); }
        set { score.text = value.ToString(); }
    }

    public void UpdateScore(int index)
    {
        ScoreValue = index;
        score.text = index.ToString();
    }

    public void UpdateInfo(Sprite _sprite)
    {
        icon.sprite = _sprite;
        label.text = _sprite.name;
    }    
}
