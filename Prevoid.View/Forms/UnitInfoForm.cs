using Prevoid.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.View.Forms
{
    public abstract class UnitInfoForm : Form
    {
        public Unit Unit { get; protected set; }

        private readonly List<List<Symbol>> MainText = new()
        {
            new() { Symbol.FromText("[unit name]") },
            new()
            {
                Symbol.FromText("HP:    "),
                Symbol.FromText("[v]", Constants.HighlightTextColor),
            },
            new()
            {
                Symbol.FromText("Move:    "),
                Symbol.FromText("[v]", Constants.HighlightTextColor),
            },
            new()
            {
                Symbol.FromText("FOV:     "),
                Symbol.FromText("[v]", Constants.HighlightTextColor),
            },
            new()
            {
                Symbol.FromText("Weapon:"),
                Symbol.FromText("[v]", Constants.HighlightTextColor),
            },
            new()
            {
                Symbol.FromText("  Attack:"),
                Symbol.FromText("[v]", Constants.HighlightTextColor),
            },
            new()
            {
                Symbol.FromText("  Rounds:"),
                Symbol.FromText("[v]", Constants.HighlightTextColor),
            },
            new()
            {
                Symbol.FromText("  Range: "),
                Symbol.FromText("[v]", Constants.HighlightTextColor),
            },
        };

        public UnitInfoForm(int x, int y, int width, int height) : base(x, y, width, height, Constants.ThinBoxCharSet)
        {
            MainText.Insert(1, new() { Symbol.FromText(new string(BoxCharSet.Hor, InnerWidth)) });
        }

        protected abstract void SetUnit();

        public override void SetInnerText()
        {
            SetUnit();

            if (Unit is null)
            {
                InnerText = GetClearText();
                return;
            }
            else
            {
                InnerText = MainText;
            }

            InnerText[0][0] = new Symbol
            {
                ForeColor = Unit.Player.Color,
                BackColor = Constants.BackgroundColor,
                Text = Unit.Name,
            };

            InnerText[2][1] = GetNumericDotValue(Unit.Hp, Unit.MaxHp);
            InnerText[3][1] = GetNumericSingleValue(Unit.MoveRange);
            InnerText[4][1] = GetNumericSingleValue(Unit.FieldOfView);
            if (Unit.Weapon is not null)
            {
                InnerText[5][1] = GetTextValue(Unit.Weapon.Name);
                InnerText[6][1] = GetNumericDotValue(Unit.Weapon.Damage);
                InnerText[7][1] = GetNumericSingleValue(Unit.Weapon.Rounds, Unit.Weapon.RoundsPerTurn);
                InnerText[8][1] = GetNumericSingleValue(Unit.Weapon.AttackRange);
            }
            else
            {
                var blank = Symbol.FromText("  -");
                InnerText[5][1] = GetTextValue("[none]");
                InnerText[6][1] = blank;
                InnerText[7][1] = blank;
                InnerText[8][1] = blank;
            }
        }

        private static Symbol GetNumericDotValue(float current, float max = -1)
        {
            if (max >= 0)
            {
                return Symbol.FromText($"{current.ToString("0.0") + '/' + max.ToString("0.0"),9}", Constants.HighlightTextColor);
            }
            else
            {
                return Symbol.FromText($"    {current.ToString("0.0")}", Constants.HighlightTextColor);
            }
        }

        private static Symbol GetNumericSingleValue(int current, int max = -1)
        {
            if (max >= 0)
            {
                return Symbol.FromText($"    {current}/{max}", Constants.HighlightTextColor);
            }
            else
            {
                return Symbol.FromText($"      {current}", Constants.HighlightTextColor);
            }
        }

        private static Symbol GetTextValue(string value)
        {
            return Symbol.FromText($"{value,9}", Constants.HighlightTextColor);
        }
    }
}
