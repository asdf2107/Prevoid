using Prevoid.View.Forms;
using System;

namespace Prevoid.View
{
    public static class Constants
    {
        public const ConsoleColor TerrainColor = ConsoleColor.Black;
        public const ConsoleColor ForestColor = ConsoleColor.DarkGreen;
        public const ConsoleColor MountainColor = ConsoleColor.Gray;
        public const ConsoleColor WavesColor = ConsoleColor.Cyan;
        public const ConsoleColor WaterColor = ConsoleColor.DarkBlue;

        public const ConsoleColor BackgroundColor = ConsoleColor.Black;
        public const ConsoleColor BrightTextColor = ConsoleColor.White;
        public const ConsoleColor HighlightTextColor = ConsoleColor.Yellow;
        public const ConsoleColor DarkTextColor = ConsoleColor.Black;
        public const ConsoleColor SelectionColor = ConsoleColor.Cyan;

        public const ConsoleColor SelectOverlayColor = ConsoleColor.White;
        public const ConsoleColor EnemyMoveAttackOverlayColor = ConsoleColor.DarkGray;
        public const ConsoleColor MoveOverlayColor = ConsoleColor.Green;
        public const ConsoleColor AttackOverlayColor = ConsoleColor.DarkMagenta;

        public static readonly BoxCharSet ThinBoxCharSet = new BoxCharSet('─', '│', '┌', '┐', '└', '┘');
    }
}
