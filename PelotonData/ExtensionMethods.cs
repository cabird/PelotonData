using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PelotonData
{
    public static class ExtensionMethods
    {
        public static Task<string> DownloadStringTaskAsync(this WebClient client, Uri uri)
        {
            var tcs = new TaskCompletionSource<string>();

            client.DownloadStringCompleted += (o, e) =>
            {
                if (e.Error != null)
                    tcs.SetException(e.Error);
                else if (e.Cancelled)
                    tcs.SetCanceled();
                else
                    tcs.SetResult(e.Result);
            };

            client.DownloadStringAsync(uri);
            return tcs.Task;
        }
    }
}
