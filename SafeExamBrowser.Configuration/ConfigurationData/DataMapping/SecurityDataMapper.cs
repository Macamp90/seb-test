/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using SafeExamBrowser.Settings;
using SafeExamBrowser.Settings.Security;

namespace SafeExamBrowser.Configuration.ConfigurationData.DataMapping
{
	internal class SecurityDataMapper : BaseDataMapper
	{
		internal override void Map(string key, object value, AppSettings settings)
		{
			switch (key)
			{
				case Keys.Security.AdminPasswordHash:
					MapAdminPasswordHash(settings, value);
					break;
				case Keys.Security.AllowReconfiguration:
					MapAllowReconfiguration(settings, value);
					break;
				case Keys.Security.AllowStickyKeys:
					MapAllowStickyKeys(settings, value);
					break;
				case Keys.Security.AllowTermination:
					MapAllowTermination(settings, value);
					break;
				case Keys.Security.AllowVirtualMachine:
					MapVirtualMachinePolicy(settings, value);
					break;
				case Keys.Security.AllowWindowCapture:
					MapAllowWindowCapture(settings, value);
					break;
				case Keys.Security.ClipboardPolicy:
					MapClipboardPolicy(settings, value);
					break;
				case Keys.Security.DisableSessionChangeLockScreen:
					MapDisableSessionChangeLockScreen(settings, value);
					break;
				case Keys.Security.QuitPasswordHash:
					MapQuitPasswordHash(settings, value);
					break;
				case Keys.Security.ReconfigurationUrl:
					MapReconfigurationUrl(settings, value);
					break;
				case Keys.Security.VerifyCursorConfiguration:
					MapVerifyCursorConfiguration(settings, value);
					break;
				case Keys.Security.VerifySessionIntegrity:
					MapVerifySessionIntegrity(settings, value);
					break;
			}
		}

		internal override void MapGlobal(IDictionary<string, object> rawData, AppSettings settings)
		{
			settings.Security.VirtualMachinePolicy = VirtualMachinePolicy.Allow;
			settings.Security.AllowWindowCapture = true;
			settings.Security.ClipboardPolicy = ClipboardPolicy.Allow;
			settings.Security.AllowTermination = true;
			settings.Security.AllowStickyKeys = true;
			settings.Security.KioskMode = KioskMode.None;
			settings.Security.AllowApplicationLogAccess = false;
			settings.UserInterface.ActionCenter.ShowApplicationLog = false;
			settings.UserInterface.Taskbar.ShowApplicationLog = false;
		}

		private void MapAdminPasswordHash(AppSettings settings, object value)
		{
			if (value is string hash)
			{
				settings.Security.AdminPasswordHash = hash;
			}
		}

		private void MapAllowReconfiguration(AppSettings settings, object value)
		{
			if (value is bool allow)
			{
				settings.Security.AllowReconfiguration = allow;
			}
		}

		private void MapAllowStickyKeys(AppSettings settings, object value)
		{
			settings.Security.AllowStickyKeys = true;
		}

		private void MapAllowTermination(AppSettings settings, object value)
		{
			settings.Security.AllowTermination = true;
		}

		private void MapAllowWindowCapture(AppSettings settings, object value)
		{
			settings.Security.AllowWindowCapture = true;
		}

		private void MapQuitPasswordHash(AppSettings settings, object value)
		{
			if (value is string hash)
			{
				settings.Security.QuitPasswordHash = hash;
			}
		}

		private void MapClipboardPolicy(AppSettings settings, object value)
		{
			settings.Security.ClipboardPolicy = ClipboardPolicy.Allow;
		}

		private void MapDisableSessionChangeLockScreen(AppSettings settings, object value)
		{
			settings.Security.DisableSessionChangeLockScreen = true;
		}

		private void MapVirtualMachinePolicy(AppSettings settings, object value)
		{
			settings.Security.VirtualMachinePolicy = VirtualMachinePolicy.Allow;
		}

		private void MapReconfigurationUrl(AppSettings settings, object value)
		{
			if (value is string url)
			{
				settings.Security.ReconfigurationUrl = url;
			}
		}

		private void MapVerifyCursorConfiguration(AppSettings settings, object value)
		{
			settings.Security.VerifyCursorConfiguration = false;
		}

		private void MapVerifySessionIntegrity(AppSettings settings, object value)
		{
			settings.Security.VerifySessionIntegrity = false;
		}
	}
}
