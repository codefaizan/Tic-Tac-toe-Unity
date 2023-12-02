using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameController : MonoBehaviour
{
    public Text[] btnText;
    public Button[] btn;
    public Text turnText;
    public Text winText;
    int turn=1;     // 1- for player, 2- for AI

    //validation
    int[] boardData = new int[9];
    int[][] winningCombinations = new int[][]
    {
        new int[] {0,1,2},
        new int[] {3,4,5},
        new int[] {6,7,8},
        new int[] {0,3,6},
        new int[] {1,4,7},
        new int[] {2,5,8},
        new int[] {0,4,8},
        new int[] {2,4,6}
    };

    private void Start()
    {
        turnText.text = "Your Turn";
    }

    void PerformAction(string symbol, int btnID)
    {
        btnText[btnID].text = symbol;
        btn[btnID].interactable = false;
        boardData[btnID] = (turn == 1) ? 1 : 2;
        turnText.text = (turn == 1) ? "AI's Turn" : "Your Turn";
        turn = (turn == 1) ? 2 : 1;
        CheckWin();
    }

    public void ButtonCLicked(int btnID)
    {
        if (turn == 1)
        {
            PerformAction("X", btnID);
            //CallAI();
            Invoke("CallAI", 1f);
        }

    }

    void CallAI()
    {
        if (turn == 2)
        {
            
            for (int i = 0; i < winningCombinations.Length; i++)
            {
                int[] com = winningCombinations[i];
                int count = 0;
                
                foreach (int j in com)
                {
                    if (boardData[j] == 1)
                    {
                        count++;
                    }
                }

                if (count == 2)
                {
                    foreach (int j in com)
                    {
                        if (boardData[j] == 0)
                        {
                            PerformAction("O", j);
                            return;
                        }
                    }
                }
                else { count = 0; }
            }

            if(boardData[4] == 0)
            {
                PerformAction("O", 4);
            }
            else
            {
                List<int> zeroIndices = new List<int>();
                for (int position = 0; position < boardData.Length; position++)
                {
                    if (boardData[position] == 0)
                    {
                        zeroIndices.Add(position);
                    }
                    //if (boardData[position] == 0)
                    //{
                    //    PerformAction("O", position);
                    //    return;
                    //}
                }
                if (zeroIndices.Count > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, zeroIndices.Count);
                    int randomNumber = zeroIndices[randomIndex];
                    PerformAction("O", randomNumber);
                }
            }
        }
        
    }

    void CheckWin()
    {
        for (int i = 0; i < winningCombinations.Length; i++)
        {
            int[] combination = winningCombinations[i];
            int firstPosition = combination[0];
            int symbol = boardData[firstPosition];

            if (symbol != 0 &&
                boardData[combination[1]] == symbol &&
                boardData[combination[2]] == symbol)
            {
                winText.text = (symbol == 1) ? "You Won" : "AI Won";
                turn = 0;
                return;
            }
        }

        if(Array.IndexOf(boardData, 0) == -1)
        {
            winText.text = "Draw";
        }
    }

    public void test()
    {
        SceneManager.LoadScene(0);
    }
}