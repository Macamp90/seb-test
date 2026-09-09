/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using SafeExamBrowser.Logging.Contracts;
using SafeExamBrowser.Monitoring.Contracts.Mouse;
using SafeExamBrowser.Settings.Monitoring;
using SafeExamBrowser.WindowsApi.Contracts;

namespace SafeExamBrowser.Monitoring.Mouse
{
	public class MouseInterceptor : IMouseInterceptor
	{
		public MouseInterceptor(ILogger logger, INativeMethods nativeMethods, MouseSettings settings)
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
