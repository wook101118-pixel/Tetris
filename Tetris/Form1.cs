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
            var depen       = new Pen(Color.Red, 4);                                                                        // Pen
            var nowPos      = game.NowPosition;                                                                             // Point
            var rectangle   = new Rectangle(nowPos.X * b_width + 2, nowPos.Y * b_height + 2, b_width - 4, b_height - 4);    // Rectangle
            graphics.DrawRectangle(depen, rectangle);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:    MoveRight();    return;
                case Keys.Left:     MoveLeft();     return;
                case Keys.Space:    MoveDown();     return;    // 한번에 한번씩 내려감
                case Keys.Up:       MoveTurn();     return;    // 한번에 다 내려감
            }
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
        }

        private void MoveTurn()
        {
           
        }

        private Region MakeRegion(int changeX, int changeY)
        {
           var nowPos = game.NowPosition;  // Point
           // 현재 사각형
           var currentRect = new Rectangle(nowPos.X * b_width, nowPos.Y * b_height, b_width, b_height);                             // Rectangle
           // 이동 전 사각형
           var previousRect = new Rectangle((nowPos.X + changeX) * b_width, (nowPos.Y + changeY) * b_height, b_width, b_height);    // Rectangle
           
           var region_00 = new Region(currentRect);    // Region
           var region_01 = new Region(previousRect);   // Region
            
           // 두 사각형의 합집합
           region_00.Union(region_01);
           return region_00;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveDown();
        }
    }
}
