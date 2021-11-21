using Prevoid.Model;
using System.Collections.Generic;

namespace Prevoid.View.Forms
{
    public class HelpForm : Form
    {
        private readonly List<List<Symbol>> MoveHelpText = new List<List<Symbol>>
        {
            new List<Symbol> { Symbol.FromText("Move  ") },
        };
        private readonly List<List<Symbol>> AttackHelpText = new List<List<Symbol>>()
        {
            new List<Symbol> { Symbol.FromText("Attack") },
        };

        public HelpForm(int x, int y, int width, int height) : base(x, y, width, height, Constants.ThinBoxCharSet) { }

        public override void SetInnerText()
        {
            InnerText = GM.GameState switch
            {
                GameState.Movement => MoveHelpText,
                GameState.Attack => AttackHelpText,
                _ => EmptyText,
            };
        }
    }
}
