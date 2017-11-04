﻿using System.Windows.Input;

namespace MyGame
{
    public class RunRightState : HeroState
    {
        public RunRightState(Hero hero) : base("Run", hero.Width, hero.Height, HeroDirection.Right)
        {
        }

        public override HeroState HandleInput(Hero hero)
        {
            if (Keyboard.IsKeyUp(Key.Right))
                return new IdleRightState(hero);

            //if iskeydown left then return new RunLeftState()
            if (Keyboard.IsKeyDown(Key.Left) & Keyboard.IsKeyUp(Key.Right))
                return new RunLeftState(hero);
            return null;
        }

        public override void Update(Hero hero)
        {
            base.Update(hero);
            hero.Position.X += 5;
        }
    }
}