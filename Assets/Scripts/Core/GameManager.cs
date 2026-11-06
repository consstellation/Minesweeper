using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    bool IsGameOver;
    Board board;
    MineGenerator mineGenerator;

    public GameManager(int row, int column)
    {
       board = new Board(row, column);
       mineGenerator = new MineGenerator(board);
    }

    public void StartGame()
    {
        mineGenerator.GenerateMines(10);
        board.CalculateAllAdjacentMines();
    }


    /// <summary>
    /// проверяем условие победы
    /// </summary>
    /// <returns></returns>
    public bool CheckWinCondition()
    {
        int safeCells = board.CountSafeCells();
        int openedCells = board.CountOpenedSafeCells();
        return safeCells == openedCells;

    }


    /// <summary>
    /// открытие клетки игроком
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    public void OpenCell(int row, int column)
    {
       CellData cellData = board.GetCell(row, column);
 

        if (IsGameOver)
            return;
      
        if (cellData.IsMine)
        {
            IsGameOver = true;
            return;
        }

        cellData.Open();
        
        if(cellData.CountAroundMine == 0)
        {
            OpenEmptyArea(row, column);
        }

        if(CheckWinCondition())
            IsGameOver = true;
    }


    /// <summary>
    /// открываем пустые поля
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    public void OpenEmptyArea(int row, int column)
    {
        List<(CellData Cell, int Row, int Column)> neighbors = board.GetNeighbors(row, column);

        foreach (var neighbor in neighbors)
        {
            if (!neighbor.Cell.IsOpen && !neighbor.Cell.IsMine && !neighbor.Cell.IsFlagged)
            {
                neighbor.Cell.Open();

                if (neighbor.Cell.CountAroundMine == 0)
                    OpenEmptyArea(neighbor.Row, neighbor.Column);

            }
        }
    }
}
