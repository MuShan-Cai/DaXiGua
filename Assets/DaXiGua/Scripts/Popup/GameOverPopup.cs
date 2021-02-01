using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOverPopup : Popup
{
     TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        textMesh = transform.Find("Score").GetComponent<TextMeshProUGUI>();
        textMesh.text = ScoreContro.Instance.Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void Close()
    {
        base.Close();
        GameManager.Instance.MyGameState = GameManager.GameState.Gaming;
    }

}
