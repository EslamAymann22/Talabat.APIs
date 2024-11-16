using System.Runtime.CompilerServices;

namespace Talabat.APIsProject.Extensions
{
    public static class AddSwaggerExtension
    {

        public static WebApplication UseSwaggerMiddleWares(this WebApplication App)
        {

            App.UseSwagger();
            App.UseSwaggerUI();
            return App;

        }

    }
}
