/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 *
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using SafeExamBrowser.Configuration.Contracts.Integrity;
using SafeExamBrowser.Core.Contracts.OperationModel;
using SafeExamBrowser.Core.Contracts.OperationModel.Events;
using SafeExamBrowser.I18n.Contracts;
using SafeExamBrowser.Logging.Contracts;

namespace SafeExamBrowser.Runtime.Operations.Bootstrap
{
	internal class ApplicationIntegrityOperation : IOperation
	{
		public event StatusChangedEventHandler StatusChanged;

		public ApplicationIntegrityOperation(IIntegrityModule module, ILogger logger)
		{
		}

		public OperationResult Perform()
		{
			StatusChanged?.Invoke(TextKey.OperationStatus_VerifyApplicationIntegrity);
			return OperationResult.Success;
		}

		public OperationResult Revert()
		{
			return OperationResult.Success;
		}
	}
}
