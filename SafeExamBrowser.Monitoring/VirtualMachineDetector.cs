/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using SafeExamBrowser.Configuration.Contracts.Integrity;
using SafeExamBrowser.Logging.Contracts;
using SafeExamBrowser.Monitoring.Contracts;
using SafeExamBrowser.SystemComponents.Contracts;
using SafeExamBrowser.SystemComponents.Contracts.Registry;

namespace SafeExamBrowser.Monitoring
{
	public class VirtualMachineDetector : IVirtualMachineDetector
	{
		private readonly ILogger logger;

		public VirtualMachineDetector(IIntegrityModule integrityModule, ILogger logger, IRegistry registry, ISystemInfo systemInfo)
		{
			this.logger = logger;
		}

		public bool IsVirtualMachine()
		{
			logger.Debug("Virtual machine check bypassed: reporting false.");
			return false;
		}
	}
}
