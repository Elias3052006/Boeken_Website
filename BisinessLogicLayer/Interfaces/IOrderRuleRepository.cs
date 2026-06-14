using System;
using BisinessLogicLayer.Domain;

namespace BisinessLogicLayer.Interfaces
{
    public interface IOrderRuleRepository
    {
        bool AddOrderRule(OrderRule orderRule);
    }
}
