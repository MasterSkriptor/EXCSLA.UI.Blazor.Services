using EXCSLA.Shared.Core.ValueObjects.Common;
using System;
using System.Collections.Generic;

namespace EXCSLA.UI.Blazor.Services
{
    public class AlertService : IAlertService
    {
        public event EventHandler<Alert> OnAlert;

        public List<Alert> Alerts { get; set; }

        public void AddAlert(string message) => AddAlert(message, AlertType.Info);
        public void AddAlert(string message, AlertType type) => OnAlert.Invoke(this, new Alert(message, type));

    }
}
