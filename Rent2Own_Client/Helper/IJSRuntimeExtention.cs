using Microsoft.JSInterop;

namespace Rent2Own_Client.Helper
{
    public static class IJSRuntimeExtention
    {
        public static async ValueTask ToastrSuccess(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("ShowToastr", "success", message);
        }

        public static async ValueTask ToastrError(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("ShowToastr", "error", message);
        }
        public static async ValueTask SweetSuccess(this IJSRuntime jsRuntime, string m1, string m2)
        {
            await jsRuntime.InvokeVoidAsync("ShowSwal", "success", m1, m2);
        }
        public static async ValueTask SweetError(this IJSRuntime jsRuntime, string m1, string m2)
        {
            await jsRuntime.InvokeVoidAsync("ShowSwal", "error", m1, m2);
        }
        public static async ValueTask SweetPic(this IJSRuntime jsRuntime, string m1, string m2)
        {
            await jsRuntime.InvokeVoidAsync("ShowPic", m1, m2);
        }

    }
}
