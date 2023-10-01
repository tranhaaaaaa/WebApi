using FinalApi.Services.Repository;
using Quartz;


namespace FinalApi.Jobs
{
    public class CreateCustomerVipJob : IJob
    {
        private readonly ICustomerService _customerService;
       
        public CreateCustomerVipJob( ICustomerService customerService)
        {  
           _customerService = customerService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _customerService.CreateCustomerVipAsync();
        }

    }
}
