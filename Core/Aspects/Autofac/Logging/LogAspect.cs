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
    public class LogAspect:MethodInterception
    {
        LoggerServiceBase _loggerServiceBase;

        public LogAspect()
        {
            _loggerServiceBase = new LoggerServiceBase();
        }


        protected override void OnBefore(IInvocation invocation)
        {
            var logDetail = GetLogDetail(invocation);
            var parameters = logDetail.LogParameters;
            var parameterName = "";
            var parameterValue = "";
            var parameterType = "";

            for (int i = 0; i < parameters.Count; i++)
            {
                parameterName += $"{parameters[i].Name}";
                parameterType += $"{parameters[i].Type}";
                parameterValue += $"{parameters[i].Value}";

                if (i == parameters.Count-1)
                {
                    break;
                }
                parameterName += ", ";
                parameterType += ", ";
                parameterValue += ", ";

            }

            _loggerServiceBase.Info(logDetail.MethodName + "(" + parameterType + ") ->" + parameterValue  + " -> " + parameterName );
        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = $"{invocation.GetConcreteMethod().GetParameters()[i]?.Name}",
                    Value = $"{invocation.Arguments[i]}",
                    Type = $"{invocation.Arguments[i]?.GetType().Name}"
                });
            }

            var logDetail = new LogDetail
            {
                MethodName = invocation.TargetType.Name + "-" + invocation.Method.Name,
                LogParameters = logParameters
            };

            return logDetail;
        }
    }
}
