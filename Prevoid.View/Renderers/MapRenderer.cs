using Prevoid.Model;
using Prevoid.Model.EventArgs;
using Prevoid.ViewModel;
using System;
using System.Collections.Generic;

namespace Prevoid.View.Renderers
{
    public class MapRenderer : Renderer
    {
        public Map Map { get; private set; }

        public MapRenderer(ScreenDrawer screenDrawer, Map map) : base(screenDrawer)
        {
            Map = map;
        }

        public void RenderMap()
        {
            var locatedSymbols = new List<LocatedSymbol>();

            for (int j = 0; j < Map.Height; j++)
            {
                var lineSymbols = new List<LocatedSymbol>();

                for (int i = 0; i < Map.Width; i++)
                {
                    lineSymbols.Add(GetLocatedSymbolAt(i, j));
                }

                locatedSymbols.AddRange(lineSymbols.MergeConsecutive());
            }

            _Drawer.Draw(locatedSymbols);
        }

        public void RenderField(int x, int y)
        {
            _Drawer.Draw(GetLocatedSymbolAt(x, y));
        }

        public void RenderFields(IEnumerable<(int, int)> coords)
        {
            foreach (var coord in coords)
            {
                RenderField(coord.Item1, coord.Item2);
            }
        }

        public void RenderOverlay(Overlay overlay)
        {
            List<LocatedSymbol> symbols = new List<LocatedSymbol>();

            foreach (var coord in overlay.GetFields())
            {
                symbols.Add(GetLocatedSymbolAt(coord.Item1, coord.Item2, GetOverlayColor(overlay.Type)));
            }

            _Drawer.Draw(symbols);
        }

        public void HideOverlay(Overlay overlay)
        {
            RenderFields(overlay.GetFields());
        }

        private ConsoleColor GetOverlayColor(OverlayType overlayType)
        {
            return overlayType switch
            {
                OverlayType.Select => Constants.SelectOverlayColor,
                OverlayType.Forbidden => Constants.EnemyMoveAttackOverlayColor,
                OverlayType.Move => Constants.MoveOverlayColor,
                OverlayType.Attack => Constants.AttackOverlayColor,
                _ => throw new NotImplementedException(),
            };
        }

        private LocatedSymbol GetLocatedSymbolAt(int x, int y, ConsoleColor? overlayColor = null)
        {
            return new LocatedSymbol
            {
                ScreenX = x * 2,
                ScreenY = y,
                Symbol = GetSymbolAt(x, y, overlayColor),
            };
        }

        private Symbol GetSymbolAt(int x, int y, ConsoleColor? overlayColor)
        {
            var (spriteType, harmable) = GetSpriteTypeAndIHarmableAt(x, y);
            var symbol = GetSymbolFromSpriteType(spriteType, harmable);

            if (Map.Selection.Item1 == x && Map.Selection.Item2 == y)
            {
                return symbol.GetTranslucent(Constants.SelectionColor);
            }
            if (overlayColor is not null)
            {
                return symbol.GetTranslucent(overlayColor.Value);
            }

            return symbol;
        }

        private (SpriteType, IHarmable) GetSpriteTypeAndIHarmableAt(int x, int y)
        {
            if (Map.Fields[x, y] != null)
            {
                return (Map.Fields[x, y].SpriteType, Map.Fields[x, y]);
            }
            else if (Map.Structures[x, y] != null)
            {
                return (Map.Structures[x, y].SpriteType, Map.Structures[x, y] as IHarmable);
            }
            else
            {
                return (GetSpriteTypeFromTerrain(x, y), null);
            }
        }

        private SpriteType GetSpriteTypeFromTerrain(int x, int y)
        {
            return Map.TerrainTypes[x, y] switch
            {
                TerrainType.Flat => SpriteType.Empty,
                TerrainType.SparceForest => SpriteType.SparceForest,
                TerrainType.DeepForest => SpriteType.DeepForest,
                TerrainType.Mountain => SpriteType.Mountain,
                TerrainType.Water => SpriteType.Water,
                _ => throw new NotImplementedException(),
            };
        }

        private Symbol GetSymbolFromSpriteType(SpriteType spriteType, IHarmable harmable)
        {
            return spriteType switch
            {
                SpriteType.Empty => new Symbol
                {
                    BackColor = Constants.TerrainColor,
                    Text = "  ",
                },
                SpriteType.Mountain => new Symbol
                {
                    ForeColor = Constants.MountainColor,
                    BackColor = Constants.TerrainColor,
                    Text = @"/\",
                },
                SpriteType.Water => new Symbol
                {
                    ForeColor = Constants.WavesColor,
                    BackColor = Constants.WaterColor,
                    Text = "~ ",
                },
                SpriteType.SparceForest => new Symbol
                {
                    ForeColor = Constants.ForestColor,
                    BackColor = Constants.TerrainColor,
                    Text = "▲ ",
                },
                SpriteType.DeepForest => new Symbol
                {
                    ForeColor = Constants.ForestColor,
                    BackColor = Constants.TerrainColor,
                    Text = "▲▲",
                },
                SpriteType.Tank => new Symbol
                {
                    ForeColor = harmable.Player.Color,
                    BackColor = Constants.TerrainColor,
                    Text = "T" + harmable.HpChar,
                },
                _ => throw new NotImplementedException(),
            };
        }
    }
}
