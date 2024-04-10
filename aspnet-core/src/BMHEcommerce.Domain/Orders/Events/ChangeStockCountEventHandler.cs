using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace BMHEcommerce.Orders.Events
{
    public class ChangeStockCountEventHandler : ILocalEventHandler<NewOrderCreatedEvent>, ITransientDependency
    {
        public Task HandleEventAsync(NewOrderCreatedEvent eventData)
        {
            throw new NotImplementedException();
        }
    }
}
