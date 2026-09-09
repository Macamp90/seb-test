/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Concurrent;
using System.Windows;
using SafeExamBrowser.Logging.Contracts;
using SafeExamBrowser.UserInterface.Contracts;

namespace SafeExamBrowser.UserInterface.Shared
{
	public class WindowGuard : IWindowGuard
	{
		public WindowGuard(ILogger logger)
		{
		}

		public void Activate()
		{
		}

		public void Deactivate()
		{
		}

		public void Register(object @object)
		{
		}
	}
}
