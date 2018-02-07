using DatabaseCommunicator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Tenderingportal.Utils;

namespace Tenderingportal.Models
{
    public class Phase
    {
        public int ID { get; set; } = -1;
        public int? IDproject { get; set; }
        public string name { get; set; }
        public DateTime date { get; set; }


        private void Update()
        {

            checkMyself();

            string query = "update [Phase] set " +
                " IDproject = '" + this.IDproject + "'," +
                " date = '" + DateAndTime.getTimeForSql(this.date) + "'," +
                " name = '" + this.name + "'" +
                "  where ID = " + this.ID
                ;

            Database.QueryNoResult(query);


        }

        private void Insert()
        {

            string query = "INSERT INTO [Project] ("
                            + "IDproject, name"
                            + ",date)"
                           + " VALUES "
                            + " (" +
                            "'" + this.IDproject + "'," +
                            "'" + this.name + "'," +
                            "'" + DateAndTime.getTimeForSql(this.date) + "); select scope_identity() as ID;"
                  ;

            DataTable table = Database.Query(query);
            this.ID = int.Parse(table.Rows[0]["ID"].ToString());


        }

        public Phase Save()
        {
            if (this.ID <= 0) Insert();
            else Update();
            return this;
        }


        /// <summary>
        /// Here is all checking about user data, if needed
        /// </summary>
        /// <returns></returns>
        private void checkMyself()
        {
            //if (this.email.Contains("gmail.com")) {
            //    throw new Exception("Users can not have gmail email address");
            //}
        }


 

        #region private helpers

        public static Phase createFromDataRow(DataRow row)
        {
            Phase phase = new Phase();
            phase.ID = int.Parse(row["ID"].ToString());
            phase.IDproject = null;
            int newId = 0;
            if (int.TryParse(row["IDproject"].ToString(), out newId)) {
                phase.IDproject = newId;
            }
            phase.name =row["name"].ToString();
            phase.date= DateTime.Parse(row["date"].ToString());

            return phase;
        }
        #endregion


    }
}