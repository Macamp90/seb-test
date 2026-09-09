/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using SafeExamBrowser.Logging.Contracts;
using SafeExamBrowser.Monitoring.Contracts;
using SafeExamBrowser.Settings.Security;
using SafeExamBrowser.WindowsApi.Contracts;

namespace SafeExamBrowser.Monitoring
{
	public class Clipboard : IClipboard
	{
		private readonly ILogger logger;

		public Clipboard(ILogger logger, INativeMethods nativeMethods, int timeout_ms = 50)
		{
			this.logger = logger;
		}

		public void Initialize(ClipboardPolicy policy)
		{
			logger.Info("Initialized clipboard without restrictions (system clipboard active).");
		}

		public void Terminate()
		{
			logger.Info("Finalized clipboard without clearing.");
		}
	}
}
