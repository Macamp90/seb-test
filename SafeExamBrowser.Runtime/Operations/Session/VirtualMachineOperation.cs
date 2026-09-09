/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 *
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using SafeExamBrowser.Core.Contracts.OperationModel;
using SafeExamBrowser.Core.Contracts.OperationModel.Events;
using SafeExamBrowser.I18n.Contracts;
using SafeExamBrowser.Monitoring.Contracts;

namespace SafeExamBrowser.Runtime.Operations.Session
{
	internal class VirtualMachineOperation : SessionOperation
	{
		public override event StatusChangedEventHandler StatusChanged;

		public VirtualMachineOperation(Dependencies dependencies, IVirtualMachineDetector detector) : base(dependencies)
		{
		}

		public override OperationResult Perform()
		{
			return ValidatePolicy();
		}

		public override OperationResult Repeat()
		{
			return ValidatePolicy();
		}

		public override OperationResult Revert()
		{
			return OperationResult.Success;
		}

		private OperationResult ValidatePolicy()
		{
			Logger.Info("Validating virtual machine policy (bypassed: running in VM allowed)...");
			StatusChanged?.Invoke(TextKey.OperationStatus_ValidateVirtualMachinePolicy);
			return OperationResult.Success;
		}
	}
}
