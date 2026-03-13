using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace Ambertation.Graphics;

public class GlTexture : IDisposable
{
    private int _id;
    private bool _disposed;

    public int Id => _id;
    public bool Disposed => _disposed;

    private GlTexture(int id)
    {
        _id = id;
    }

    public static GlTexture FromStream(Stream stream)
    {
        stream.Seek(0, SeekOrigin.Begin);
        using var bmp = new Bitmap(stream);
        return FromBitmap(bmp);
    }

    public static GlTexture FromBitmap(Bitmap bmp)
    {
        int id = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, id);

        var data = bmp.LockBits(
            new Rectangle(0, 0, bmp.Width, bmp.Height),
            ImageLockMode.ReadOnly,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        try
        {
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                bmp.Width, bmp.Height, 0,
                OpenTK.Graphics.OpenGL4.PixelFormat.Bgra,
                PixelType.UnsignedByte, data.Scan0);
        }
        finally
        {
            bmp.UnlockBits(data);
        }

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        GL.BindTexture(TextureTarget.Texture2D, 0);

        return new GlTexture(id);
    }

    public void Dispose()
    {
        if (!_disposed && _id != 0)
        {
            GL.DeleteTexture(_id);
            _id = 0;
        }
        _disposed = true;
    }
}
