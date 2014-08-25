using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;
using Owin;
class Program
    {
        private static ManualResetEvent _quitEvent = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            var port = 5000;
            if (args.Length > 0)
            {
                int.TryParse(args[0], out port);
            }

            Console.CancelKeyPress += (sender, eArgs) =>
            {
                _quitEvent.Set();
                eArgs.Cancel = true;
            };

 var options = new StartOptions
            {
                ServerFactory = "Microsoft.Owin.Host.HttpListener",
            };
options.Urls.Add("http://*:" + port);

            using (WebApp.Start<Startup>(options))
            {
                Console.WriteLine(string.Format("Running a http server on port {0}", port));
                Console.ReadKey();
            }
        }
    }

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Run(context =>
         {
            if (context.Request.Path.Value == "/")
             {
               context.Response.ContentType = "text/plain";
             return context.Response.WriteAsync("Hello World!");
                }

              context.Response.StatusCode = 404;
            return Task.Delay(0);
      });
        }
    }


