/*
 * Copyright (c) 2025 ETH Zürich, IT Services
 *
 * This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using SafeExamBrowser.Configuration.Contracts;
using SafeExamBrowser.Runtime.Desktop;

namespace SafeExamBrowser.Runtime
{
	public class App : Application
	{
		private static readonly Mutex Mutex = new Mutex(true, AppConfig.RUNTIME_MUTEX_NAME);
		private static readonly DesktopAppearance DesktopAppearance = new DesktopAppearance();
		private readonly CompositionRoot instances = new CompositionRoot();

		[STAThread]
		public static void Main()
		{
			try
			{
				StartApplication();
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message + "\n\n" + e.StackTrace, "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			finally
			{
				RestoreDesktop();
				Mutex.Close();
			}
		}

		private static void StartApplication()
		{
			if (NoInstanceRunning())
			{
				new App().Run();
			}
			else
			{
				MessageBox.Show("You can only run one instance of SEB at a time.", "Startup Not Allowed", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private static bool NoInstanceRunning()
		{
			return Mutex.WaitOne(TimeSpan.Zero, true);
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			ShutdownMode = ShutdownMode.OnExplicitShutdown;

			var desktopWarning = DesktopAppearance.Apply();

			if (!string.IsNullOrEmpty(desktopWarning))
			{
				MessageBox.Show(desktopWarning, "Personalizzazione desktop incompleta", MessageBoxButton.OK, MessageBoxImage.Warning);
			}

			instances.BuildObjectGraph(Shutdown);
			instances.LogStartupInformation();

			Task.Run(new Action(TryStart));
		}

		private void TryStart()
		{
			var success = instances.RuntimeController.TryStart();

			if (!success)
			{
				Shutdown();
			}
		}

		public new void Shutdown()
		{
			Task.Run(new Action(ShutdownInternal));
		}

		private void ShutdownInternal()
		{
			try
			{
				instances.RuntimeController.Terminate();
				instances.LogShutdownInformation();
			}
			finally
			{
				RestoreDesktop();
				Dispatcher.Invoke(base.Shutdown);
			}
		}

		protected override void OnExit(ExitEventArgs e)
		{
			RestoreDesktop();
			base.OnExit(e);
		}

		private static void RestoreDesktop()
		{
			var warning = DesktopAppearance.Restore();

			if (!string.IsNullOrEmpty(warning))
			{
				MessageBox.Show(warning + "\n\nÈ possibile ripristinare lo sfondo da Impostazioni > Personalizzazione e le icone da Visualizza > Mostra icone del desktop.",
					"Ripristino desktop incompleto", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
		}
	}
}
