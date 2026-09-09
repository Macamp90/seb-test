/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using SafeExamBrowser.Communication.Contracts.Hosts;
using SafeExamBrowser.Communication.Contracts.Proxies;
using SafeExamBrowser.Configuration.Contracts;
using SafeExamBrowser.Core.Contracts.OperationModel;
using SafeExamBrowser.Core.Contracts.OperationModel.Events;
using SafeExamBrowser.I18n.Contracts;
using SafeExamBrowser.SystemComponents.Contracts;

namespace SafeExamBrowser.Runtime.Operations.Session
{
	internal class ServiceOperation : SessionOperation
	{
		public override event StatusChangedEventHandler StatusChanged;

		public ServiceOperation(
			Dependencies dependencies,
			IRuntimeHost runtimeHost,
			IServiceProxy serviceProxy,
			int timeout_ms,
			IUserInfo userInfo) : base(dependencies)
		{
		}

		public override OperationResult Perform()
		{
			Logger.Info("Service operation (bypassed: service not required)...");
			StatusChanged?.Invoke(TextKey.OperationStatus_InitializeServiceSession);
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
