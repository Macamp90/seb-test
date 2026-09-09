/*
 * Copyright (c) 2026 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using SafeExamBrowser.Configuration.Contracts.Integrity;
using SafeExamBrowser.Logging.Contracts;

namespace SafeExamBrowser.Runtime.Responsibilities
{
	internal class IntegrityResponsibility : RuntimeResponsibility
	{
		public IntegrityResponsibility(
			IIntegrityModule integrityModule,
			ILogger logger,
			RuntimeContext runtimeContext,
			Action shutdown) : base(logger, runtimeContext)
		{
		}

		public override void Assume(RuntimeTask task)
		{
			// Monitoring disabled
		}
	}
}
