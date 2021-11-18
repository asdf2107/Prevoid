using Prevoid.Model;
using Prevoid.Model.EventArgs;
using Prevoid.ViewModel;
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
            foreach (var coord in overlay.GetFields())
            {
                _Drawer.Draw(new LocatedSymbol
                {
                    ScreenX = coord.Item1 * 2,
                    ScreenY = coord.Item2,
                    Symbol = GetSymbol(overlay.Sprite, coord.Item1, coord.Item2),
                });
            }
        }

        public void HideOverlay(Overlay overlay)
        {
            RenderFields(overlay.GetFields());
        }

        private LocatedSymbol GetLocatedSymbolAt(int x, int y)
        {
            return new LocatedSymbol
            {
                ScreenX = x * 2,
                ScreenY = y,
                Symbol = GetSymbolAt(x, y),
            };
        }

        private Symbol GetSymbolAt(int x, int y)
        {
            var symbol = GetSymbolFromSpriteType(GetSpriteTypeAt(x, y));

            if (Map.Selection.Item1 == x && Map.Selection.Item2 == y)
            {
                symbol = new Symbol
                {
                    ForeColor = symbol.ForeColor,
                    BackColor = Constants.SelectionColor,
                    Text = symbol.Text,
                };
            }

            return symbol;
        }

        private SpriteType GetSpriteTypeAt(int x, int y)
        {
            if (Map.Fields[x, y] != null)
            {
                return Map.Fields[x, y].SpriteType;
            }
            else if (Map.Structures[x, y] != null)
            {
                return Map.Structures[x, y].SpriteType;
            }
            else
            {
                return SpriteType.Empty;
            }
        }

        private Symbol GetSymbol(Sprite sprite, int x, int y)
        {
            Symbol result = new Symbol
            {
                Text = sprite.Text,
            };

            return result.MergeWith(GetSymbolFromSpriteType(sprite.Type == SpriteType.Translucent ? GetSpriteTypeAt(x, y) : sprite.Type));
        }

        private Symbol GetSymbolFromSpriteType(SpriteType spriteType)
        {
            return spriteType switch
            {
                SpriteType.Mountain => new Symbol
                {
                    ForeColor = Constants.MountainColor,
                    BackColor = Constants.TerrainColor,
                    Text = @"/\",
                },
                _ => new Symbol
                {
                    BackColor = Constants.TerrainColor,
                    Text = "  ",
                },
            };
        }
    }
}
