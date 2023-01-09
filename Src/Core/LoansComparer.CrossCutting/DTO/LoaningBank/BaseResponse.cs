using System.Net;

namespace LoansComparer.CrossCutting.DTO.LoaningBank
{
    public class BaseResponse<T> where T : class
    {
        public T? Content { get; set; }

        public bool IsSuccessful { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
