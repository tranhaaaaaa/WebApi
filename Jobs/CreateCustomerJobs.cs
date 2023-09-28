using FinalApi.Services.Repository;
using Quartz;


namespace FinalApi.Jobs
{
    public class CreateCustomerVipJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;
       
        public CreateCustomerVipJob( IUnitOfWork unitOfWork)
        {  
            _unitOfWork = unitOfWork;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _unitOfWork.CustomerRequest.CreateCustomerVipAsync();
        }

    }
}
