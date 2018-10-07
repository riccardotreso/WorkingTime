using System;
using SQLite;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.IO;

namespace WorkingTime
{
    [Table("TimeWork")]
    public class TimeWork{
        [PrimaryKey, AutoIncrement, Column("ID")]
        public int ID { get; set; }
        public DateTime WorkingStart { get; set; }
        public DateTime WorkingEnd { get; set; }
    }

    public class DB
    {
        private static string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ATMTime.db3");
        private static SQLiteConnection db;
        public DB()
        {
            db = new SQLiteConnection(dbPath);
			db.CreateTable<TimeWork>();

            
        }


        public TimeWork InsertWorkTime(DateTime Start, DateTime End){

            var timeWork = new TimeWork
            {
                WorkingStart = Start,
                WorkingEnd = End
            };

            db.CreateTable<TimeWork>();
            db.Insert(timeWork); // after creating the newStock object
            return timeWork;
            
        }

		internal void DeleteWorkItem(TimeWork t)
		{
			db.Delete<TimeWork>(t.ID); 
		}

		public List<TimeWork> GetWorkTime(){
            var dbTable = db.Table<TimeWork>();
            if(dbTable != null)
				return dbTable.OrderByDescending(x=> x.WorkingStart).ToList();
            return new List<TimeWork>();
        }



    }
}
