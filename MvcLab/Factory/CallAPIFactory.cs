using MvcLab.NetTool;

namespace MvcLab.Factory
{
    public class CallAPIFactory
    {
        internal static ICallAPI? callAPIMoke;

        internal static ICallAPI Generate()
        {
            if (callAPIMoke != null)
            {
                Console.WriteLine("test mode: get callAPIMoke");
                return callAPIMoke;
            }
            return new CallAPI();
        }
    }
}
