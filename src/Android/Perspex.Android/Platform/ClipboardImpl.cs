using Android.Content;
using Android.Runtime;
using Android.Views;
using Perspex.Input.Platform;
using Perspex.Platform;
using System.Threading.Tasks;

namespace Perspex.Android.Platform
{
    internal class ClipboardImpl : IClipboard
    {
        private Context context = (PerspexLocator.Current.GetService<IWindowImpl>() as View).Context;

        private ClipboardManager ClipboardManager
        {
            get
            {
                return this.context.GetSystemService(Context.ClipboardService).JavaCast<ClipboardManager>();
            }
        }

        public Task<string> GetTextAsync()
        {
            if (ClipboardManager.HasPrimaryClip)
            {
                return Task.FromResult<string>(ClipboardManager.PrimaryClip.GetItemAt(0).Text);
            }

            return Task.FromResult<string>(null);
        }

        public Task SetTextAsync(string text)
        {
            ClipData clip = ClipData.NewPlainText("text", text);
            ClipboardManager.PrimaryClip = clip;

            return Task.FromResult<object>(null);
        }

        public Task ClearAsync()
        {
            ClipboardManager.PrimaryClip = null;

            return Task.FromResult<object>(null);
        }
    }
}