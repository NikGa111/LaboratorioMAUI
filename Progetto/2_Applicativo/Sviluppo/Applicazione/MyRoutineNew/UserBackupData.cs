using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyRoutine.Models;
using MyRoutineNew.Models;

namespace MyRoutineNew
{
    public class UserBackupData
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }

        public List<Attivita> Tasks { get; set; } // TaskItem deve essere definito nel namespace corretto

        public List<Badge> Badges { get; set; }

        public int CurrentStreak { get; set; }
        public int BestStreak { get; set; }

        public int TotalCompletedTasks { get; set; }

        public DateTime ExportDate { get; set; }
    }
}
