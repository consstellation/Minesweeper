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
    }
}
