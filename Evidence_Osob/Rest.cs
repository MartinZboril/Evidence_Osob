using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evidence_Osob
{
    public class Rest
    {
        public async Task<ObservableCollection<Person>> GetPersonsListAsync()
        {
            string url = "https://student.sps-prosek.cz/~zborima14/json.php";
            var client = new RestClient(url);
            var request = new RestRequest("resource/{id}", Method.POST);
            request.AddHeader("header", "value");

            ObservableCollection<Person> persons = new ObservableCollection<Person>();

            IRestResponse response = client.Execute(request);
            persons = JsonConvert.DeserializeObject<ObservableCollection<Person>>((response.Content));
            return persons;

        }
    }
}
