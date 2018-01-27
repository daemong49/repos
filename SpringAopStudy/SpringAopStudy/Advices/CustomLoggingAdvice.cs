using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AopAlliance.Intercept;
using Spring.Aop;
using Spring.Core;
using log4net;
using System.Reflection;
using System.Diagnostics;

namespace SpringAopStudy.Advices
{
    public class CustomLoggingAdvice : IMethodInterceptor
    {
        ILog _logger = LogManager.GetLogger(typeof(CustomLoggingAdvice));

        public object Invoke(IMethodInvocation invocation)
        {
            object returnValue = null;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
           
            returnValue = invocation.Proceed();

            stopwatch.Stop();
            //_logger.Info($"{invocation.Method.Name} Excuted:{stopwatch.Elapsed.Milliseconds.ToString("n0")}");


            return returnValue;
        }
    }

    public class CustomLoggingBeforeAdvice : IMethodBeforeAdvice
    {
        ILog _logger = LogManager.GetLogger(typeof(CustomLoggingBeforeAdvice));
        public void Before(MethodInfo method, object[] args, object target)
        {
            _logger.Info($"Intercepted call to this method : {method.Name}");
            _logger.Info($"     The target is              : {target}");
            _logger.Info($"     The arguments are          :");
            if(args != null)
            {
                foreach(var arg in args)
                {
                    _logger.Info($"\t\t:{arg}");
                }
            }
        }
    }

    public class CustomLoggingAfterAdvice : IAfterReturningAdvice
    {
        ILog _logger = LogManager.GetLogger(typeof(CustomLoggingAfterAdvice));
        public void AfterReturning(object returnValue, MethodInfo method, object[] args, object target)
        {
            _logger.Info("This method call returned successfully : " + method.Name);
            _logger.Info("    The target was                     : " + target);
            _logger.Info("    The arguments were                 : ");
            if (args != null)
            {
                foreach (object arg in args)
                {
                    _logger.Info("\t: " + arg);
                }
            }
            _logger.Info("    The return value is                : " + returnValue);
        }
    }

    public class CustomLoggingThrowsAdvice : IThrowsAdvice
    {
        ILog _logger = LogManager.GetLogger(typeof(CustomLoggingThrowsAdvice));
        public void AfterThrowing(IndexOutOfRangeException ex)
        {
            _logger.Error($"IndexOutOfRangeException : {ex.Message}");

           
        }

        public void AfterThrowing(Exception ex)
        {
            _logger.Error($"{ex.Message}");
        
        }
    }


}
