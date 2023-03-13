using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    private TileGrid grid;
    private List<Tile> tiles;
    private bool waiting;
    public Tile tilePrefab;
    public TileState[] tileStates;
    private void Awake(){
        grid = GetComponentInChildren<TileGrid>();
        tiles = new List<Tile>();

    }

    private void Start(){
        
        CreateTile(1);
        CreateTile(1);
        CreateTile(1);
        CreateTile(1);
        CreateTile(1);
        CreateTile(1);
        CreateTile(1);
        CreateTile(1);
    
    }

    private void CreateTile(){
        Tile tile = Instantiate(tilePrefab, grid.transform);
        tile.SetState(tileStates[0],1);
        tile.Spawn(grid.GetRandomEmptyCell());
        tiles.Add(tile);
    }
    private void CreateTile(int number){
        Tile tile = Instantiate(tilePrefab, grid.transform);
        int stateID = number-1;//(int)Math.Pow(2,stateID+1);
        if(stateID > 11){stateID = 11;}
        tile.SetState(tileStates[stateID],number);
        tile.Spawn(grid.GetRandomEmptyCell());
        tiles.Add(tile);
    }

    private void Update(){
        
        if(!waiting){
            if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.UpArrow)){
                MoveTiles(Vector2Int.up, 0,1,1,1);
            }
            else if(Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveTiles(Vector2Int.down, 0,1,grid.height-2,-1);
            }
            else if(Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveTiles(Vector2Int.left, 0,1,0,1);
            }
            else if(Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveTiles(Vector2Int.right, grid.width-2,-1,0,1);
            }
        }
        


    }

    private void MoveTiles(Vector2Int direction, int startX, int incrementX, int startY, int incrementY){
        bool stateChanged = false;
        for(int x = startX; x >=0 && x < grid.width; x+=incrementX){
            for(int y = startY; y >=0 && y < grid.height; y+=incrementY){
                TileCell cell = grid.GetCell(x,y);
                
                if(cell.occupied){
                    stateChanged |= MoveTile(cell.tile,direction);
                }
            }   
        }

        if(stateChanged){
            StartCoroutine(WaitForChanges());
        }
    }

    private bool MoveTile(Tile tile, Vector2Int direction){
        TileCell newCell = null;
        TileCell adjacent = grid.GetAdjacentCell(tile.cell, direction);
                            
        while(adjacent != null){

            if(adjacent.occupied){

                if(CanMerge(tile, adjacent.tile)){
                    MergeTiles(tile,adjacent.tile);
                }

                break;
            }
            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent,direction);

        }

        if(newCell!= null){
            tile.MoveTo(newCell);
            return true;
        }
        return false;  
    }

    private bool CanMerge(Tile A, Tile B){
        return A.number == B.number;
    }
    private void MergeTiles(Tile A, Tile B){ //Move Tile A into Tile B. Destroy Tile A and Promote Tile B
        
        tiles.Remove(A);
        A.MergeTo(B.cell);
        PromoteTile(B);
    }

    private void PromoteTile(Tile tile){
        if(tile.number > 11){
            tile.SetState(tileStates[11],tile.number+1);
        }
        tile.SetState(tileStates[tile.number+1],tile.number+1);
    }
    private IEnumerator WaitForChanges(){
        waiting = true;

        yield return new WaitForSeconds(0.1f);

        waiting = false;

        if(tiles.Count <= grid.size){
            CreateTile();
        }
        
        //TODO: create new Tiles and check board state
    }


}
