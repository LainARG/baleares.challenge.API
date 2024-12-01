namespace baleares.challenge.API.infrastructure.services.utilities;

    public class ExceptionHelper
    {
         public static string GetExceptionMessage(Exception ex)
         { 
            return ex.InnerException?.Message ?? ex.Message;
         }
    }