using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class Tile : MonoBehaviour
{
    public TileState state {get; private set;}
    public TileCell cell {get; private set;}
    public bool locked{get; set;}
    public int number{get; private set;}
    public Vector3 canonicalSize;
    private Image background;
    private TextMeshProUGUI text;

    private void Awake(){
        background = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        canonicalSize = transform.localScale;
    }

    public void SetState(TileState state, int number){

        this.state = state;
        this.number = number;

        background.color = state.backgroundColor;
        text.color = state.textColor;
        text.text = number.ToString();

    }

    public void Spawn(TileCell cell){
        if(this.cell != null){//If this tile is being reassigned for some reason, remove it from the cell it occupies.
            this.cell.tile = null;
        }
        this.cell = cell;
        this.cell.tile = this;
        transform.position = cell.transform.position;
        canonicalSize = transform.localScale;
        StartCoroutine(Scale(0f,1f));
    }

    public void MoveTo(TileCell cell){ //Move to destination
        
        if(this.cell != null){
            this.cell.tile = null;
        }
        this.cell = cell;
        this.cell.tile = this;
        StartCoroutine(Animate(cell.transform.position,false));
        
    }

    public void MergeTo(TileCell cell){
        
        this.cell.tile = null;
        
        StartCoroutine(Animate(cell.transform.position,true));
        StartCoroutine(Scale(1,0));

    }


    private IEnumerator Animate(Vector3 to, bool merging){
        float elapsed = 0f;
        float duration = 0.1f;

        Vector3 from = transform.position;
        while(elapsed <duration){
            transform.position = Vector3.Lerp (from, to, elapsed/duration); //Move from "from" to "to" by an amount equal to the percent of the overall move
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = to;

        if(merging){Destroy(gameObject);}
    }

    private IEnumerator Scale(float from, float to){
        float elapsed = 0f;
        float duration = 0.15f;

        Vector3 start = new Vector3(canonicalSize.x * from, canonicalSize.y * from, canonicalSize.z *from);
        Vector3 finish = new Vector3(canonicalSize.x * to, canonicalSize.y * to, canonicalSize.z * to);
        

        while(elapsed < duration){
            transform.localScale = Vector3.Lerp(start,finish,elapsed/duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = finish;
         
    }
}
