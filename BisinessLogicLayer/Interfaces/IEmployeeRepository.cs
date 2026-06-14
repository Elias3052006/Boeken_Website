using System;
using BisinessLogicLayer.Domain;

namespace BisinessLogicLayer.Interfaces
{
    public interface IEmployeeRepository
    {
       
        int GetEmployeeIdByName(string employeeName);
        string GetRole(string username, string password); 
        
    }
}
