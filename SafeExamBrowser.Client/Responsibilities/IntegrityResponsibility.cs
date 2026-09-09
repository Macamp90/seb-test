/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using SafeExamBrowser.Client.Contracts;
using SafeExamBrowser.I18n.Contracts;
using SafeExamBrowser.Logging.Contracts;

namespace SafeExamBrowser.Client.Responsibilities
{
	internal class IntegrityResponsibility : ClientResponsibility
	{
		public IntegrityResponsibility(ClientContext context, ICoordinator coordinator, ILogger logger, IText text) : base(context, logger)
		{
		}

		public override void Assume(ClientTask task)
		{
			// Integrity monitoring disabled so it never locks the screen
		}
	}
}
