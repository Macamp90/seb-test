/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 *
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using SafeExamBrowser.Core.Contracts.OperationModel;
using SafeExamBrowser.Lockdown.Contracts;
using SafeExamBrowser.Logging.Contracts;

namespace SafeExamBrowser.Service.Operations
{
	internal class LockdownOperation : SessionOperation
	{
		private readonly ILogger logger;

		public LockdownOperation(
			IFeatureConfigurationBackup backup,
			IFeatureConfigurationFactory factory,
			IFeatureConfigurationMonitor monitor,
			ILogger logger,
			SessionContext sessionContext) : base(sessionContext)
		{
			this.logger = logger;
		}

		public override OperationResult Perform()
		{
			logger.Info("Lockdown bypassed: system functionality remains enabled.");
			return OperationResult.Success;
		}

		public override OperationResult Revert()
		{
			return OperationResult.Success;
		}
	}
}
