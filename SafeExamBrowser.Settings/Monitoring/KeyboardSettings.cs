/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;

namespace SafeExamBrowser.Settings.Monitoring
{
	/// <summary>
	/// Defines all settings for monitoring keyboard input.
	/// </summary>
	[Serializable]
	public class KeyboardSettings
	{
		public bool AllowAltEsc { get; set; } = true;
		public bool AllowAltF4 { get; set; } = true;
		public bool AllowAltTab { get; set; } = true;
		public bool AllowCtrlC { get; set; } = true;
		public bool AllowCtrlEsc { get; set; } = true;
		public bool AllowCtrlV { get; set; } = true;
		public bool AllowCtrlX { get; set; } = true;
		public bool AllowEsc { get; set; } = true;
		public bool AllowF1 { get; set; } = true;
		public bool AllowF2 { get; set; } = true;
		public bool AllowF3 { get; set; } = true;
		public bool AllowF4 { get; set; } = true;
		public bool AllowF5 { get; set; } = true;
		public bool AllowF6 { get; set; } = true;
		public bool AllowF7 { get; set; } = true;
		public bool AllowF8 { get; set; } = true;
		public bool AllowF9 { get; set; } = true;
		public bool AllowF10 { get; set; } = true;
		public bool AllowF11 { get; set; } = true;
		public bool AllowF12 { get; set; } = true;
		public bool AllowPrintScreen { get; set; } = true;
		public bool AllowSystemKey { get; set; } = true;
	}
}
