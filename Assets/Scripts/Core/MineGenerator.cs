
using Unity.VisualScripting;

public class MineGenerator
{
    Board board;

    public MineGenerator(Board existingBoard)
    {
        board = existingBoard;
    }

    /// <summary>
    /// создаём мины на поле
    /// </summary>
    /// <param name="mineCount"></param>
    public void GenerateMines(int mineCount)
    {
        System.Random rnd = new System.Random();
        int placedMines = 0;
        int limit = board.Height * board.Width;

        if (mineCount > limit)
             mineCount = limit;

        while (placedMines < mineCount)
            {
                int row = rnd.Next(0, board.Height);
                int column = rnd.Next(0, board.Width);

                CellData cellData = board.GetCell(row, column);


                if (!cellData.IsMine)
                {
                    cellData.PlaceMine();
                    placedMines++;
                }

            }
        
    }

    public bool CheckWinCondition()
    {
        bool isWin;
        int safeCells = board.CountSafeCells();
        int openedCells = board.CountOpenedSafeCells();

        if (safeCells == openedCells)
            return isWin = true;
    }

}
