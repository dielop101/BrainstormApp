
using BrainstormApp.ComponentModel;
using BrainstormApp.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.ProtectedBrowserStorage;

namespace BrainstormApp.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject] ICodeService CodeService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] ProtectedSessionStorage ProtectedSessionStore { get; set; }

        protected Code _codeModel = new Code();
        protected bool _isValid = true;
        protected async void HandleValidCode()
        {
            _isValid = CodeService.CheckCode(_codeModel);
            if (_isValid)
            {
                await ProtectedSessionStore.SetAsync("code", _codeModel.CodeValue);
                NavigationManager.NavigateTo("Brainstorming");
            }
        }
    }
}
