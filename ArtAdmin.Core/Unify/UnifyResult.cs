namespace ArtAdmin
{
    public class UnifyResult<T>
    {
        public HttpStatusCode Code { get; set; }
        public T Data { get; set; }
        public string Msg { get; set; }
        public object Exts { get; set; }
        public long Timestamp { get; set; }
        public long ElapsedMilliseconds { get; set; }
    }
}