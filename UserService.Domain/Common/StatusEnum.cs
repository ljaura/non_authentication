namespace UserService.Domain.Common
{
    public enum StatusEnum
    {
        /// <summary>
        /// All went well, and (usually) some data was returned.
        /// </summary>
        Success = 1,
        /// <summary>
        /// There was a problem with the data submitted, or some pre-condition of the API call wasn't satisfied
        /// </summary>
        Fail = 2,
        /// <summary>
        /// An error occurred in processing the request, i.e. an exception was thrown
        /// </summary>
        Error = 3
    }
}
