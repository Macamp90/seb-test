/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 * 
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.IO;
using System.Text.RegularExpressions;
using CefSharp;
using SafeExamBrowser.Browser.Contracts.Filters;
using SafeExamBrowser.Browser.Events;
using SafeExamBrowser.Configuration.Contracts;
using SafeExamBrowser.Logging.Contracts;
using SafeExamBrowser.Settings.Browser;
using BrowserSettings = SafeExamBrowser.Settings.Browser.BrowserSettings;

namespace SafeExamBrowser.Browser.Handlers
{
	internal class RequestHandler : CefSharp.Handler.RequestHandler
	{
		private readonly AppConfig appConfig;
		private readonly IRequestFilter filter;
		private readonly ILogger logger;
		private readonly ResourceHandler resourceHandler;
		private readonly WindowSettings windowSettings;
		private readonly BrowserSettings settings;

		private string quitUrlPattern;

		internal event UrlEventHandler QuitUrlVisited;
		internal event UrlEventHandler RequestBlocked;

		internal RequestHandler(
			AppConfig appConfig,
			IRequestFilter filter,
			ILogger logger,
			ResourceHandler resourceHandler,
			BrowserSettings settings,
			WindowSettings windowSettings)
		{
			this.appConfig = appConfig;
			this.filter = filter;
			this.logger = logger;
			this.resourceHandler = resourceHandler;
			this.settings = settings;
			this.windowSettings = windowSettings;
		}

		protected override bool GetAuthCredentials(IWebBrowser webBrowser, IBrowser browser, string originUrl, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback)
		{
			if (isProxy)
			{
				foreach (var proxy in settings.Proxy.Proxies)
				{
					if (proxy.RequiresAuthentication && host?.Equals(proxy.Host, StringComparison.OrdinalIgnoreCase) == true && port == proxy.Port)
					{
						callback.Continue(proxy.Username, proxy.Password);

						return true;
					}
				}
			}

			return base.GetAuthCredentials(webBrowser, browser, originUrl, isProxy, host, port, realm, scheme, callback);
		}

		protected override IResourceRequestHandler GetResourceRequestHandler(IWebBrowser webBrowser, IBrowser browser, IFrame frame, IRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
		{
			return resourceHandler;
		}

		protected override bool OnBeforeBrowse(IWebBrowser webBrowser, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect)
		{
			if (IsQuitUrl(request))
			{
				QuitUrlVisited?.Invoke(request.Url);

				return true;
			}

			if (IsConfigurationFile(request, out var downloadUrl))
			{
				browser.GetHost().StartDownload(downloadUrl);

				return true;
			}

			return base.OnBeforeBrowse(webBrowser, browser, frame, request, userGesture, isRedirect);
		}

		protected override bool OnOpenUrlFromTab(IWebBrowser webBrowser, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture)
		{
			return base.OnOpenUrlFromTab(webBrowser, browser, frame, targetUrl, targetDisposition, userGesture);
		}

		private bool IsConfigurationFile(IRequest request, out string downloadUrl)
		{
			var isValidUri = Uri.TryCreate(request.Url, UriKind.RelativeOrAbsolute, out var uri);
			var hasFileExtension = string.Equals(appConfig.ConfigurationFileExtension, Path.GetExtension(uri.AbsolutePath), StringComparison.OrdinalIgnoreCase);
			var isDataUri = request.Url.Contains(appConfig.ConfigurationFileMimeType);
			var isConfigurationFile = isValidUri && (hasFileExtension || isDataUri);

			downloadUrl = request.Url;

			if (isConfigurationFile)
			{
				if (isDataUri)
				{
					if (uri.Scheme == appConfig.SebUriScheme)
					{
						downloadUrl = request.Url.Replace($"{appConfig.SebUriScheme}{Uri.SchemeDelimiter}", "data:");
					}
					else if (uri.Scheme == appConfig.SebUriSchemeSecure)
					{
						downloadUrl = request.Url.Replace($"{appConfig.SebUriSchemeSecure}{Uri.SchemeDelimiter}", "data:");
					}
				}
				else
				{
					if (uri.Scheme == appConfig.SebUriScheme)
					{
						downloadUrl = new UriBuilder(uri) { Scheme = Uri.UriSchemeHttp }.Uri.AbsoluteUri;
					}
					else if (uri.Scheme == appConfig.SebUriSchemeSecure)
					{
						downloadUrl = new UriBuilder(uri) { Scheme = Uri.UriSchemeHttps }.Uri.AbsoluteUri;
					}
				}

				logger.Debug($"Detected configuration file {(windowSettings.UrlPolicy.CanLog() ? $"'{uri}'" : "")}.");
			}

			return isConfigurationFile;
		}

		private bool IsQuitUrl(IRequest request)
		{
			var isQuitUrl = false;

			if (!string.IsNullOrWhiteSpace(settings.QuitUrl))
			{
				if (quitUrlPattern == default)
				{
					quitUrlPattern = $"^{Regex.Escape(settings.QuitUrl.TrimEnd('/'))}/?$";
				}

				isQuitUrl = Regex.IsMatch(request.Url, quitUrlPattern, RegexOptions.IgnoreCase);

				if (isQuitUrl)
				{
					logger.Debug($"Detected quit URL{(windowSettings.UrlPolicy.CanLog() ? $"'{request.Url}'" : "")}.");
				}
			}

			return isQuitUrl;
		}
	}
}
