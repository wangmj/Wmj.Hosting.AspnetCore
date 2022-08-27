using Wmj.Infrastruct.Exceptions;

namespace Wmj.Infrastruct
{
    public static class Guard
    {
        public static void ThrowIfNull(object obj,string name)
        {
            if(obj == null)
                throw new DisallowNullException(name);
        }
    }
}
