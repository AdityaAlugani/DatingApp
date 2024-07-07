namespace API.Error
{
    public class Errorpropogate
    {
        public string Message;
        public string StackTrace;
        public int StatusCode;

        public Errorpropogate(string message,string stacktrace,int statuscode)
        {
            Message=message;
            StackTrace=stacktrace;
            StatusCode=statuscode;
        }
    }
}