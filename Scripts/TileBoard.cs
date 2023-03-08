using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    private TileGrid grid;
    private List<Tile> tiles;

    public Tile tilePrefab;
    public TileState[] tileStates;
    private void Awake(){
        grid = GetComponentInChildren<TileGrid>();
        tiles = new List<Tile>();

    }

    private void Start(){
        CreateTile(0);
        CreateTile(1);
        CreateTile(2);
        CreateTile(3);
        CreateTile(4);
        CreateTile(5);
        CreateTile(6);
        CreateTile(7);
    }

    private void CreateTile(){
        Tile tile = Instantiate(tilePrefab, grid.transform);
        tile.SetState(tileStates[Random.Range(0,11)],2);
        tile.Spawn(grid.getRandomEmptyCell());
        tiles.Add(tile);
    }
    private void CreateTile(int stateID){
        Tile tile = Instantiate(tilePrefab, grid.transform);
        int number = 2^^stateID;
        if(stateID > 11){stateID = 11;}
        tile.SetState(tileStates[stateID],number);
        tile.Spawn(grid.getRandomEmptyCell());
        tiles.Add(tile);
    }
}
