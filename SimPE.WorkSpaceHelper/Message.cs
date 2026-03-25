/***************************************************************************
 *   Copyright (C) 2005 by Ambertation                                     *
 *   quaxi@ambertation.de                                                  *
 *                                                                         *
 *   Copyright (C) 2025 by GramzeSweatShop                                 *
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

// Ported from WinForms Form to a pure-logic helper.
// All MessageBox dialogs are replaced with console/trace output since Avalonia
// message dialogs are async and this code runs in synchronous contexts.

using System;

namespace SimPe
{
    // ── Message dialog enums ──────────────────────────────────────────────────
    // Defined here in the SimPe namespace; replaces the WinForms originals.

    public enum MessageBoxButtons
    {
        OK, OKCancel, YesNo, YesNoCancel, AbortRetryIgnore, RetryCancel
    }

    public enum MessageBoxIcon
    {
        None, Error, Warning, Information, Question, Hand, Stop, Asterisk, Exclamation
    }

    public enum MessageBoxDefaultButton
    {
        Button1, Button2, Button3
    }

    /// <summary>
    /// Cross-platform message helper — replaces WinForms MessageBox calls.
    /// In the Avalonia port all messages are written to Trace; UI dialogs
    /// will be wired up once the Avalonia async dialog infrastructure is in place.
    /// </summary>
    public class Message
    {
        public static DialogResult Show(string message)
        {
            return Show(message, null, MessageBoxButtons.OK);
        }

        public static DialogResult Show(string message, string caption)
        {
            return Show(message, caption, MessageBoxButtons.OK);
        }

        public static DialogResult Show(string message, string caption, MessageBoxButtons mbb)
        {
            bool wasWaiting = WaitingScreen.Running;
            if (wasWaiting) WaitingScreen.Stop();

            try
            {
                caption = SimPe.Localization.GetString(caption);
                System.Diagnostics.Trace.TraceInformation("[Message] {0}: {1}", caption, message);
                // For YesNo/YesNoCancel default to Yes so "Fix" operations proceed.
                return (mbb == MessageBoxButtons.YesNo || mbb == MessageBoxButtons.YesNoCancel)
                    ? DialogResult.Yes
                    : DialogResult.OK;
            }
            finally
            {
                if (wasWaiting) WaitingScreen.Wait();
            }
        }
    }
}
