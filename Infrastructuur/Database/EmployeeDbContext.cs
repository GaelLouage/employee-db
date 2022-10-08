using Infrastructuur.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Infrastructuur.Dtos;
using Infrastructuur.Mappers;

namespace Infrastructuur.Database
{
    public sealed class EmployeeDbContext
    {
        private  MongoClient dbClient = new MongoClient("mongodb://localhost:27017/EmployeeMongo");
     
        //employees
        public async Task<List<EmployeeEntity>> GetAllEmployeesAsync()
        {
            // get the database
            var employees = new List<EmployeeEntity>();
            var database = dbClient.GetDatabase("EmployeeMongo");
            // get the collection in that database
            var collection = database.GetCollection<BsonDocument>("Employees");
            // change one onbject for objectid replacer otherwise error in code
            var json = (await collection.FindAsync(new BsonDocument())).ToList();
            foreach (var item in json)
            {
                employees.Add(JsonConvert.DeserializeObject<EmployeeEntity>(item
                    .ToString()
                    .Replace("ObjectId(", "")
                    .Replace(")", "")));
            }
            return employees;
        }
        public async Task<EmployeeResultDto> CreateEmployee(EmployeeEntity employee)
        {
            var employeeRestult = new EmployeeResultDto();
            var database = dbClient.GetDatabase("EmployeeMongo");
            var allEmployeesCount = (await GetAllEmployeesAsync()).Max(x => x.IdNumber) +1;
            var addressExist = (await GetAllAddressesAsync()).Any(x => x.Id == employee.AddressId);
            if(addressExist == false)
            {
                employeeRestult.Errors.Add($"Cannot create user. Address with id {employee.AddressId} does not exist!");
                return employeeRestult;
            }
            // get the collection in that database
            var collection = database.GetCollection<BsonDocument>("Employees");
            var serialize = JsonConvert.SerializeObject(employee);
           
            await collection.InsertOneAsync(new BsonDocument
            {
                {"id", allEmployeesCount},
                {"name",employee.Name },
                {"addressId", employee.AddressId }
            }); 
            
            return employee.EmployeeMapper(employeeRestult.Errors);
        }
        // update employee
        public async Task<EmployeeResultDto> UpdateEmployeeAsync(EmployeeEntity employee) 
        {
            var employeeResult = new EmployeeResultDto();
            var database = dbClient.GetDatabase("EmployeeMongo");
            var addressExist = (await GetAllAddressesAsync()).Any(x => x.Id == employee.AddressId);
            if (addressExist == false)
            {
                employeeResult.Errors.Add($"Cannot create user. Address with id {employee.AddressId} does not exist!");
                return employeeResult;
            }
            // get the collection in that database
            var collection = database.GetCollection<BsonDocument>("Employees");
            var serialize = JsonConvert.SerializeObject(employee);
           
            var document = new BsonDocument
            {
                {"id", employee.IdNumber},
                {"name",employee.Name },
                {"addressId", employee.AddressId }
            };
            // filter based on the document with employee id equaling employee.IdNumber.
            var filter = Builders<BsonDocument>.Filter.Eq("id", employee.IdNumber);

            // to update
            var updateName = Builders<BsonDocument>.Update.Set("name", employee.Name);
            var updateAddressId = Builders<BsonDocument>.Update.Set("addressId", employee.AddressId);
            collection.UpdateOne(filter, updateName);
            collection.UpdateOne(filter, updateAddressId);
         
            return employee.EmployeeMapper(employeeResult.Errors); ;
        }
        //delete employee
        public async Task<EmployeeEntity> DeleteEmployeeAsync(EmployeeEntity employee)
        {
            var database = dbClient.GetDatabase("EmployeeMongo");

            // get the collection in that database
            var collection = database.GetCollection<BsonDocument>("Employees");
            var serialize = JsonConvert.SerializeObject(employee);
            // make error if this document(employee) does not exist

            // filter based on the document with employee id equaling employee.IdNumber.
           
            var filter = Builders<BsonDocument>.Filter.Eq("id", employee.IdNumber);
            //delete
            collection.DeleteOne(filter);

            return employee;
        }
        // addresses
        public async Task<List<AddressEntity>> GetAllAddressesAsync()
        {
            var addresses = new List<AddressEntity>();
            // get the database
            var database = dbClient.GetDatabase("EmployeeMongo");
            // get the collection in that database
            var collection = database.GetCollection<BsonDocument>("Address");
            // change one onbject for objectid replacer otherwise error in code
            var json = (await collection.FindAsync(new BsonDocument())).ToList();
            foreach (var item in json)
            {
                addresses.Add(JsonConvert.DeserializeObject<AddressEntity>(item
                    .ToString()
                    .Replace("ObjectId(", "")
                    .Replace(")", "")));
            }

            return addresses;
        }
        public async Task<AddressEntity> CreateAddress(AddressEntity address)
        {
            var database = dbClient.GetDatabase("EmployeeMongo");
            var allEmployeesCount = (await GetAllAddressesAsync()).Max(x => x.Id) + 1;
            // get the collection in that database
            var collection = database.GetCollection<BsonDocument>("Address");
            var serialize = JsonConvert.SerializeObject(address);
            await collection.InsertOneAsync(new BsonDocument
            {
                {"id", allEmployeesCount},
                {"addressLine",address.AddressLine}
            });
            return address;
        }
        // update employee
        public async Task<AddressEntity> UpdateAddressAsync(AddressEntity addresss)
        {
            var database = dbClient.GetDatabase("EmployeeMongo");

            // get the collection in that database
            var collection = database.GetCollection<BsonDocument>("Address");
            var serialize = JsonConvert.SerializeObject(addresss);

            var document = new BsonDocument
            {
                {"addressLine",addresss.AddressLine}
            };
            // filter based on the document with employee id equaling employee.IdNumber.
            var filter = Builders<BsonDocument>.Filter.Eq("id", addresss.Id);

            // to update
            var updateName = Builders<BsonDocument>.Update.Set("addressLine", addresss.AddressLine);
 
            collection.UpdateOne(filter, updateName);
      

            return addresss;
        }
        //delete employee
        public async Task<AddressEntity> DeleteAddressAsync(AddressEntity address)
        {
            var database = dbClient.GetDatabase("EmployeeMongo");

            // get the collection in that database
            var collection = database.GetCollection<BsonDocument>("Address");
            var serialize = JsonConvert.SerializeObject(address);
            // make error if this document(employee) does not exist
          
            // filter based on the document with employee id equaling employee.IdNumber.
            var filter = Builders<BsonDocument>.Filter.Eq("id", address.Id);
            //delete
            collection.DeleteOne(filter);

            return address;
        }
    }
}
