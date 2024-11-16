using Microsoft.AspNetCore.Mvc;
using Talabat.APIsProject.Errors;
using Talabat.APIsProject.Helper;
using Talabat.Core.Repositories;
using Talabat.Repository;

namespace Talabat.APIsProject.Extensions
{
    public static class ApplicationServicesExtension
    {

        public static IServiceCollection AddApplicationServices (this IServiceCollection Services) {

            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            //builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            Services.AddAutoMapper(typeof(MappingProfiles));
            //builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(M => M.Value.Errors.Count() > 0)
                                             .SelectMany(E => E.Value.Errors)
                                             .Select(E => E.ErrorMessage)
                                             .ToList();
                    var ValidationErrorResponse = new ApiValidationErrorsResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });

            return Services;
        }

    }
}
