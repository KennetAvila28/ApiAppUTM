using System.Text.Json;
using System.Threading.Tasks;
using AppUTM.Enum;
using AppUTM.Interfaces;
using Microsoft.JSInterop;

namespace AppUTM.Helpers
{
    public class LocalStorage : ILocalStorage
    {
        private readonly IJSRuntime _jsRuntime;

        public LocalStorage(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        private readonly string _storageType = "sessionStorage.";

        public async Task ClearAll()
        {
            await _jsRuntime.InvokeVoidAsync($"{_storageType}clear").ConfigureAwait(false);
        }

        public async Task<T> GetValue<T>(ValueKeys key)
        {
            var data = await _jsRuntime.InvokeAsync<string>($"{_storageType}getItem", key.ToString()).ConfigureAwait(false);
            return IsDataNull.Check<T>(data);
        }

        public async Task RemoveItem(ValueKeys key)
        {
            await _jsRuntime.InvokeVoidAsync($"{_storageType}removeItem", key.ToString()).ConfigureAwait(false);
        }

        public async Task SetValue<T>(ValueKeys key, T value)
        {
            await _jsRuntime.InvokeVoidAsync($"{_storageType}setItem", key.ToString(), JsonSerializer.Serialize(value)).ConfigureAwait(false);
        }
    }
}