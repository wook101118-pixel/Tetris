using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class GameRule
    {
        // window form에서는 y축이 수학과 반대 방향이다.
        internal const int B_WIDTH          = 30;   // 상수를 정의할 때는 식별자를 모두 대문자로!
        internal const int B_HEIGHT         = 30;
        internal const int B_WIDTH_COUNT    = 12;
        internal const int B_HEIGHT_COUNT   = 20;
        internal const int START_X          = 4;
        internal const int START_Y          = 0;
    }
}
