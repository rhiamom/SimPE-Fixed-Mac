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

#nullable enable
#pragma warning disable CS8603, CS8618, CS8622, CS8625, CS8601, CS8600, CS8602, CS8604
namespace SimPe
{
    /// <summary>
    /// Provides standard SimPE images.
    /// GDI+ rendering is not available cross-platform; placeholder images return null.
    /// Types use System.Drawing.Image so callers in WinForms-origin code compile directly.
    /// </summary>
    public static class GetImage
    {
        /// <summary>Generic "something went wrong" image (null placeholder).</summary>
        public static System.Drawing.Image? Fail => null;

        /// <summary>Generic "no sim" placeholder image (null placeholder).</summary>
        public static System.Drawing.Image? NoOne => null;

        /// <summary>Generic "network/lot" placeholder image (null placeholder).</summary>
        public static System.Drawing.Image? Network => null;

        /// <summary>Generic "demo" placeholder image.</summary>
        public static System.Drawing.Image? Demo
        {
            get
            {
                var bmp = new System.Drawing.Bitmap(64, 64);
                using (var g = System.Drawing.Graphics.FromImage(bmp))
                {
                    g.Clear(System.Drawing.Color.FromArgb(220, 220, 230));
                    using var font = new System.Drawing.Font("Arial", 8);
                    g.DrawString("No Preview", font, System.Drawing.Brushes.Gray, 2, 24);
                }
                return bmp;
            }
        }

        /// <summary>Returns a logo image for an expansion pack (null placeholder).</summary>
        public static System.Drawing.Image? GetExpansionLogo(int expansionId) => null;

        /// <summary>Returns an icon-sized image for an expansion pack (null placeholder).</summary>
        public static System.Drawing.Image? GetExpansionIcon(int expansionId) => null;
    }
}
