using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MVC5_Ref.Tests.Extensions
{
    public static class ExceptionAssert
    {
        //https://stackoverflow.com/questions/933613/how-do-i-use-assert-to-verify-that-an-exception-has-been-thrown
        public static TException Throws<TException>(Action action) where TException : Exception
        {
            try
            {
                action();
                throw new AssertFailedException(String.Format("An exception of type {0} was expected, but not thrown", typeof(TException)));
            }
            catch (TException ex)
            {
                return ex;
            }
        }
    }
}
