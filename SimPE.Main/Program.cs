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
 ***************************************************************************/

using Avalonia;
using System;
using System.IO;

namespace SimPe
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                LogCrash(e.ExceptionObject as Exception ?? new Exception(e.ExceptionObject?.ToString()), e.IsTerminating);

            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

            try
            {
                Packages.StreamFactory.UnlockAll();
                Packages.StreamFactory.CloseAll(true);
                Packages.StreamFactory.CleanupTeleport();
            }
            catch { }

            Environment.Exit(0);
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();

        static void LogCrash(Exception ex, bool isTerminating)
        {
            try
            {
                string logPath = Path.Combine(AppContext.BaseDirectory, "crash.log");
                string msg = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Terminating={isTerminating}\r\n{ex}\r\n\r\n";
                File.AppendAllText(logPath, msg);
            }
            catch { }
        }
    }
}
