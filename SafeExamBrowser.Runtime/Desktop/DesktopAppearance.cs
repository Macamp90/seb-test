/*
 * Custom desktop appearance for the test build.
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. See https://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace SafeExamBrowser.Runtime.Desktop
{
	internal sealed class DesktopAppearance
	{
		private const uint GetWallpaper = 0x0073;
		private const uint SetWallpaper = 0x0014;
		private const int BackgroundColor = 1;
		private const uint ToggleDesktopIcons = 0x7402;
		private const string ExplorerAdvanced = @"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced";
		private readonly object sync = new object();
		private string wallpaper;
		private uint color;
		private bool restoreWallpaper;
		private bool restoreColor;
		private bool restoreIcons;

		internal string Apply()
		{
			lock (sync)
			{
				var errors = new List<string>();

				try
				{
					var buffer = new StringBuilder(32768);
					if (!ReadSystemParameters(GetWallpaper, (uint) buffer.Capacity, buffer, 0))
					{
						throw new Win32Exception(Marshal.GetLastWin32Error());
					}

					wallpaper = buffer.ToString();
					color = GetSysColor(BackgroundColor);
					// Do not persist changes to the user's theme or wallpaper registry settings.
					if (!SetSysColors(1, new[] { BackgroundColor }, new uint[] { 0 }))
					{
						throw new Win32Exception(Marshal.GetLastWin32Error());
					}
					restoreColor = true;
					if (!WriteSystemParameters(SetWallpaper, 0, string.Empty, 0))
					{
						throw new Win32Exception(Marshal.GetLastWin32Error());
					}
					restoreWallpaper = true;
				}
				catch (Exception e)
				{
					errors.Add("Sfondo nero: " + e.Message);
				}

				try
				{
					if (!IconsHidden())
					{
						// Use Explorer's actual Show desktop icons command, not deletion of icons.
						restoreIcons = true;
						SetIconsHidden(true);
					}
				}
				catch (Exception e)
				{
					errors.Add("Icone del desktop: " + e.Message);
				}

				return string.Join("\n", errors);
			}
		}

		internal string Restore()
		{
			lock (sync)
			{
				var errors = new List<string>();

				if (restoreColor)
				{
					if (SetSysColors(1, new[] { BackgroundColor }, new[] { color }))
					{
						restoreColor = false;
					}
					else
					{
						errors.Add("Impossibile ripristinare il colore del desktop.");
					}
				}

				if (restoreWallpaper)
				{
					if (WriteSystemParameters(SetWallpaper, 0, wallpaper, 0))
					{
						restoreWallpaper = false;
					}
					else
					{
						errors.Add("Impossibile ripristinare lo sfondo del desktop.");
					}
				}

				if (restoreIcons)
				{
					try
					{
						SetIconsHidden(false);
						restoreIcons = false;
					}
					catch (Exception e)
					{
						errors.Add("Ripristino delle icone: " + e.Message);
					}
				}

				return string.Join("\n", errors);
			}
		}

		private static bool IconsHidden()
		{
			using (var key = Registry.CurrentUser.OpenSubKey(ExplorerAdvanced))
			{
				return Convert.ToInt32(key?.GetValue("HideIcons", 0) ?? 0) != 0;
			}
		}

		private static void SetIconsHidden(bool hidden)
		{
			if (IconsHidden() == hidden)
			{
				return;
			}

			var view = IntPtr.Zero;
			EnumWindows((window, parameter) =>
			{
				var candidate = FindWindowEx(window, IntPtr.Zero, "SHELLDLL_DefView", null);
				if (candidate != IntPtr.Zero)
				{
					view = candidate;
					return false;
				}
				return true;
			}, IntPtr.Zero);

			if (view == IntPtr.Zero)
			{
				throw new InvalidOperationException("Desktop di Esplora risorse non disponibile.");
			}

			UIntPtr result;
			if (SendMessageTimeout(view, 0x0111, new UIntPtr(ToggleDesktopIcons), IntPtr.Zero, 0x0002, 2000, out result) == IntPtr.Zero)
			{
				throw new InvalidOperationException("Esplora risorse non ha confermato il comando delle icone.");
			}

			for (var attempt = 0; attempt < 10; attempt++)
			{
				if (IconsHidden() == hidden)
				{
					return;
				}
				Thread.Sleep(50);
			}

			throw new InvalidOperationException("La visibilità delle icone non risulta aggiornata; verificare i criteri di Windows.");
		}

		private delegate bool EnumWindowsCallback(IntPtr window, IntPtr parameter);

		[DllImport("user32.dll", EntryPoint = "SystemParametersInfoW", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool ReadSystemParameters(uint action, uint parameter, StringBuilder value, uint flags);

		[DllImport("user32.dll", EntryPoint = "SystemParametersInfoW", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool WriteSystemParameters(uint action, uint parameter, string value, uint flags);

		[DllImport("user32.dll")]
		private static extern uint GetSysColor(int index);

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetSysColors(int count, int[] elements, uint[] colors);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool EnumWindows(EnumWindowsCallback callback, IntPtr parameter);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern IntPtr FindWindowEx(IntPtr parent, IntPtr after, string className, string title);

		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern IntPtr SendMessageTimeout(IntPtr window, uint message, UIntPtr wParam, IntPtr lParam, uint flags, uint timeout, out UIntPtr result);
	}
}
