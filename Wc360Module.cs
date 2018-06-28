using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WC360
{
    public class Wc360Module : BackgroundService
    {
        private readonly ILogger<Wc360Module> _logger;
        private readonly Wc360Options _options;
        private PointCalculator Calculator{get;set;}

        public Wc360Module(IOptions<Wc360Options> settings, ILogger<Wc360Module> logger, PointCalculator calculator)
        {
            _logger = logger;
            _options = settings.Value;
            Calculator = calculator;
        }

        protected override async Task ExecuteAsync(CancellationToken token)
        {
            _logger.LogDebug("Wc360 module starting");
            token.Register(() => _logger.LogDebug("Wc360 module stopping"));

            while (!token.IsCancellationRequested)
            {
                _logger.LogDebug("Wc360 module running in background");
                Calculator.CalculateBets();

                await Task.Delay(_options.Delay, token);
            }

            _logger.LogDebug("Wc360 module is stopping");
        }
    }
}
