using System.Collections.Generic;
using System.Drawing;

namespace Demusicker.UI;

public static class ButtonColorPalette
{
    private static readonly List<Color> Colors =
    [
        Color.LightCoral,
        Color.LightBlue,
        Color.LightGreen,
        Color.Khaki,
        Color.Plum,
        Color.Turquoise,
        Color.Moccasin,
        Color.MediumAquamarine,
        Color.Thistle,
        Color.SkyBlue,
        Color.PaleGreen,
        Color.Wheat
    ];

    public static Color Get(int? version = null)
    {
        return Colors[(version ?? 0 + 1) % Colors.Count];
    }
}
