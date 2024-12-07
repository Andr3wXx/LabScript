
using Microsoft.JSInterop;
using System.Diagnostics.Contracts;

namespace LabScript
{
    public class AlertService : IAsyncDisposable, IAlertService
    {
        readonly Lazy<Task<IJSObjectReference>> ijsObjectReference;

        public AlertService(IJSRuntime ijsRuntime)
        {
            this.ijsObjectReference = new Lazy<Task<IJSObjectReference>>(() =>
            ijsRuntime.InvokeAsync<IJSObjectReference>("import",
            "./Home.js").AsTask());


        }
        public async ValueTask DisposeAsync()
        {
            if (ijsObjectReference.IsValueCreated)
            {
                IJSObjectReference moduleJs = await ijsObjectReference.Value;
                await moduleJs.DisposeAsync();
            }
        }

        public async Task CallJsAlertJsFuction()
        {
            var jsModule = await ijsObjectReference.Value;

            await jsModule.InvokeVoidAsync("jsFuncion");

        }
    }
}
