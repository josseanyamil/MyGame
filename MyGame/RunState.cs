﻿using System.Drawing;
using System.Windows.Input;

namespace MyGame
{
    public class RunState : HeroState
    {
        //TEMP field
        private const double vel = 0.25; //Velocity = 0.2 pixel / 1 ms
        private const int _borderError = 55; //55 pixels
        public override HeroState HandleInput(Hero h)
        {
            switch (Direction)
            {
                case HeroDirection.Right:
                    if (Keyboard.IsKeyUp(Key.Right))
                    {
                        if (Keyboard.IsKeyDown(Key.Left))
                            return new RunState(h, HeroDirection.Left);

                        return new IdleState(h, HeroDirection.Right);
                    }
                    break;
                case HeroDirection.Left:
                    if (Keyboard.IsKeyUp(Key.Left))
                    {
                        if (Keyboard.IsKeyDown(Key.Right))
                            return new RunState(h, HeroDirection.Right);

                        return new IdleState(h, HeroDirection.Left);
                    }
                    break;
            }

            if (Keyboard.IsKeyDown(Key.A))
                return new JumpState(h, this.Direction);
            else if (Keyboard.IsKeyDown(Key.S))
                return new AttackState(h, this.Direction, vel);

            return null;
        }

        public override void Update(Hero hero, double elapsed)
        {
            base.Update(hero, elapsed);
            switch (Direction)
            {
                case HeroDirection.Right:
                    if (hero.Position.X <= GameWorld.CONTAINER_WIDTH + _borderError)
                        hero.Position.X += (int)(vel * elapsed);

                    //Put the hero in the other position if its going over the right border
                    //if (hero.Position.X > GameWorld.CONTAINER_WIDTH + _borderError)
                    //    hero.Position.X -= GameWorld.CONTAINER_WIDTH + _borderError * 2;
                    break;
                case HeroDirection.Left:
                    if (hero.Position.X >= 0 - _borderError)
                        hero.Position.X -= (int)(vel * elapsed);

                    //Put the hero in the other position if its going over the left border
                    //if (hero.Position.X < 0 - _borderError)
                    //    hero.Position.X += GameWorld.CONTAINER_WIDTH + _borderError * 2;
                    break;
            }
        }

        public override void Draw(Graphics g, int x, int y)
        {
            base.Draw(g, x, y + GameWorld.TILES_AMOUNT_Y / 4);
        }

        public RunState(Hero h, HeroDirection d) : base("Run", h.Width + GameWorld.TILES_WIDTH * 1/3, h.Height, d)
        {

        }
    }
}
