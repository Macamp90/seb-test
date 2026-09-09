/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using SafeExamBrowser.Browser.Contracts.Filters;
using SafeExamBrowser.Settings.Browser.Filter;

namespace SafeExamBrowser.Browser.Filters
{
	internal class RequestFilter : IRequestFilter
	{
		public FilterResult Default { get; set; }

		internal RequestFilter()
		{
			Default = FilterResult.Allow;
		}

		public void Load(IRule rule)
		{
		}

		public FilterResult Process(Request request)
		{
			return FilterResult.Allow;
		}
	}
}
