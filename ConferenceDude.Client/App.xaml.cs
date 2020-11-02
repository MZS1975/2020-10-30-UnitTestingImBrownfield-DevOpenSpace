namespace ConferenceDude.UI
{
    using System;
    using System.Windows;
    using Application;
    using Application.Conference;
    using Application.Session;
    using Domain.Session;
    using Domain.Session.Policies;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Services;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                       .ConfigureServices((context, services) =>
                                          {
                                              ConfigureServices(context.Configuration, services);
                                          })
                       .Build();
        }

        private void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            var conferencePlanner = new ConferencePlanner(configuration);
            services.AddSingleton(conferencePlanner.ConferencePlanning);
            services.AddSingleton<IUserInteractionService, UserInteractionService>();
            services.AddSingleton<SessionsViewModel>();
            services.AddSingleton<SessionWindow>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var sessionWindow = _host.Services.GetRequiredService<SessionWindow>();
            sessionWindow.Show();
            
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }
    }
}
