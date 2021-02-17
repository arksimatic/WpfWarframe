using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WpfWarframe.Model.API.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace WpfWarframe.Model.API
{
    class WarframeAPI
    {
        private static WarframeAPI _instance;
        private WebClient _client;
        public static WarframeAPI GetInstance()
        {
            if (_instance == null)
            {
                _instance = new WarframeAPI();

                _instance._client = new WebClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                _instance._client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");

                _instance.UpdateData();
            }

            return _instance;
        }

        public List<Fissure> Fissures { get; private set; }

        public Sortie Sortie { get; private set; }

        public void UpdateFissures()
        {
            String fissureResponse = _client.DownloadString("https://api.warframestat.us/PC/fissures");

            JArray fissuresJArray = JArray.Parse(fissureResponse);

            List<Fissure> fissureList = new List<Fissure>();
            foreach (JObject fissureJObject in fissuresJArray)
            {
                Fissure fissure = JsonConvert.DeserializeObject<Fissure>(fissureJObject.ToString());
                if(!fissure.expired)
                    fissureList.Add(fissure);
            }

            fissureList = fissureList.OrderBy(o => o.tierNum).ToList();

            Fissures = fissureList;
            //return fissureList;
        }

        public void UpdateSortie()
        {
            String sortieResponse = _client.DownloadString("https://api.warframestat.us/PC/sortie");

            Sortie sortie = JsonConvert.DeserializeObject<Sortie>(sortieResponse.ToString());

            Sortie = sortie;
            //return sortie;
        }


        public void UpdateData()
        {
            UpdateFissures();
            UpdateSortie();

            Task task = new Task(() =>
            {
                while (true)
                {
                    UpdateFissures();
                    UpdateSortie();

                    Thread.Sleep(60000);
                }
            });
            task.Start();
        }
    }
}
