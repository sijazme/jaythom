using MVC5.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace MVC5
{
    [ServiceContract]  
    public interface IService
    {

        [OperationContract]
        [WebInvoke(UriTemplate = "GetAnimal/{Id}", Method = "GET")]
        Animal GetAnimal(int? Id);

        [OperationContract]
        List<Animal> GetAnimals();

        [OperationContract]
        void AddAnimal(Animal animal);

        [OperationContract]      
        void UpdateAnimal(Animal animal);

        [OperationContract]
        void DeleteAnimal(int id);

        [OperationContract]
        List<Staff> GetAvailableStaff();

        [OperationContract]
        void UpdateStaff(Staff staff);

        [OperationContract]
        void DeleteStaff(int id);

        [OperationContract]
        void CreateStaff(Staff staff);

        [OperationContract]
        List<Staff> GetStaffs();

        [OperationContract]
        [WebInvoke(UriTemplate = "GetStaff/{Id}", Method = "GET")]
        Staff GetStaff(int? id);
    }
}
