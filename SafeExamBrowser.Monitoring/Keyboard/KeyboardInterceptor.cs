/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using SafeExamBrowser.Logging.Contracts;
using SafeExamBrowser.Monitoring.Contracts.Keyboard;
using SafeExamBrowser.Settings.Monitoring;
using SafeExamBrowser.WindowsApi.Contracts;
using SafeExamBrowser.WindowsApi.Contracts.Events;

namespace SafeExamBrowser.Monitoring.Keyboard
{
	public class KeyboardInterceptor : IKeyboardInterceptor
	{
		public KeyboardInterceptor(ILogger logger, INativeMethods nativeMethods, KeyboardSettings settings)
		{
		}

		public void Start()
		{
		}

		public void Stop()
		{
		}
	}
}
