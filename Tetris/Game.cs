using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    // 게임의 상태를 관리하는 클래스
    internal class Game
    {
        //private readonly static Game _instance = new Game();
        //public static Game Instance = _instance;
        private Diagram now;
        internal Point NowPosition
        {
            get
            {
                if (now == null)
                {
                    return new Point(0,  0);
                }
                else
                {
                    return new Point(now.X, now.Y);
                }
            }
        }

        internal static Game Singleton {get; private set;}

        // 정적 생성자: 클래스가 처음 사용될 때 한 번만 호출됨
        static Game()
        {
            Singleton = new Game();
        }

        // 일반 생성자: 외부에서 호출할 수 없음
        private Game()
        {
            now = new Diagram();   
        }

        internal bool MoveLeft()
        {
            if (now.X > 0)
            {
                now.MoveLeft();
                return true;
            }
            return false;
        }

        internal bool MoveRight()
        {
            if (now.X + 1 < GameRule.B_WIDTH_COUNT)
            {
                now.MoveRight();
                return true;
            }
            return false;
        }

        internal bool MoveDown()
        {
            if (now.Y + 1 < GameRule.B_HEIGHT_COUNT)
            {
                now.MoveDown();
                return true;
            }
            return false;
        }
    }
}