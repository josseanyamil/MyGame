﻿
using System.Drawing;
using System.Windows.Input;

namespace MyGame
{
    public class AttackState : HeroState
    {
        private double velx; //VelocityX = 0.3 pixels / 1ms
        private int vely; //VelocityY = 1 pixels / 1FPS

        public override HeroState HandleInput(Hero h)
        {
            if (_framecount == _frametotal)
            {
                if (Direction == HeroDirection.Left)
                    h.Position.X += GameWorld.TILES_WIDTH * 1 / 2;

                if (Keyboard.IsKeyDown(Key.Right) | Keyboard.IsKeyDown(Key.Left))
                    return new RunState(h, Direction);
                else
                    return new IdleState(h, Direction);
            }

            return null;
        }

        public AttackState(Hero hero, HeroDirection direction, double x_vel = 0.15, int y_vel = 1) : base("Jump_Attack", hero.Width + (int)(GameWorld.TILES_WIDTH * 0.85), hero.Height + GameWorld.TILES_HEIGHT * 1/4, direction)
        {
            velx = x_vel;
            vely = y_vel;

            if (direction == HeroDirection.Left)
                velx *= -1;
        }

        public override void Update(Hero hero, double elapsed)
        {
            base.Update(hero, elapsed);
            hero.Position.X += (int)(velx * elapsed);

            if (_framecount <= _frametotal / 2)
                hero.Position.Y -= vely;
            else
                hero.Position.Y += vely;
        }

        public override void Draw(Graphics g, int x, int y)
        {
            base.Draw(g, x, y + GameWorld.TILES_HEIGHT * 1/10);
        }
    }
}
