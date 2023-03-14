using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public CanvasGroup gameOver;
    public TileBoard board;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestText;
    private int score;
    private void Start(){
        gameOver.alpha = 0f;
        NewGame();
        
    
    }

    public void NewGame(){
        StartCoroutine(Fade(gameOver, 0f,0f));
        board.ClearBoard();
        board.CreateTile(1);
        board.CreateTile(1);
        board.enabled = true;
        board.maxNumber = 1;
        board.mergeCount = 0;
        SetScore(2);
    }

    public void GameOver(){
        board.enabled = false;
        
        StartCoroutine(Fade(gameOver, 1f,1f));
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay){
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while(elapsed < duration){
            canvasGroup.alpha = Mathf.Lerp(from,to,elapsed/duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void SetScore(int score){
        this.score = score;
        scoreText.text = score.ToString();
        SaveHighScore(score);
        bestText.text = LoadHighScore().ToString();
    }
    
    private void SaveHighScore(int score){
        int tmpScore = LoadHighScore();
        if(score > tmpScore){
            PlayerPrefs.SetInt("HighScore",score);
        }
    }

    private int LoadHighScore(){
        return PlayerPrefs.GetInt("HighScore",0);
    }
    
}
