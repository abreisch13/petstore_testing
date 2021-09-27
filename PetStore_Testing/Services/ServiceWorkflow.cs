using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PetStore_Testing.Models;
using PetStore_Testing.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace PetStore_Testing.Services
{
    class ServiceWorkflow
    {

        public readonly ITestOutputHelper loggerOutput;

        public ServiceWorkflow(ITestOutputHelper helper)
        {
            this.loggerOutput = helper;
        }

        public void ValidatePetIsAdded(object jsonInput)
        {
            var data = JObject.Parse(jsonInput.ToString());
            var requests = new CreatePet_Request();
            CreatePet_RequestObject objRequest = new CreatePet_RequestObject();
            objRequest = JsonConvert.DeserializeObject<CreatePet_RequestObject>(jsonInput.ToString());
            objRequest.id = int.Parse(data["id"].ToString());
            objRequest.name = data["name"].ToString();

            Category category = new Category();
            category.id = int.Parse(data["category"]["id"].ToString());
            category.name = data["category"]["name"].ToString();
            objRequest.category = category;

            for (int i = 0; i < objRequest.photoUrls.Count; i++)
            {
                var photoUrl = objRequest.photoUrls[i].ToString();
            }
            for (int i = 0; i < objRequest.tags.Count; i++)
            {
                var tagID = objRequest.tags[i].id.ToString();
                var tagName = objRequest.tags[i].name.ToString();
            }

            objRequest.status = data["status"].ToString();


            requests.createPetRequestObjectProperty = objRequest;

            var responseObj = new PetStoreAPIActions(loggerOutput).Validate_AddPet(requests);

            if (responseObj != null)
            {
                Assert.True(responseObj.Id == objRequest.id, "ID matches");
                Assert.True(responseObj.Name == objRequest.name, "Name matches");
                Assert.True(responseObj.Status == objRequest.status, "Status matches");

                for(int i = 0; i < responseObj.Tags.Length; i++)
                {
                    Assert.True(responseObj.Tags[i].id == objRequest.tags[i].id, "Tag ID matches");
                    Assert.True(responseObj.Tags[i].name == objRequest.tags[i].name, "Tag name matches");
                }
                for (int i = 0; i < responseObj.PhotoUrls.Length; i++)
                {
                    Assert.True(responseObj.PhotoUrls[i].ToString() == objRequest.photoUrls[i].ToString(), "Photo URL matches");
                }

            }
        }

        public bool validate_getPetResponse(string id, object jsonInput)
        {
            var data = JObject.Parse(jsonInput.ToString());
            var response = new PetStoreAPIActions(loggerOutput).getPetInfoByID(id);
            if (response.Equals("Pet created"))
            {
                return true;
            }
            else
                return false;


        }

        public bool getPetsByStatus(string status)
        {
            var response = new PetStoreAPIActions(loggerOutput).getPetsByStatus(status);
            if (response.Equals("No pets found"))
                return false;
            else
                return true;

        }
    }
}
