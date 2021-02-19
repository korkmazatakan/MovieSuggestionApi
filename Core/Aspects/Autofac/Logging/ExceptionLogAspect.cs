using System;
using System.Collections.Generic;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Entities.Concrete;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.Messages;


namespace Core.Aspects.Autofac.Logging
{
    public class ExceptionLogAspect : MethodInterception
    {
        LoggerServiceBase _loggerServiceBase;

        public ExceptionLogAspect()
        {
            _loggerServiceBase = new LoggerServiceBase();
        }


        protected override void OnException(IInvocation invocation, Exception ex)
        {
            _loggerServiceBase.Error("Internal server error",ex);
        }

    }
}
