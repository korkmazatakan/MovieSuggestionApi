using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Mailing;
using Core.Utilities.Interceptors.Autofac;

namespace Core.Aspects.Autofac.Mailing
{
    public class MailAspect:MethodInterception
    {
        protected override void OnSuccess(IInvocation invocation)
        {
            Email email = new Email
                ("otokon@otokon.tech","ata58kann@gmail.com","aktivasyon testi",false,"movie suggestion aktivasyon testi :)");
            Credentials credentials = new Credentials("otokon@otokon.tech","selami58A",587,"mail.otokon.tech");
            
            EmailTool.Sent(email,credentials);
        }
    }
}