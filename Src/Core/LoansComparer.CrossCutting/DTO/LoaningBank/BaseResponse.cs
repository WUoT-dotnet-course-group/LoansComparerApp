using System.Net;

namespace LoansComparer.CrossCutting.DTO.LoaningBank
{
    public class BaseResponse<T> where T : class
    {
        public static BaseResponse<T> Empty => new();

        public T? Content { get; set; }

        public HttpStatusCode ErrorStatusCode { get; set; }
    }
}
