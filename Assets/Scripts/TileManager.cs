using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TileManager : MonoBehaviour
{
    List<List<GameObject>> upperRow = new List<List<GameObject>>();
    List<List<GameObject>> lowerRow = new List<List<GameObject>>();
    List<GameObject> tmpUpperRow = new List<GameObject>();
    List<GameObject> tmpLowerRow = new List<GameObject>();


    private char letterUpper = ' ';
    private char letterLower = ' ';

    private int index = 0;

    Dictionary<int, char> TileNames = new Dictionary<int, char>()
    {
         {0, 'A'},
         {1, 'B'},
         {2, 'C'},
         {3, 'D'},
         {4, 'E'},
         {5, 'F'},
         {6, 'G'},
         {7, 'H'},
    };

    private int[] PlayersIDs = new int[4] { 0, 2, 4, 6 };
    void Start()
    {
        RotatePlayingArea.onRotationStartEvent.AddListener(ChangeIDs);


        TileSearch();
        Debug.Log(upperRow[PlayersIDs[3]][0].name);

        foreach (GameObject gameobj in GetAvailableTilesForPlayer(1))
        {

            Debug.Log(gameobj.name);


        }
    }


    public List<GameObject> GetAvailableTilesForPlayer(int PlayerID)
    {   //Инвертирование игроков(Так игроки располагаются по часовой стрелке)
        if (PlayerID == 2)
            PlayerID = 4;
        else
        if (PlayerID == 4)
            PlayerID = 2;


        if (PlayerID < 1 || PlayerID > 4) return new List<GameObject>();
        List<GameObject> AvailableTiles = new List<GameObject>();

        //MainTiles

        AvailableTiles.AddRange(upperRow[PlayersIDs[PlayerID - 1]]);
        AvailableTiles.AddRange(lowerRow[PlayersIDs[PlayerID - 1]]);

        //SideTiles

        //LeftSideTiles
        if (PlayersIDs[PlayerID - 1] - 1 == -1)
            index = 7;
        else
            index = PlayersIDs[PlayerID - 1] - 1;

        AvailableTiles.AddRange(lowerRow[index]);

        //RightSideTiles
        if (PlayersIDs[PlayerID - 1] + 1 == 8)
            index = 0;
        else
            index = PlayersIDs[PlayerID - 1] + 1;

        AvailableTiles.AddRange(lowerRow[index]);

        return AvailableTiles;
    }
    private void ChangeIDs()
    {
        for (int i = 0; i < PlayersIDs.Length; i++)
        {
            PlayersIDs[i] = (PlayersIDs[i] + 1) % (PlayersIDs.Length * 2);
            Debug.Log(PlayersIDs[i] + "  " + PlayersIDs.Length);
        }
    }

    private void TileSearch()
    {

        foreach (Transform child in this.transform)
        {
            if (child.name != "_Border" && child.name != "_Center")
            {
                Debug.Log((int)child.name[1] - 48);
                if ((int)child.name[1] - 48 <= 2)
                {
                    if (child.name[0] == letterUpper)
                    {
                        tmpUpperRow.Add(child.gameObject);
                    }
                    else if (letterUpper == ' ')
                    {
                        letterUpper = child.name[0];
                        tmpUpperRow.Add(child.gameObject);
                    }
                    else
                    {
                        letterUpper = child.name[0];
                        upperRow.Add(tmpUpperRow);
                        tmpUpperRow = new List<GameObject>();
                        tmpUpperRow.Add(child.gameObject);

                    }
                }
                else
                {
                    if (child.name[0] == letterLower)
                    {
                        tmpLowerRow.Add(child.gameObject);
                    }
                    else if (letterLower == ' ')
                    {
                        letterLower = child.name[0];
                        tmpLowerRow.Add(child.gameObject);
                    }
                    else
                    {
                        letterLower = child.name[0];
                        lowerRow.Add(tmpLowerRow);
                        tmpLowerRow = new List<GameObject>();
                        tmpLowerRow.Add(child.gameObject);

                    }
                }
            }
        }
        upperRow.Add(tmpUpperRow);
        lowerRow.Add(tmpLowerRow);
    }

}
// GetAvailableTilesForPlayer(PlayerID) ---> 1)MainTiles Добавить в список Тайлы, присущие данному ID с помощью Функции, которая по букве ищет все Тайлы с этой буквой(PlayersIDs[PlayerID-1]), 2)SideTiles Нужно по букве(PlayersIDs[PlayerID-1]) искать соседние с этой буквой тайлы НИЖНЕЙ ЛИНИИ, то есть те, у которых число будет >=31. Если значение -1 от буквы будет равно -1, то добавляем список с буквой 7.Если значение +1 от буквы будет равно 8, то добавляем список с буквой 0.