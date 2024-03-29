﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.DynamicProxy;
using Core.Aspects.Autofac.Logging;

namespace Core.Utilities.Interceptors.Autofac
{
    public class AspectInterceptorSelector:IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true);

            classAttributes.AddRange(methodAttributes);
            classAttributes.Add(new ExceptionLogAspect());

            return classAttributes.OrderBy(x => x.Priority).ToArray();

        }
    }
}
