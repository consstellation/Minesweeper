using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class Board
{
    CellData[,] cells;

    public int Width { get; private set; }
    public int Height { get; private set; }


    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\\

    public Board(int _width, int _height)
    {
        Width = _width;
        Height = _height;
        InitializeCells();
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\\

    /// <summary>
    /// //----//создаём клетки игрового поля и заполняем ими массив//----//create the cells of the game board and populate an array with them//----//
    /// //
    /// </summary>
    public void InitializeCells()
    {
        cells = new CellData[Height, Width];

        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                cells[i, j] = new CellData();
            }
        }

    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\\
    /// <summary>
    /// //----//возвращаем список соседних клеток со смещением вокруг указанной клетки//----//returns a list of neighboring cells with an offset around the specified cell//----//
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    public List<CellData> GetNeighbors(int row, int column)
    {
        List<CellData> cellNeighbors = new List<CellData>();

        for (int rowOffset = -1; rowOffset < 2; rowOffset++)
        {
            for (int columntOffset = -1; columntOffset < 2; columntOffset++)
            {
                if (rowOffset == 0 && columntOffset == 0)
                    continue;

                int neighborRow = row + rowOffset;
                int neighborColumn = column + columntOffset;

                CellData neighbor = GetCell(neighborRow, neighborColumn);

                if (neighbor != null)
                    cellNeighbors.Add(neighbor);


            }
        }
        return cellNeighbors;
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\\

    /// <summary>
    /// //----//возвращаем клетку с координатами//----//we return the cell with the coordinates//----//
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    public CellData GetCell(int row, int column)
    {
        if ((row>=0 && row<Height) && (column >= 0 && column < Width))
        {
            return cells[row, column];
        }
        
        return null;
   
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\\


    /// <summary>
    /// //----//считаем количество мин вокруг клетки и возвращаем результат//----//we count the number of mines around the cell and return the result//----//
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    /// <returns></returns>
    public int CountMinesAround(int row, int column)
    {

        List<CellData> neighbors = GetNeighbors(row, column);

        int count = 0;

        foreach (var cell in neighbors)
        {
            if (cell.IsMine)
                count++;
        }

        return count;
    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\\


    /// <summary>
    /// закрепляем количество мин за каждой клеткой
    /// </summary>
    public void CalculateAllAdjacentMines()
    {

        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                int count = CountMinesAround(i, j);
                cells[i, j].SetAdjacentMines(count);
            }
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\\
    /// <summary>
    /// подсчитываем количество безопасных клеток
    /// </summary>
    public int CountSafeCells()
    {
        int safeCells = 0;
        CellData cell;

        for (int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                cell = cells[i, j];

                if (!cell.IsMine)
                {
                    safeCells++;
                }
            }
        }

        return safeCells;

    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------\\











}
