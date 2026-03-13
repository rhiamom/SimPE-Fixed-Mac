using System.Drawing;

namespace Ambertation.Graphics;

public struct GlMaterial
{
    public Color Diffuse;
    public Color Ambient;
    public Color Specular;
    public Color Emissive;
    public float SpecularSharpness;
}
