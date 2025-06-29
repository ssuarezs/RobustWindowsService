﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

using RobustWindowsService.Application;
using RobustWindowsService.Application.Convenios;
using RobustWindowsService.Application.Worker;

namespace RobustWindowsService
{
    public partial class MainService : ServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IServiceProvider _serviceProvider;
        private readonly CancellationTokenSource _cts;

        public MainService()
        {
            InitializeComponent();
            _serviceProvider = DependencyContainer.ConfigureServices();
            _mediator = _serviceProvider.GetRequiredService<IMediator>();
            _cts = new CancellationTokenSource();
        }

        protected override void OnStart(string[] args)
        {
            _mediator.SendCommandAsync(new StartProcessingCommand(), _cts.Token).GetAwaiter().GetResult();

            var command = new CreateMyAggregateCommand { Cupo = 7500, FechaVigencia = DateTime.Now.AddMonths(6) };
            var nuevoConvenioId = _mediator.SendCommandAsync<CreateMyAggregateCommand, Guid>(command).GetAwaiter().GetResult();
            Console.WriteLine($"[PRUEBA] Se ha creado un nuevo agg con ID: {nuevoConvenioId}");
        }

        protected override void OnStop()
        {
            _cts.Cancel();
            Task.Delay(5000).GetAwaiter().GetResult();
        }

        public void DebugRun()
        {
            OnStart(null);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cts?.Dispose();

                if (_serviceProvider is IDisposable disposable)
                {
                    disposable.Dispose(); // Esto cerrará todos los singletons y liberará el contenedor.
                }

                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
