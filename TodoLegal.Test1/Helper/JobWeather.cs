using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TodoLegal.Test1.Helper
{
    public class JobWeather : IJob
    {
        public virtual Task Execute(IJobExecutionContext context)
        {
            Tools.RegisterInfo($"Wheather execution task! - {DateTime.Now:r}");
            return Task.CompletedTask;
        }
    }
}