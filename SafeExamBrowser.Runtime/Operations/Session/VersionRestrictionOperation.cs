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

namespace SafeExamBrowser.Runtime.Operations.Session
{
	internal class VersionRestrictionOperation : SessionOperation
	{
		public override event StatusChangedEventHandler StatusChanged;

		public VersionRestrictionOperation(Dependencies dependencies) : base(dependencies)
		{
		}

		public override OperationResult Perform()
		{
			return ValidateRestrictions();
		}

		public override OperationResult Repeat()
		{
			return ValidateRestrictions();
		}

		public override OperationResult Revert()
		{
			return OperationResult.Success;
		}

		private OperationResult ValidateRestrictions()
		{
			Logger.Info("Validating version restrictions (bypassed)...");
			StatusChanged?.Invoke(TextKey.OperationStatus_ValidateVersionRestrictions);
			return OperationResult.Success;
		}
	}
}
