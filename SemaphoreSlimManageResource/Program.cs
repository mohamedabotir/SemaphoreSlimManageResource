namespace SemaphoreSlimManageResource
{
    internal class Program
    {
        public static SemaphoreSlim _resource = new SemaphoreSlim(1);
       public static HttpClient _client= new HttpClient()
       {
           Timeout= TimeSpan.FromSeconds(5),
       };
        static async Task Main(string[] args)
        {
           Task.WaitAll(Program.ProcessWork().ToArray());
        }
       static async Task InvokeWork() {

            try
            {
            await    _resource.WaitAsync();
          var _request = await  _client.GetAsync("https://www.bing.com/");
                Console.WriteLine(_request.StatusCode.ToString());
                _resource.Release();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }

       static  IEnumerable<Task> ProcessWork() {
            for (int i = 0; i < 1000; i++)
            {
                yield return  InvokeWork();
            }
        }
    }
}