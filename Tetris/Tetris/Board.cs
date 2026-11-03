using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    // 보드판의 상태를 2차원 배열로 관리하는 클래스❤️
    internal class Board
    {
        internal static Board GameBoard { get; private set; }

        static Board()
        {
            GameBoard = new Board();
        }

        int[,] board = new int[GameRule.B_WIDTH_COUNT, GameRule.B_HEIGHT_COUNT];
        
        // 인덱서
        internal int this[int x, int y]
        {
            get
            {
                return board[x, y];
            }
            set
            {
                board[x, y] = value;
            }
        }

        internal bool MoveEnable(int blockNum, int turnNum, int x, int y)
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvalues[blockNum, turnNum, xx, yy] != 0)
                    {
                        if (board[x + xx, y + yy] != 0)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        internal void Store(int blockNum, int turnNum, int x, int y)
        {
            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if ((x + xx) >= 0 && (x + xx < GameRule.B_WIDTH_COUNT) && (y + yy) >= 0 && (y + yy < GameRule.B_HEIGHT_COUNT))
                    {
                        board[x + xx, y + yy] += BlockValue.bvalues[blockNum, turnNum, xx, yy];
                    }
                }
            }

            CheckLines(y + 3);
        }

        private void CheckLines(int v)
        {
            for (int yy = 0; yy < 4; yy++)
            {
                if (v - yy < GameRule.B_HEIGHT_COUNT)
                {
                    if (CheckLine(v - yy))
                    {
                        ClearLine(v - yy);
                        v++;
                    }
                }
            }
        }

        private void ClearLine(int y)
        {
            for (; y > 0; y--)
            {
                for (int xx = 0; xx < GameRule.B_WIDTH_COUNT; xx++)
                {
                    board[xx, y] = board[xx, y - 1];
                }
            }
        }

        private bool CheckLine(int y)
        {
            for (int xx = 0; xx < GameRule.B_WIDTH_COUNT; xx++)
            {
                if (board[xx, y] == 0)
                {
                    return false;
                }
            }

            return true;
        }

        internal void ClearBoard()
        {
            for (int xx = 0; xx < GameRule.B_WIDTH_COUNT; xx++)
            {
                for (int yy = 0; yy < GameRule.B_HEIGHT_COUNT; yy++)
                {
                    board[xx, yy] = 0;
                }
            }
        }
    }
}
