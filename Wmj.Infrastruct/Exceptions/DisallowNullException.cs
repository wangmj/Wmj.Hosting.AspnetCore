namespace Wmj.Infrastruct.Exceptions
{
    public class DisallowNullException:ApplicationException
    {
        public DisallowNullException(string varName):base($"The variable:{varName} is not allowd null")
        {

        }
    }
}
