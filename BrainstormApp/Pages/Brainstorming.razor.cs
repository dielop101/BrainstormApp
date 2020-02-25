using BrainstormApp.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.ProtectedBrowserStorage;
using BrainstormApp.ComponentModel;
using Microsoft.AspNetCore.SignalR.Client;

namespace BrainstormApp.Pages
{
    public class BrainstormingBase : ComponentBase
    {
        [Inject] ICodeService CodeService { get; set; }
        [Inject] IAnswersService AnswersService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] ProtectedSessionStorage ProtectedSessionStore { get; set; }

        protected IEnumerable<string> _answers = new List<string>() { };
        protected string _answer { get; set; }

        private string _codeStorage;

        private HubConnection _hubConnection;
        private List<string> _messages = new List<string>();

        protected override async Task OnInitializedAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithAutomaticReconnect()
                .WithUrl(NavigationManager.ToAbsoluteUri("/Brainstorming"))
                .Build();

            _hubConnection.On("AnswerNotify", () =>
            {
                _answers = AnswersService.GetAnswersByCode(_codeStorage);
                StateHasChanged();
            });

            await _hubConnection.StartAsync();
        }

        public bool IsConnected =>
        _hubConnection.State == HubConnectionState.Connected;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                try
                {
                    _codeStorage = await ProtectedSessionStore.GetAsync<string>("code");

                    var code = new Code { CodeValue = _codeStorage };
                    var isValid = CodeService.CheckCode(code);
                    if (!isValid)
                    {
                        NavigationManager.NavigateTo("/");
                    }

                    _answers = AnswersService.GetAnswersByCode(_codeStorage);
                    StateHasChanged();

                }
                catch (Exception)
                {
                    NavigationManager.NavigateTo("/");
                }
            }
        }

        protected void AddAnswer()
        {
            AnswersService.SetAnswerByCode(_codeStorage, _answer);

            _answer = string.Empty;
            _answers = AnswersService.GetAnswersByCode(_codeStorage);

            _hubConnection.SendAsync("SendMessage");

            StateHasChanged();
        }
    }
}
