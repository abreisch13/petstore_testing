using Newtonsoft.Json;
using PetStore_Testing.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;


namespace PetStore_Testing.Helpers
{
    public class PetStoreAPIActions
    {
        private ITestOutputHelper loggerOutput;

        
        public ITestOutputHelper Logger
        {
            get
            {
                return loggerOutput;
            }
            set
            {
                loggerOutput = value;
            }
        }

        public PetStoreAPIActions(ITestOutputHelper output)
        {
            this.loggerOutput = output;
        }

        public CreatePet_Response Validate_AddPet(CreatePet_Request createPetRequest)
        {
            RestClient restClient = new RestClient();
            RestRequest restRequest = new RestRequest(Method.POST);
            IRestResponse restResponse;

            // Call URl to add pet
            restClient.BaseUrl = new Uri(PetStoreAPIMethods.AddNewPet);

            // Include test data from json file
            var request = JsonConvert.SerializeObject(createPetRequest.createPetRequestObjectProperty);
            restRequest.AddJsonBody(request);

            
            restResponse = restClient.Execute(restRequest);

            if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<CreatePet_Response>(restResponse.Content);
            }
            else
            {
                // log error
                loggerOutput.WriteLine("Add Pet Failed, invalid input: " + restResponse.StatusCode);
                return null;
            }

        }

        public string getPetInfoByID(string id)
        {
            RestClient restClient = new RestClient();
            RestRequest restRequest = new RestRequest(Method.POST);
            IRestResponse restResponse;

            var url = PetStoreAPIMethods.GetPetByID.Replace("{id}", id);
            restClient.BaseUrl = new Uri(url);
            restResponse = restClient.Execute(restRequest);

            if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return "Pet created";
            }
            else
                return null;
        }

        public string getPetsByStatus(string status)
        {
            RestClient restClient = new RestClient();
            RestRequest restRequest = new RestRequest(Method.GET);
            IRestResponse restResponse;

            var url = PetStoreAPIMethods.GetPetsByStatus.Replace("{status}", status);
            restClient.BaseUrl = new Uri(url);
            restResponse = restClient.Execute(restRequest);

            if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                loggerOutput.WriteLine(restResponse.Content);
                return "Pets found for status = : " + status;
            }
            else
                return "No pets found";


        }

        

    }

}