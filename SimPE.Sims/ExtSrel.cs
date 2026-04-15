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
using SimPe.Interfaces.Plugin;
using SimPe.Interfaces;
using SimPe.PackedFiles.Wrapper.Supporting;
using SimPe.Data;
using System.Drawing;
using SkiaSharp;

namespace SimPe.PackedFiles.Wrapper
{
	/// <summary>
	/// Summary description for ExtSDesc.
	/// </summary>
	public class ExtSrel : SRel, SimPe.Interfaces.Plugin.IMultiplePackedFileWrapper
	{
		
		public ExtSrel() : base()
		{
			
		}

		protected override IWrapperInfo CreateWrapperInfo()
		{			
			return new AbstractWrapperInfo(
				"Extended Sim Relation Wrapper",
				"Quaxi",
				"This File Contians the Relationship states for two Sims.",
				2,
				Helper.LoadImage(this.GetType().Assembly.GetManifestResourceStream("SimPe.PackedFiles.Wrapper.srel.png"))
				); 
			
		}

		protected override IPackedFileUI CreateDefaultUIHandler()
		{
			return new SimPe.PackedFiles.UserInterface.ExtSrel();
		}

		#region Descriptions
		protected SimPe.PackedFiles.Wrapper.ExtSDesc GetDescriptionByInstance(uint inst)
		{
			SimPe.PackedFiles.Wrapper.ExtSDesc ret = (SimPe.PackedFiles.Wrapper.ExtSDesc)FileTable.ProviderRegistry.SimDescriptionProvider.FindSim((ushort)inst);
			return ret;
#if UNREACHABLE
            if (ret==null) 
			{
				if (Package==null) return null;
				if (this.FileDescriptor==null) return null;
				SimPe.Interfaces.Files.IPackedFileDescriptor pfd = Package.FindFile(Data.MetaData.SIM_DESCRIPTION_FILE, 0, FileDescriptor.Group, inst);
				if (pfd==null) return null;

				ret = new SimPe.PackedFiles.Wrapper.ExtSDesc();
				ret.ProcessData(pfd, Package);
			}
			return ret;
#endif
        }

		public uint TargetSimInstance
		{
			get 
			{
				if (FileDescriptor==null) return 0;
				return (FileDescriptor.Instance & 0xffff);
			}
		}

		public uint SourceSimInstance
		{
			get 
			{
				if (FileDescriptor==null) return 0;
				return ((FileDescriptor.Instance>>16) & 0xffff);
			}
		}

		SimPe.PackedFiles.Wrapper.ExtSDesc src, dst;
		public SimPe.PackedFiles.Wrapper.ExtSDesc SourceSim
		{
			get 
			{
				if (src==null) src = GetDescriptionByInstance(SourceSimInstance);
				else if (src.FileDescriptor.Instance!=SourceSimInstance) src = GetDescriptionByInstance(SourceSimInstance);

				return src;
			}
		}

		public SimPe.PackedFiles.Wrapper.ExtSDesc TargetSim
		{
			get 
			{
				if (dst==null) dst = GetDescriptionByInstance(TargetSimInstance);
				else if (dst.FileDescriptor.Instance!=TargetSimInstance) dst = GetDescriptionByInstance(TargetSimInstance);

				return dst;
			}
		}

		public string SourceSimName
		{
			get 
			{
				if (SourceSim!=null) return SourceSim.SimName+" "+SourceSim.SimFamilyName;
				return SimPe.Localization.GetString("Unknown");
			}
		}

		public string TargetSimName
		{
			get 
			{
				if (TargetSim!=null) return TargetSim.SimName+" "+TargetSim.SimFamilyName;
				return SimPe.Localization.GetString("Unknown");
			}
		}

        private static SKBitmap GdiImageToSkBitmap(Image img)
        {
            if (img == null) return null;
            using var ms = new System.IO.MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            ms.Position = 0;
            return SKBitmap.Decode(ms);
        }

        private static Image SkBitmapToGdiImage(SKBitmap skBmp)
        {
            using var skImage = SKImage.FromBitmap(skBmp);
            using var encoded = skImage.Encode(SKEncodedImageFormat.Png, 100);
            var ms = new System.IO.MemoryStream();
            encoded.SaveTo(ms);
            ms.Position = 0;
            return Image.FromStream(ms);
        }

        public Image Image
        {
            get
            {
                var skBmp = new SKBitmap(356, 256, SKColorType.Bgra8888, SKAlphaType.Premul);
                using var canvas = new SKCanvas(skBmp);
                canvas.Clear(SKColors.Transparent);

                // ---- Source Sim image ----
                Image isrc = null;
                if (SourceSim != null)
                {
                    if (SourceSim.Image is Image srcImg)
                    {
                        if (srcImg.Width > 8)
                            isrc = srcImg;
                    }
                }

                if (isrc == null)
                {
                    isrc = SkBitmapToGdiImage(Helper.LoadImage(
                        this.GetType().Assembly.GetManifestResourceStream(
                            "SimPe.PackedFiles.Wrapper.noone.png"
                        )
                    ));
                }
                else
                {
                    isrc = Ambertation.Drawing.GraphicRoutines.KnockoutImage(
                        isrc,
                        new Point(0, 0),
                        Color.Magenta
                    );
                }

                // ---- Target Sim image ----
                Image idst = null;
                if (TargetSim != null)
                {
                    if (TargetSim.Image is Image dstImg)
                    {
                        if (dstImg.Width > 8)
                            idst = dstImg;
                    }
                }

                if (idst == null)
                {
                    idst = SkBitmapToGdiImage(Helper.LoadImage(
                        this.GetType().Assembly.GetManifestResourceStream(
                            "SimPe.PackedFiles.Wrapper.noone.png"
                        )
                    ));
                }
                else
                {
                    idst = Ambertation.Drawing.GraphicRoutines.KnockoutImage(
                        idst,
                        new Point(0, 0),
                        Color.Magenta
                    );
                }

                const int offsety = 32;

                // Draw source sim on the left
                using (var skSrc = GdiImageToSkBitmap(isrc))
                {
                    if (skSrc != null)
                    {
                        canvas.DrawBitmap(skSrc,
                            new SKRect(0, 0, skSrc.Width, skSrc.Height - offsety),
                            new SKRect(0, offsety, 256, 256));
                    }
                }

                // Draw target sim on the right
                using (var skDst = GdiImageToSkBitmap(idst))
                {
                    if (skDst != null)
                    {
                        canvas.DrawBitmap(skDst,
                            new SKRect(0, 0, skDst.Width, skDst.Height),
                            new SKRect(100, 0, 356, 256));
                    }
                }

                canvas.Flush();
                var result = SkBitmapToGdiImage(skBmp);
                skBmp.Dispose();
                return result;
            }
        }

        #endregion

        protected override string GetResourceName(SimPe.Data.TypeAlias ta)
		{
			if (!this.Processed) ProcessData(FileDescriptor, Package);
			return SourceSimName  + " "+SimPe.Localization.GetString("towards") + " "+TargetSimName;
		}
	}
}
