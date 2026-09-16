using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Handling request: {Request} - Response: {Response} - ResponseData: {ResponseData}", 
                typeof(TRequest).Name, typeof(TResponse).Name,request);


            var timer = new Stopwatch();
            timer.Start();

            var response = await next();    

            timer.Stop();
            var timeTaken = timer.Elapsed;
            if(timeTaken.Seconds > 3)
              logger.LogWarning("[SLOW-PERFORMANCE] Handling request: {Request} - Response: {Response} - TimeTaken: {TimeTaken}sec",typeof(TRequest).Name, typeof(TResponse).Name, timeTaken);
            
            logger.LogInformation("[END] Handling request: {Request} - Response: {Response} - ResponseData: {ResponseData}",
                typeof(TRequest).Name, typeof(TResponse).Name, response);

            return response;
        }
    }
}
