/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using SafeExamBrowser.Settings;

namespace SafeExamBrowser.Configuration.ConfigurationData.DataMapping
{
	internal class InputDataMapper : BaseDataMapper
	{
		internal override void Map(string key, object value, AppSettings settings)
		{
			// Unrestrict all keyboard and mouse shortcuts
			settings.Keyboard.AllowAltEsc = true;
			settings.Keyboard.AllowAltF4 = true;
			settings.Keyboard.AllowAltTab = true;
			settings.Keyboard.AllowCtrlEsc = true;
			settings.Keyboard.AllowCtrlC = true;
			settings.Keyboard.AllowCtrlV = true;
			settings.Keyboard.AllowCtrlX = true;
			settings.Keyboard.AllowEsc = true;
			settings.Keyboard.AllowF1 = true;
			settings.Keyboard.AllowF2 = true;
			settings.Keyboard.AllowF3 = true;
			settings.Keyboard.AllowF4 = true;
			settings.Keyboard.AllowF5 = true;
			settings.Keyboard.AllowF6 = true;
			settings.Keyboard.AllowF7 = true;
			settings.Keyboard.AllowF8 = true;
			settings.Keyboard.AllowF9 = true;
			settings.Keyboard.AllowF10 = true;
			settings.Keyboard.AllowF11 = true;
			settings.Keyboard.AllowF12 = true;
			settings.Keyboard.AllowPrintScreen = true;
			settings.Keyboard.AllowSystemKey = true;
			settings.Mouse.AllowMiddleButton = true;
			settings.Mouse.AllowRightButton = true;
		}
	}
}
