using SequelyMAUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudBlazor;
using SequelyMAUI.Components.Utils;

namespace SequelyMAUI.Services
{
    public class ErrorProvider : IErrorProvider
    {
        private readonly IDialogService _dialogService;

        public ErrorProvider(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }
        public async Task ProvideError(string message)
        {
            //var options = new DialogOptions { CloseButton = true, DisableBackdropClick = true };

            //var parameters = new DialogParameters<InfoDialog>();
            //parameters.Add(x => x.ContentText, message);

            //var dialog = await _dialogService.ShowAsync<InfoDialog>("Error", parameters, options);

            //await dialog.Result;

            await _dialogService.ShowMessageBox(message.ToString(), "Error");


        }
    }
}
