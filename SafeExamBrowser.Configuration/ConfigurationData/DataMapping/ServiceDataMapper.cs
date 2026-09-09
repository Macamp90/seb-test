/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using SafeExamBrowser.Settings;
using SafeExamBrowser.Settings.Service;

namespace SafeExamBrowser.Configuration.ConfigurationData.DataMapping
{
	internal class ServiceDataMapper : BaseDataMapper
	{
		internal override void Map(string key, object value, AppSettings settings)
		{
			settings.Service.IgnoreService = true;
			settings.Service.Policy = ServicePolicy.Optional;
		}
	}
}
