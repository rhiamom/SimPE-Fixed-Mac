/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
 *                                                                         *
 *   Copyright (C) 2025 by GramzeSweatshop                                 *
 *   rhiamom@mac.com                                                       *
 *                                                                         *
 *   This program is free software; you can redistribute it and/or modify  *
 *   it under the terms of the GNU General Public License as published by  *
 *   the Free Software Foundation; either version 2 of the License, or     *
 *   (at your option) any later version.                                   *
 *                                                                         *
 *   This program is distributed in the hope that it will be useful,       *
 *   but WITHOUT ANY WARRANTY; without even the implied warranty of        *
 *   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the         *
 *   GNU General Public License for more details.                          *
 *                                                                         *
 *   You should have received a copy of the GNU General Public License     *
 *   along with this program; if not, write to the                         *
 *   Free Software Foundation, Inc.,                                       *
 *   59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.             *
 ***************************************************************************/

 using System;
using System.IO;
using OpenTK.Graphics.OpenGL4;
using SkiaSharp;

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
        using var skBmp = SKBitmap.Decode(stream);
        if (skBmp == null) return null;
        return FromSKBitmap(skBmp);
    }

    public static GlTexture FromBitmap(System.Drawing.Bitmap bmp)
    {
        // Convert System.Drawing.Bitmap to SKBitmap via PNG round-trip
        using var ms = new MemoryStream();
        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        ms.Position = 0;
        using var skBmp = SKBitmap.Decode(ms);
        if (skBmp == null) return null;
        return FromSKBitmap(skBmp);
    }

    public static GlTexture FromSKBitmap(SKBitmap skBmp)
    {
        // Ensure BGRA8888 format for GL upload
        SKBitmap bgraBmp;
        if (skBmp.ColorType != SKColorType.Bgra8888)
        {
            bgraBmp = new SKBitmap(skBmp.Width, skBmp.Height, SKColorType.Bgra8888, SKAlphaType.Premul);
            using var canvas = new SKCanvas(bgraBmp);
            canvas.DrawBitmap(skBmp, 0, 0);
        }
        else
        {
            bgraBmp = skBmp;
        }

        int id = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, id);

        IntPtr pixels = bgraBmp.GetPixels();
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
            bgraBmp.Width, bgraBmp.Height, 0,
            OpenTK.Graphics.OpenGL4.PixelFormat.Bgra,
            PixelType.UnsignedByte, pixels);

        if (bgraBmp != skBmp)
            bgraBmp.Dispose();

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
