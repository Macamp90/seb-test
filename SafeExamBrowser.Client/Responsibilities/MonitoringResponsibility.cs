/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using SafeExamBrowser.Client.Contracts;
using SafeExamBrowser.I18n.Contracts;
using SafeExamBrowser.Logging.Contracts;
using SafeExamBrowser.Monitoring.Contracts.Applications;
using SafeExamBrowser.Monitoring.Contracts.Display;
using SafeExamBrowser.Monitoring.Contracts.System;
using SafeExamBrowser.Monitoring.Contracts.System.Events;
using SafeExamBrowser.UserInterface.Contracts.Shell;
using SafeExamBrowser.WindowsApi.Contracts;

namespace SafeExamBrowser.Client.Responsibilities
{
	internal class MonitoringResponsibility : ClientResponsibility
	{
		public MonitoringResponsibility(
			IActionCenter actionCenter,
			IApplicationMonitor applicationMonitor,
			ClientContext context,
			ICoordinator coordinator,
			IDisplayMonitor displayMonitor,
			IExplorerShell explorerShell,
			ILogger logger,
			ISystemSentinel sentinel,
			ITaskbar taskbar,
			IText text) : base(context, logger)
		{
		}

		public override void Assume(ClientTask task)
		{
			// All lockscreen triggers disabled
		}
	}
}
