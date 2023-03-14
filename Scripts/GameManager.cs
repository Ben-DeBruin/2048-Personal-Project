using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CanvasGroup gameOver;
    public TileBoard board;
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
    
}
