using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace WC360
{
    public abstract class BackgroundService :  IHostedService, IDisposable
    {
        private Task _mainTask;
        private readonly CancellationTokenSource _stoppingCts =
            new CancellationTokenSource();

        protected abstract Task ExecuteAsync(CancellationToken token);

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            _mainTask = ExecuteAsync(_stoppingCts.Token);

            if (_mainTask.IsCompleted)
            {
                return _mainTask;
            }

            return Task.CompletedTask;
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_mainTask == null)
            {
                return;
            }

            try
            {
                _stoppingCts.Cancel();
            }
            finally
            {
                await Task.WhenAny(_mainTask, Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        public virtual void Dispose()
        {
            _stoppingCts.Cancel();
        }
    }
}
