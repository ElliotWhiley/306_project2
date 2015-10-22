using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ScoreEntity : TableEntity
    {
        public ScoreEntity(string level, string guid)
        {
            this.PartitionKey = level;
            this.RowKey = guid;
        }

        public ScoreEntity() { }

        public string Name { get; set; }

        public string Time { get; set; }
    }
}