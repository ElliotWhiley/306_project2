using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;
using WebApplication1.Models;
using System.Net.Http.Formatting;

namespace WebApplication1.Controllers
{ 
    public class ValuesController : ApiController
    {
        //This method sorts the highscores by time order
        List<ScoreEntity> sortEntitiesByTime(List<ScoreEntity> scoreList)
        {
            // sort the list by time
            scoreList.Sort((x, y) => x.Time.CompareTo(y.Time));

            // create an empty list
            List<ScoreEntity> listToReturn = new List<ScoreEntity>();

            // If there are more than 8 scores inside the list, retrieve the top 8, otherwise return the whole list
            if (scoreList.Count >= 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    listToReturn.Add(scoreList[i]);
                }

            }
            else
            {
                for (int i = 0; i < scoreList.Count; i++)
                {
                    listToReturn.Add(scoreList[i]);
                }
            }

            return listToReturn;
        }

        
        // GET api/values/id
        public List<ScoreEntity> Get(int id)
        {
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            // Create the CloudTable object that represents the "FlubbaLeaderborad" table.
            CloudTable table = tableClient.GetTableReference("FlubbaLeaderboard");

            // Construct the query operation for all score entities from level = id (where id = game level)
            TableQuery<ScoreEntity> query = new TableQuery<ScoreEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id.ToString()));

            List<ScoreEntity> listToReturn = new List<ScoreEntity>();
            // place each entity into a processable list
            foreach (ScoreEntity entity in table.ExecuteQuery(query))
            {
                listToReturn.Add(entity);
            }
            return sortEntitiesByTime(listToReturn);
        }
        
        // POST api/values
        public void Post(FormDataCollection form)
        {
            // extract field values from the received form
            string level = form.Get("level");
            string name = form.Get("name");
            string time = form.Get("time");
            string guid = form.Get("guid");
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            // Create the CloudTable object that represents the "FlubbaLeaderboard" table.
            CloudTable table = tableClient.GetTableReference("FlubbaLeaderboard");

            // Create a new score entity.
            ScoreEntity score1 = new ScoreEntity(level, guid);
            score1.Name = name;
            score1.Time = time;

            // Create the TableOperation object that inserts the score entity.
            TableOperation insertOperation = TableOperation.Insert(score1);

            // Execute the insert operation.
            table.Execute(insertOperation);
        }

        // POST api/values (create new tables)
        [Route("createtables")]
        public void PostTables()
        {
            //retrieve storage account
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            CloudTable table = tableClient.GetTableReference("FlubbaLeaderboard");
            table.CreateIfNotExists();
        }
    }
}
