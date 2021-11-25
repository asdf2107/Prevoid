using Prevoid.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Prevoid.View.Forms
{
    public class UnitInfoForm : Form
    {
        public Unit Unit { get; private set; }

        private readonly List<List<Symbol>> MainText = new()
        {
            new() { Symbol.FromText("[unit name]") },
            new()
            {
                Symbol.FromText("HP:      "),
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
                Symbol.FromText("Attack:  "),
                Symbol.FromText("[v]", Constants.HighlightTextColor),
            },
            new()
            {
                Symbol.FromText("Range:   "),
                Symbol.FromText("[v]", Constants.HighlightTextColor),
            },
        };

        public UnitInfoForm(int x, int y, int width, int height) : base(x, y, width, height, Constants.ThinBoxCharSet)
        {
            MainText.Insert(1, new() { Symbol.FromText(new string(BoxCharSet.Hor, InnerWidth)) });
        }

        public void SetUnit(Unit unit)
        {
            Unit = unit;
        }

        public override void SetInnerText()
        {
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
            InnerText[5][1] = GetNumericDotValue(Unit.Weapon?.Damage ?? 0);
            InnerText[6][1] = GetNumericSingleValue(Unit.Weapon?.AttackRange ?? 0);
        }

        private static Symbol GetNumericDotValue(float current, float max = -1)
        {
            if (max >= 0)
            {
                return Symbol.FromText($"{current.ToString("0.0")}/{max.ToString("0.0")}", Constants.HighlightTextColor);
            }
            else
            {
                return Symbol.FromText($"    {current.ToString("0.0")}", Constants.HighlightTextColor);
            }
        }

        private static Symbol GetNumericSingleValue(float current)
        {
            return Symbol.FromText($"      {current.ToString("0")}", Constants.HighlightTextColor);
        }

    }
}
