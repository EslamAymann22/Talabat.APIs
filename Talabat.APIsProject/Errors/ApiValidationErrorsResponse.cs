namespace Talabat.APIsProject.Errors
{
    public class ApiValidationErrorsResponse : ApiResponses
    {
        public IEnumerable<string>Errors { get; set; }
        public ApiValidationErrorsResponse() : base(400)
        {
            Errors = new List<string>();
        }
    }
}
