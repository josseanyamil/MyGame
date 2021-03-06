﻿using System;
using System.Drawing;

namespace MyGame
{
    public class Hero : GameObject
    {
        public Image Image { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public HeroState State { get; set; }

        public Hero(int x, int y, int width, int height)
        {
            Position = new Vector();
            Position.X = x;
            Position.Y = y;

            Width = width;
            Height = height;

            //First state is the Idle to the Right
            State = new IdleState(this, HeroDirection.Right);
        }

        public override void Update(double elapsed)
        {
            State.Update(this, elapsed);
        }

        public override void Draw(Graphics g)
        {
            State.Draw(g, Position.X, Position.Y);
        }

        public void HandleInput()
        {
            HeroState returnedstate = State.HandleInput(this);
            if (returnedstate != null)
            {
                //delete State;
                GC.Collect();
                State = returnedstate;
                State.Enter(this);
            }
        }
    }
}
