using System;
using System.Collections.Generic;
using System.Text;

namespace PetStore_Testing.Helpers
{
    public class PetStoreAPIMethods
    {
        public static string AddNewPet
        {
            get
            {
                return $"{CustomConfigurationProvider.GetKey("petStoreService-baseUrl")}" + "v2/pet";
            }
        }

        public static string UploadImage
        {
            get
            {
                return $"{CustomConfigurationProvider.GetKey("petStoreService-baseUrl")}" + "v2/{id}/uploadImage";
            }
        }

        public static string GetPetByID
        {
            get
            {
                return $"{CustomConfigurationProvider.GetKey("petStoreService-baseUrl")}" + "v2/pet/{id}";
            }

        }

        public static string GetPetsByStatus
        {
            get
            {
                return $"{CustomConfigurationProvider.GetKey("petStoreService-baseUrl")}" + "v2/pet/findByStatus?status={status}";
            }
        }

    }

}