using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    // 그리는 책임
    public partial class Form1 : Form
    {
        Game game;
        int b_width_count;
        int b_height_count;
        int b_width;
        int b_height;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game            = Game.Singleton;
            b_width_count   = GameRule.B_WIDTH_COUNT;
            b_height_count  = GameRule.B_HEIGHT_COUNT;
            b_width         = GameRule.B_WIDTH;
            b_height        = GameRule.B_HEIGHT;

            SetClientSizeCore(b_width_count * b_width, b_height_count * b_height);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawLines(e.Graphics);
            DrawDiagram(e.Graphics);
            DoubleBuffered = true;  // 화면 깜빡임 문제를 홰결
            DrawBoard(e.Graphics);
        }

        private void DrawBoard(Graphics graphics)
        {
            for (int xx = 0; xx < b_width_count; xx++)
            {
                for (int yy = 0; yy < b_height_count; yy++)
                {
                    if (game[xx, yy] != 0)
                    {
                        var rectangle = new Rectangle(xx * b_width + 2, yy * b_height + 2, b_width - 4, b_height - 4);
                        graphics.DrawRectangle(Pens.HotPink, rectangle);
                        graphics.FillRectangle(Brushes.HotPink, rectangle);
                    }
                }
            }
        }

        private void DrawLines(Graphics graphics)
        {
            DrawHorizons(graphics);

            DrawVerticals(graphics);
        }
        private void DrawHorizons(Graphics graphics)
        {
            var startPos    = new Point();
            var  endPos     = new Point();

            for (int i = 0; i < b_width_count; i++)
            {
                startPos.X  = i * b_width;
                startPos.Y  = 0;
                endPos.X    = i * b_width;
                endPos.Y    = b_height_count * b_height;

                graphics.DrawLine(Pens.HotPink, startPos, endPos);
            }
        }

        private void DrawVerticals(Graphics graphics)
        {
            var startPos   = new Point();   // Point
            var endPos     = new Point();   // Point

            for (int i = 0; i < b_height_count; i++)
            {
                startPos.X  = 0;
                startPos.Y  = i * b_height;
                endPos.X    = b_width_count * b_width;
                endPos.Y    = i * b_height;

                graphics.DrawLine(Pens.HotPink, startPos, endPos);
            }
        }


        private void DrawDiagram(Graphics graphics)
        {
            var depen       = new Pen(Color.Red, 4);        // Pen
            var nowPos      = game.NowPosition;             // Point
            int blockNum    = game.BlockNum;
            int turnNum     = game.Turn;

            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvalues[blockNum, turnNum,xx,yy] != 0)
                    {
                        var rectangle   = new Rectangle((nowPos.X + xx) * b_width + 2, (nowPos.Y + yy) * b_height + 2
                            , b_width - 4, b_height - 4);   // Rectangle
                        graphics.DrawRectangle(depen, rectangle);

                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:case Keys.D:     MoveRight();    return;
                case Keys.Left: case Keys.A:     MoveLeft();     return;
                case Keys.Down: case Keys.S:     MoveDown();     return;    // 한번에 한번씩 내려감
                case Keys.Space:                 MoveTurn();     return;    //= 회전
                case Keys.Enter:                 MoveSuperDown(); return;    // 한번에 다 내려감
            }
        }

        private void MoveSuperDown()
        {
            while (game.MoveDown())
            {
                var region = MakeRegion(0, -1);     // Region
                Invalidate(region);
            }

            EndingCheck();                         
        }

        private void MoveRight()
        {
            if (game.MoveRight())
            {
                var region = MakeRegion(-1, 0);     // Region
                Invalidate(region);
            }
        }

        private void MoveLeft()
        {
            if (game.MoveLeft())
            {
                var region = MakeRegion(1, 0);     // Region
                Invalidate(region);
            }
        }

        private void MoveDown()
        {
            if (game.MoveDown())
            {
                var region = MakeRegion(0, -1);     // Region
                Invalidate(region);
            }
            else
            {
                EndingCheck();
            }
        }

        private void EndingCheck()
        {
            if (game.Next())
            {
                Invalidate();
            }
            else
            {
                timer1.Enabled = false;
                DialogResult result = MessageBox.Show("다시시작 하시겠습니까?", "다시시작", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    game.Reset();
                    timer1.Enabled = true;
                    Invalidate();
                }
                else
                {
                    Close();
                }
            }
        }

        private void MoveTurn()
        {
           if (game.MoveTurn())
            {
                Region region = MakeRegion();
                Invalidate(region);
            }
        }

        // 회전 전용
        private Region MakeRegion()
        {
            Point now = game.NowPosition;
            int blockNum = game.BlockNum;
            int turnNum = game.Turn;
            int oldturnNum = (turnNum + 3) % 4;

            Region region = new Region();

            for (int xx = 0; xx < 4; xx++)
            {
                for (int yy = 0; yy < 4; yy++)
                {
                    if (BlockValue.bvalues[blockNum, turnNum, xx, yy] != 0)
                    {
                        Rectangle rect1 = new Rectangle((now.X + xx) * b_width, (now.Y + yy) * b_height, b_width, b_height);
                        Region rg1 = new Region(rect1);
                        region.Union(rg1);
                    }

                    if (BlockValue.bvalues[blockNum, oldturnNum, xx ,yy] != 0)
                    {
                        Rectangle rect2 = new Rectangle((now.X + xx) * b_width, (now.Y + yy) * b_height, b_width, b_height);
                        Region rg2 = new Region(rect2);
                        region.Union(rg2);
                    }
                }
            }
            return region;
        }

        private Region MakeRegion(int changeX, int changeY)
        {
           var nowPos    = game.NowPosition;  // Point
           int blockNum  = game.BlockNum;
           int turnNum  = game.Turn;
           var region    = new Region();

           for (int xx = 0; xx < 4; xx++)
           {
                for (int yy = 0; yy< 4; yy++)
                {
                    if (BlockValue.bvalues[blockNum, turnNum, xx, yy] != 0)
                    {
                        // 현재(이동 후) 사각형
                        var currentRect = new Rectangle((nowPos.X + xx) * b_width, (nowPos.Y + yy) * b_height
                            , b_width, b_height);     // Rectangle
                                                      // 이동 전 사각형
                        var previousRect = new Rectangle((nowPos.X + xx + changeX) * b_width, (nowPos.Y + yy + changeY) * b_height
                            , b_width, b_height);    // Rectangle

                        var region_00 = new Region(currentRect);    // Region
                        var region_01 = new Region(previousRect);   // Region

                        region.Union(region_00);
                        region.Union(region_01);
                    }
                }
           }

            return region;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveDown();
        }
    }
}
