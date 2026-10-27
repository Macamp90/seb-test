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
using SafeExamBrowser.UserInterface.Contracts.MessageBox;

namespace SafeExamBrowser.Runtime.Operations.Session
{
	// Custom test build: Screen Proctoring is absent and must never be reported as active.
	internal class DisclaimerOperation : SessionOperation
	{
		private readonly IMessageBox messageBox;

		public override event StatusChangedEventHandler StatusChanged;

		public DisclaimerOperation(Dependencies dependencies) : base(dependencies)
		{
			messageBox = dependencies.MessageBox;
		}

		public override OperationResult Perform()
		{
			return ConfirmTestMode();
		}

		public override OperationResult Repeat()
		{
			return ConfirmTestMode();
		}

		public override OperationResult Revert()
		{
			return OperationResult.Success;
		}

		private OperationResult ConfirmTestMode()
		{
			StatusChanged?.Invoke(TextKey.OperationStatus_WaitDisclaimerConfirmation);

			var proctoring = Context.Next.Settings.Proctoring;
			var requested = proctoring.Enabled || proctoring.ScreenProctoring.Enabled;
			var title = "BUILD DI TEST - SCREEN PROCTORING ASSENTE";
			var message = "Questa build personalizzata non include Screen Proctoring: il modulo non acquisisce né trasmette schermate. " +
				"Non è una sessione monitorata. Usare esclusivamente per test autorizzati. " +
				"Il server potrebbe rifiutare questa build o richiedere il client ufficiale.";

			if (requested)
			{
				message += "\n\nLa configurazione caricata richiede il proctoring: continuando, tale richiesta viene disabilitata nelle impostazioni effettive di questa sessione di test.";
			}

			message += "\n\nContinuare in modalità di test senza Screen Proctoring?";

			var result = ClientBridge.IsRequired()
				? ClientBridge.ShowMessageBox(message, title, MessageBoxAction.OkCancel, MessageBoxIcon.Warning)
				: messageBox.Show(message, title, MessageBoxAction.OkCancel, MessageBoxIcon.Warning, RuntimeWindow);

			if (result != MessageBoxResult.Ok)
			{
				Logger.Warn("The user declined the explicit no-screen-proctoring test mode. Aborting session initialization.");
				return OperationResult.Aborted;
			}

			proctoring.ScreenProctoring.Enabled = false;
			proctoring.Enabled = false;
			Logger.Warn($"CUSTOM TEST BUILD: Screen Proctoring is unavailable and disabled in effective session settings. Configuration requested proctoring: {requested}. User explicitly confirmed unmonitored test mode.");

			return OperationResult.Success;
		}
	}
}
