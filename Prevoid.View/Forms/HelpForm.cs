using Prevoid.Model;
using System.Collections.Generic;

namespace Prevoid.View.Forms
{
    public class HelpForm : Form
    {
        private readonly List<List<Symbol>> MoveHelpText = new()
        {
            new() { Symbol.FromText("[player]"), Symbol.FromText("'s turn [Movement]") },
            new() 
            { 
                Symbol.FromText("Move your units. Use "),
                Symbol.FromText("WASD", Constants.HighlightTextColor),
                Symbol.FromText(" or "),
                Symbol.FromText("Arrow keys", Constants.HighlightTextColor),
                Symbol.FromText(" to move the"),
            },
            new()
            {
                Symbol.FromText("cursor. To select a unit, press "),
                Symbol.FromText("Enter", Constants.HighlightTextColor),
                Symbol.FromText(" or "),
                Symbol.FromText("Space", Constants.HighlightTextColor),
                Symbol.FromText("."),
            },
            new() { },
            new()
            {
                Symbol.FromText("Press "),
                Symbol.FromText("Tab", Constants.HighlightTextColor),
                Symbol.FromText(" to finish your turn."),
            },
        };

        private readonly List<List<Symbol>> AttackHelpText = new()
        {
            new() { Symbol.FromText("[player]"), Symbol.FromText("'s turn. [Attack]") },
            new()
            {
                Symbol.FromText("Attack enemy units. Use "),
                Symbol.FromText("WASD", Constants.HighlightTextColor),
                Symbol.FromText(" or "),
                Symbol.FromText("Arrow keys", Constants.HighlightTextColor),
                Symbol.FromText(" to move"),
            },
            new()
            {
                Symbol.FromText("the cursor. To select a unit, press "),
                Symbol.FromText("Enter", Constants.HighlightTextColor),
                Symbol.FromText(" or "),
                Symbol.FromText("Space", Constants.HighlightTextColor),
                Symbol.FromText("."),
            },
            new() { },
            new()
            {
                Symbol.FromText("Press "),
                Symbol.FromText("Tab", Constants.HighlightTextColor),
                Symbol.FromText(" to finish your turn."),
            },
        };

        private readonly List<List<Symbol>> TurnEndedHelpText = new ()
        {
            new () { Symbol.FromText("[player]"), Symbol.FromText("'s turn.") },
            new () { Symbol.FromText("Press any key to continue...", Constants.HighlightTextColor) }
        };

        public HelpForm(int x, int y, int width, int height) : base(x, y, width, height, Constants.ThinBoxCharSet) { }

        public override void SetInnerText()
        {
            if (GM.HasTurnEnded)
            {
                InnerText = TurnEndedHelpText;
            }
            else
            {
                InnerText = GM.GameState switch
                {
                    GameState.Movement => MoveHelpText,
                    GameState.Attack => AttackHelpText,
                    _ => EmptyText,
                };
            }

            InnerText[0][0] = new Symbol
            {
                ForeColor = GM.CurrentPlayer.Color,
                BackColor = Constants.BackgroundColor,
                Text = $"Player {GM.CurrentPlayer.Id}",
            };
        }
    }
}
