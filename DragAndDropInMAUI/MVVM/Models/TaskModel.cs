using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragAndDropInMAUI.MVVM.Models
{
	public class TaskModel
	{
		public String TaskName { get; set; }
		public int TaskStatus { get; set; }
		public int TaskId { get; set; }
	}
	public enum TaskStatusOptions
	{
		NewTask,
		InProgress,
		InReview,
		Done
	}
}