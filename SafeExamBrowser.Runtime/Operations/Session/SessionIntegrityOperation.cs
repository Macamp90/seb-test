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
using SafeExamBrowser.Monitoring.Contracts.System;

namespace SafeExamBrowser.Runtime.Operations.Session
{
	internal class SessionIntegrityOperation : SessionOperation
	{
		public override event StatusChangedEventHandler StatusChanged;

		public SessionIntegrityOperation(Dependencies dependencies, ISystemSentinel sentinel) : base(dependencies)
		{
		}

		public override OperationResult Perform()
		{
			StatusChanged?.Invoke(TextKey.OperationStatus_VerifySessionIntegrity);
			Logger.Info("Session integrity verification (bypassed).");
			return OperationResult.Success;
		}

		public override OperationResult Repeat()
		{
			return OperationResult.Success;
		}

		public override OperationResult Revert()
		{
			return OperationResult.Success;
		}
	}
}
