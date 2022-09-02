using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoApplicationInsights.Simple.MVC.Filters
{
    public class PageViewActionFilterAttribute : ActionFilterAttribute
    {
        private readonly TelemetryClient _telemetryClient;

        public PageViewActionFilterAttribute(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            if(context.HttpContext.Request.Method == "GET")
            {
                PageViewTelemetry pageViewTelemetry = new(context.HttpContext.Request.Path);

                // Si el usuario ha iniciado sesión registramos cual fue el usuario que visitó la página 

                pageViewTelemetry.Context.GlobalProperties.Add("email", "hello@lautarocarro.com");

                _telemetryClient.TrackPageView(pageViewTelemetry);
            }

            
        }
    }
}
